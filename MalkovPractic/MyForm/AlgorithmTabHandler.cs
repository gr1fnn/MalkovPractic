using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Algorithms.Core;
using Algorithms.Pipelines;
using Algorithms.Preprocessing;

namespace MyForm
{
    public abstract class AlgorithmTabHandler
    {
        protected MainForm MainForm { get; }
        public Pipeline Pipeline { get; set; }
        protected DefaultDataPreprocessor Preprocessor => MainForm.SharedPreprocessor;
        protected string[] ColumnNames => MainForm.ColumnNames;
        protected string CurrentFilePath => MainForm.CurrentFilePath;

        public TabPage TabPage { get; }
        public List<Control> InputFields { get; protected set; } = new List<Control>();
        public List<int> SelectedFeatureIndices { get; protected set; } = new List<int>();

        // Контролы вкладки
        protected CheckedListBox FeaturesListBox { get; }
        protected ComboBox TargetComboBox { get; }
        protected ComboBox ProblemTypeComboBox { get; }
        protected NumericUpDown KControl { get; }
        protected TextBox ResultTextBox { get; }
        public Button PredictButton { get; }
        protected ProgressBar ProgressBar { get; }
        protected Label StatusLabel { get; }
        protected ComboBox NormalizationComboBox { get; set; }

        public AlgorithmTabHandler(
            MainForm mainForm,
            TabPage tabPage,
            CheckedListBox featuresListBox,
            ComboBox targetComboBox,
            ComboBox problemTypeComboBox,
            ComboBox normalizationComboBox,  
            NumericUpDown kControl,
            TextBox resultTextBox,
            Button predictButton,
            ProgressBar progressBar,
            Label statusLabel)
        {
            MainForm = mainForm;
            TabPage = tabPage;
            FeaturesListBox = featuresListBox;
            TargetComboBox = targetComboBox;
            ProblemTypeComboBox = problemTypeComboBox;
            NormalizationComboBox = normalizationComboBox;  
            KControl = kControl;
            ResultTextBox = resultTextBox;
            PredictButton = predictButton;
            ProgressBar = progressBar;
            StatusLabel = statusLabel;

            InitializeControls();
        }

        protected virtual void InitializeControls()
        {
            if (ProblemTypeComboBox.Items.Count == 0)
            {
                ProblemTypeComboBox.Items.Add("Классификация");
                ProblemTypeComboBox.SelectedIndex = 0;
            }
            if (NormalizationComboBox != null && NormalizationComboBox.Items.Count == 0)
            {
                NormalizationComboBox.Items.Add("Без нормирования");
                NormalizationComboBox.Items.Add("Z-Score (статистическое)");
                NormalizationComboBox.Items.Add("Min-Max (линейное)");
                NormalizationComboBox.Items.Add("Логарифмическое");
                NormalizationComboBox.SelectedIndex = 0; 
            }

            KControl.Minimum = 1;
            KControl.Maximum = 10000;
            KControl.Value = GetDefaultKValue();
        }

        protected abstract int GetDefaultKValue();
        protected abstract Type GetAlgorithmType();
        protected abstract Dictionary<string, object> GetAlgorithmParameters();
        protected virtual NormalizationType GetNormalizationType()
        {
            if (NormalizationComboBox == null || NormalizationComboBox.SelectedIndex < 0)
                return NormalizationType.None;

            return NormalizationComboBox.SelectedIndex switch
            {
                0 => NormalizationType.None,
                1 => NormalizationType.ZScore,
                2 => NormalizationType.MinMax,
                3 => NormalizationType.LogScale,
                _ => NormalizationType.None
            };
        }

        public virtual void UpdateFeatureSelection(string[] columnNames)
        {
            // Сохраняем выбранные элементы
            var selectedItems = new List<string>();
            for (int i = 0; i < FeaturesListBox.Items.Count; i++)
            {
                if (FeaturesListBox.GetItemChecked(i))
                {
                    selectedItems.Add(FeaturesListBox.Items[i].ToString());
                }
            }

            // Обновляем список признаков
            FeaturesListBox.Items.Clear();
            foreach (string columnName in columnNames)
            {
                FeaturesListBox.Items.Add(columnName, selectedItems.Contains(columnName));
            }

            // Обновляем целевой признак
            TargetComboBox.Items.Clear();
            foreach (string columnName in columnNames)
            {
                TargetComboBox.Items.Add(columnName);
            }
            if (TargetComboBox.Items.Count > 0)
                TargetComboBox.SelectedIndex = 0;
        }

        public virtual void Train()
        {
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                MessageBox.Show("Сначала выберите файл с данными", "Ошибка");
                return;
            }

            try
            {
                StartTraining();

                // Получаем выбранные признаки
                SelectedFeatureIndices.Clear();
                for (int i = 0; i < FeaturesListBox.Items.Count; i++)
                {
                    if (FeaturesListBox.GetItemChecked(i) &&
                        FeaturesListBox.Items[i].ToString() != TargetComboBox.SelectedItem.ToString())
                    {
                        SelectedFeatureIndices.Add(i);
                    }
                }

                ValidateSelection();

                // Получаем выбранный тип нормирования
                NormalizationType normType = GetNormalizationType();
                Console.WriteLine($"[{GetAlgorithmType().Name}] Выбран тип нормирования: {normType}");

                // Создаем конфигурацию
                var datasetConfig = CreateDatasetConfig();

                // Создаем и обучаем Pipeline с учетом нормирования
                Pipeline = new Pipeline(datasetConfig);

                // Устанавливаем тип нормирования ПЕРЕД обучением
                Pipeline.SetNormalizationType(normType);
                Pipeline.LoadAndTrain(CurrentFilePath);

                // Сохраняем препроцессор (если это первая вкладка)
                if (MainForm.SharedPreprocessor == null)
                {
                    MainForm.SharedPreprocessor = Pipeline.Preprocessor;
                }

                // Создаем поля для ввода
                CreateInputFields();

                // Показываем результаты с информацией о нормировании
                ShowTrainingResults(normType);

                PredictButton.Enabled = true;
                UpdateStatus($"Модель обучена! K = {KControl.Value}, Нормирование: {NormalizationComboBox.SelectedItem}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обучении: {ex.Message}", "Ошибка");
                UpdateStatus("Ошибка при обучении");
            }
            finally
            {
                FinishTraining();
            }
        }

        public virtual void Predict()
        {
            if (Pipeline == null || Preprocessor == null)
            {
                MessageBox.Show("Сначала обучите модель", "Ошибка");
                return;
            }

            try
            {
                ValidateInputFields();

                // Собираем значения признаков
                var features = CollectFeaturesFromInputFields();

                // Делаем предсказание
                double prediction = Pipeline.Predict(features);

                // Отображаем результат
                DisplayPrediction(prediction, features);

                UpdateStatus("Предсказание выполнено успешно");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка предсказания: {ex.Message}", "Ошибка");
                UpdateStatus("Ошибка предсказания");
            }
        }

        protected virtual void StartTraining()
        {
            ProgressBar.Value = 0;
            ProgressBar.Style = ProgressBarStyle.Marquee;
            UpdateStatus("Обучение модели...");
        }

        protected virtual void FinishTraining()
        {
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.Value = 100;
        }

        protected virtual void UpdateStatus(string message)
        {
            if (StatusLabel != null && !StatusLabel.IsDisposed)
            {
                StatusLabel.Text = $"Статус: {message}";
                StatusLabel.Refresh();
            }
        }

        protected virtual DatasetConfig CreateDatasetConfig()
        {
            int targetIndex = TargetComboBox.SelectedIndex;
            var problemType = ProblemTypeComboBox.Text == "Классификация"
                ? ProblemType.Classification
                : ProblemType.Regression;

            return new DatasetConfig
            {
                Name = System.IO.Path.GetFileNameWithoutExtension(CurrentFilePath),
                HasHeader = true,
                FeatureColumns = SelectedFeatureIndices.ToArray(),
                LabelColumn = targetIndex,
                ProblemType = problemType,
                AlgorithmType = GetAlgorithmType(),
                AlgorithmParameters = GetAlgorithmParameters()
            };
        }

        protected virtual void ValidateSelection()
        {
            if (SelectedFeatureIndices.Count == 0)
                throw new Exception("Выберите хотя бы один признак");

            if (TargetComboBox.SelectedIndex < 0)
                throw new Exception("Выберите целевую переменную");
        }

        protected virtual void CreateInputFields()
        {
            // Удаляем старые поля
            foreach (var field in InputFields)
            {
                if (TabPage.Controls.Contains(field))
                    TabPage.Controls.Remove(field);
            }
            InputFields.Clear();

            // Удаляем старые лейблы
            var labelsToRemove = TabPage.Controls.OfType<Label>()
                .Where(l => l.Text.StartsWith("Признак") || l.Name?.StartsWith("inputLabel_") == true)
                .ToList();
            foreach (var label in labelsToRemove)
            {
                TabPage.Controls.Remove(label);
            }

            if (SelectedFeatureIndices.Count <= 0) return;

            int startX = 12;
            int startY = ProblemTypeComboBox.Bottom + 10;

            // Загружаем данные для анализа
            var rawData = Algorithms.Preprocessing.DataLoader.LoadCSV(CurrentFilePath, true);

            for (int i = 0; i < SelectedFeatureIndices.Count; i++)
            {
                // Получаем имя признака и его индекс
                int featureIndex = SelectedFeatureIndices[i];
                string featureName = ColumnNames[featureIndex];

                // Анализируем столбец, чтобы определить тип
                bool isNumeric = IsColumnNumericDirectly(rawData, featureIndex);

                // Label для поля ввода
                var label = new Label
                {
                    Text = $"{featureName}:",
                    Location = new Point(startX, startY + i * 30),
                    Size = new Size(100, 20),
                    Name = $"inputLabel_{i}_{GetAlgorithmType().Name}",
                    Font = new Font("Microsoft Sans Serif", 8.25f),
                    Tag = isNumeric ? "numeric" : "categorical"
                };
                TabPage.Controls.Add(label);
                label.BringToFront();

                Control inputControl;

                if (isNumeric)
                {
                    // Для числовых признаков - TextBox
                    var textBox = new TextBox
                    {
                        Location = new Point(startX + 105, startY + i * 30),
                        Size = new Size(100, 20),
                        Name = $"inputField_{i}_{GetAlgorithmType().Name}",
                        Font = new Font("Microsoft Sans Serif", 8.25f),
                        Tag = featureIndex
                    };
                    inputControl = textBox;
                }
                else
                {
                    var comboBox = new ComboBox
                    {
                        Location = new Point(startX + 105, startY + i * 30),
                        Size = new Size(100, 20), 
                        Name = $"inputField_{i}_{GetAlgorithmType().Name}",
                        Font = new Font("Microsoft Sans Serif", 8.25f),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Tag = featureIndex
                    };

                    var uniqueValues = GetUniqueValuesFromRawData(rawData, featureIndex);

                    if (uniqueValues.Count == 0)
                    {
                        var mapping = Preprocessor?.GetColumnMapping(featureIndex);
                        if (mapping != null && mapping.Count > 0)
                        {
                            foreach (var kvp in mapping.OrderBy(kvp => kvp.Value))
                            {
                                comboBox.Items.Add(kvp.Key);
                            }
                        }
                        else
                        {
                            comboBox.Items.Add("<нет данных>");
                        }
                    }
                    else
                    {
                        // Добавляем все уникальные значения
                        foreach (var value in uniqueValues.OrderBy(v => v))
                        {
                            comboBox.Items.Add(value);
                        }
                    }

                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;

                    inputControl = comboBox;
                }

                TabPage.Controls.Add(inputControl);
                inputControl.BringToFront();
                InputFields.Add(inputControl);
            }
        }

        
        private bool IsColumnNumericDirectly(string[][] rawData, int columnIndex)
        {
            if (rawData == null || rawData.Length == 0)
                return true; // По умолчанию считаем числовым

            int numericCount = 0;
            int totalCount = 0;
            int samplesToCheck = Math.Min(100, rawData.Length); 

            for (int i = 0; i < samplesToCheck; i++)
            {
                if (columnIndex < rawData[i].Length)
                {
                    string value = rawData[i][columnIndex].Trim();
                    if (!string.IsNullOrEmpty(value))
                    {
                        totalCount++;
                        // Проверяем, можно ли преобразовать в число
                        if (double.TryParse(value, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out _))
                        {
                            numericCount++;
                        }
                    }
                }
            }

            return totalCount > 0 && (numericCount * 100.0 / totalCount) > 70;
        }

        private List<string> GetUniqueValuesFromRawData(string[][] rawData, int columnIndex)
        {
            var uniqueValues = new HashSet<string>();

            if (rawData == null || columnIndex < 0)
                return new List<string>();

            int maxRows = Math.Min(1000, rawData.Length);

            for (int i = 0; i < maxRows; i++)
            {
                if (columnIndex < rawData[i].Length)
                {
                    string value = rawData[i][columnIndex].Trim();
                    if (!string.IsNullOrEmpty(value))
                    {
                        uniqueValues.Add(value);
                    }
                }
            }

            return uniqueValues.ToList();
        }

        public void RefreshInputFields()
        {
            if (SelectedFeatureIndices.Count > 0 && !string.IsNullOrEmpty(CurrentFilePath))
            {
                CreateInputFields();
            }
        }
        public void UpdateInputFields()
        {
            if (TabPage != null && TabPage.Visible)
            {
                ClearInputFields();

                // Обновляем только если есть выбранные признаки
                if (SelectedFeatureIndices.Count > 0 && !string.IsNullOrEmpty(CurrentFilePath))
                {
                    CreateInputFields();
                }
            }
        }
        protected virtual void ClearInputFields()
        {
            // Удаляем старые поля
            foreach (var field in InputFields)
            {
                if (TabPage.Controls.Contains(field))
                    TabPage.Controls.Remove(field);
            }
            InputFields.Clear();

            // Удаляем старые лейблы
            var labelsToRemove = TabPage.Controls.OfType<Label>()
                .Where(l => l.Name?.Contains("inputLabel_") == true)
                .ToList();
            foreach (var label in labelsToRemove)
            {
                TabPage.Controls.Remove(label);
            }
        }

        protected virtual void ValidateInputFields()
        {
            for (int i = 0; i < InputFields.Count; i++)
            {
                var control = InputFields[i];
                if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                    throw new Exception($"Заполните поле для признака {ColumnNames[SelectedFeatureIndices[i]]}");

                if (control is ComboBox comboBox && comboBox.SelectedItem == null)
                    throw new Exception($"Выберите значение для признака {ColumnNames[SelectedFeatureIndices[i]]}");
            }
        }

        protected virtual double[] CollectFeaturesFromInputFields()
        {
            var features = new double[InputFields.Count];

            for (int i = 0; i < InputFields.Count; i++)
            {
                var control = InputFields[i];
                int featureIndex = SelectedFeatureIndices[i];

                if (control is TextBox textBox)
                {
                    if (!double.TryParse(textBox.Text, out double value))
                        throw new Exception($"Некорректное числовое значение");

                    features[i] = value;
                }
                else if (control is ComboBox comboBox)
                {
                    string textValue = comboBox.SelectedItem.ToString();
                    features[i] = Preprocessor.ConvertToNumeric(textValue, featureIndex);
                }
            }

            return features;
        }

        protected virtual void DisplayPrediction(double prediction, double[] features)
        {
            string predictionDisplay;
            if (Pipeline.Config.ProblemType == ProblemType.Classification)
            {
                int targetIndex = TargetComboBox.SelectedIndex;
                string predictedClass = Pipeline.GetCategoryName(targetIndex, Math.Round(prediction));
                predictionDisplay = predictedClass;
            }
            else
            {
                predictionDisplay = prediction.ToString("F4");
            }

            string resultText = ResultTextBox.Text;
            resultText += $"\n\n=== ПРЕДСКАЗАНИЕ ===\n";
            resultText += $"Результат: {predictionDisplay}";

            ResultTextBox.Text = resultText;
            ResultTextBox.ScrollToCaret();
        }

        protected virtual void ShowTrainingResults(NormalizationType normType = NormalizationType.None)
        {
            if (Pipeline == null || Pipeline.Result == null)
            {
                ResultTextBox.Text = "Модель не обучена";
                return;
            }

            var result = Pipeline.Result;

            string normTypeName = normType switch
            {
                NormalizationType.None => "Без нормирования",
                NormalizationType.ZScore => "Z-Score (статистическое)",
                NormalizationType.MinMax => "Min-Max (линейное)",
                NormalizationType.LogScale => "Логарифмическое",
                _ => "Неизвестно"
            };

            ResultTextBox.Text =
                "=== РЕЗУЛЬТАТЫ ОБУЧЕНИЯ ===\n\n" +
                $"Алгоритм: {GetAlgorithmType().Name} (k={KControl.Value})\n" +
                $"Нормирование: {normTypeName}\n" +
                $"Точность: {result.Accuracy:P2}\n\n" +
                "=== ВЫБРАННЫЕ ПРИЗНАКИ ===\n" +
                string.Join("\n", SelectedFeatureIndices.Select(i => $"• {ColumnNames[i]}")) + "\n\n" +
                "=== ЦЕЛЕВОЙ ПРИЗНАК ===\n" +
                $"• {TargetComboBox.SelectedItem}";
        }
    }
}