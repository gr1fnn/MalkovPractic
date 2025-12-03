using Algorithms.Algorithms;
using Algorithms.Core;
using Algorithms.Pipelines;
using Algorithms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyForm
{
    public partial class MainForm : Form
    {
        private Pipeline currentPipeline;
        private string currentFilePath;
        private List<Control> inputFields = new List<Control>();
        private DefaultDataPreprocessor preprocessor;
        private List<int> selectedFeatureIndices = new List<int>();
        private string[] columnNames;

        public MainForm()
        {
            InitializeComponent();

            problemTypeComboBox.Items.Add("Классификация");
            problemTypeComboBox.SelectedIndex = 0;

            numKNN.Minimum = 1;
            numKNN.Maximum = 60;
            numKNN.Value = 3;

            PredictButton.Enabled = false;
        }

        private void UpdateFeatureSelectionControls(string[] columnNames)
        {
            this.columnNames = columnNames;

            var selectedItemsKNN = new List<string>();
            var selectedItemsWeightKNN = new List<string>();
            var selectedItemsSTOL = new List<string>();
            var selectedItemsSVM = new List<string>();
            var selectedItemsNadaraya = new List<string>();

            for (int i = 0; i < featuresCheckedListBox.Items.Count; i++)
            {
                if (featuresCheckedListBox.GetItemChecked(i))
                {
                    selectedItemsKNN.Add(featuresCheckedListBox.Items[i].ToString());
                }
            }

            // Сохраняем текущий выбор для Weighted KNN вкладки
            for (int i = 0; i < featuresCheckedListBox1.Items.Count; i++)
            {
                if (featuresCheckedListBox1.GetItemChecked(i))
                {
                    selectedItemsWeightKNN.Add(featuresCheckedListBox1.Items[i].ToString());
                }
            }

            for (int i = 0; i < featuresCheckedListBox2.Items.Count; i++)
            {
                if (featuresCheckedListBox2.GetItemChecked(i))
                {
                    selectedItemsSTOL.Add(featuresCheckedListBox2.Items[i].ToString());
                }
            }

            for (int i = 0; i < featuresCheckedListBox3.Items.Count; i++)
            {
                if (featuresCheckedListBox3.GetItemChecked(i))
                {
                    selectedItemsSVM.Add(featuresCheckedListBox3.Items[i].ToString());
                }
            }

            for (int i = 0; i < featuresCheckedListBox4.Items.Count; i++)
            {
                if (featuresCheckedListBox4.GetItemChecked(i))
                {
                    selectedItemsNadaraya.Add(featuresCheckedListBox4.Items[i].ToString());
                }
            }

            featuresCheckedListBox.Items.Clear();
            foreach (string columnName in columnNames)
            {
                featuresCheckedListBox.Items.Add(columnName, selectedItemsKNN.Contains(columnName));
            }

            targetComboBox.Items.Clear();
            foreach (string columnName in columnNames)
            {
                targetComboBox.Items.Add(columnName);
            }
            if (targetComboBox.Items.Count > 0)
                targetComboBox.SelectedIndex = 0;

            featuresCheckedListBox1.Items.Clear();
            foreach (string columnName in columnNames)
            {
                featuresCheckedListBox1.Items.Add(columnName, selectedItemsWeightKNN.Contains(columnName));
            }

            problemTypeComboBoxWeightKNN.Items.Clear();
            foreach (string columnName in columnNames)
            {
                problemTypeComboBoxWeightKNN.Items.Add(columnName);
            }
            if (problemTypeComboBoxWeightKNN.Items.Count > 0)
                problemTypeComboBoxWeightKNN.SelectedIndex = 0;

            if (targetComboBoxWeightKNN.Items.Count == 0)
            {
                targetComboBoxWeightKNN.Items.Add("Классификация");
                targetComboBoxWeightKNN.SelectedIndex = 0;
            }

            featuresCheckedListBox2.Items.Clear();
            foreach (string columnName in columnNames)
            {
                featuresCheckedListBox2.Items.Add(columnName, selectedItemsSTOL.Contains(columnName));
            }

            problemTypeComboBoxSTOL.Items.Clear();
            foreach (string columnName in columnNames)
            {
                problemTypeComboBoxSTOL.Items.Add(columnName);
            }
            if (problemTypeComboBoxSTOL.Items.Count > 0)
                problemTypeComboBoxSTOL.SelectedIndex = 0;

            if (targetComboBoxSTOL.Items.Count == 0)
            {
                targetComboBoxSTOL.Items.Add("Классификация");
                targetComboBoxSTOL.SelectedIndex = 0;
            }

            featuresCheckedListBox3.Items.Clear();
            foreach (string columnName in columnNames)
            {
                featuresCheckedListBox3.Items.Add(columnName, selectedItemsSVM.Contains(columnName));
            }

            problemTypeComboBoxSVM.Items.Clear();
            foreach (string columnName in columnNames)
            {
                problemTypeComboBoxSVM.Items.Add(columnName);
            }
            if (problemTypeComboBoxSVM.Items.Count > 0)
                problemTypeComboBoxSVM.SelectedIndex = 0;

            if (targetComboBoxSVM.Items.Count == 0)
            {
                targetComboBoxSVM.Items.Add("Классификация");
                targetComboBoxSVM.SelectedIndex = 0;
            }

            featuresCheckedListBox4.Items.Clear();
            foreach (string columnName in columnNames)
            {
                featuresCheckedListBox4.Items.Add(columnName, selectedItemsNadaraya.Contains(columnName));
            }

            problemTypeComboBoxNadaraya.Items.Clear();
            foreach (string columnName in columnNames)
            {
                problemTypeComboBoxNadaraya.Items.Add(columnName);
            }
            if (problemTypeComboBoxNadaraya.Items.Count > 0)
                problemTypeComboBoxNadaraya.SelectedIndex = 0;

            if (targetComboBoxNadaraya.Items.Count == 0)
            {
                targetComboBoxNadaraya.Items.Add("Классификация");
                targetComboBoxNadaraya.SelectedIndex = 0;
            }
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Сначала выберите файл с данными", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                progressBar1.Value = 0;
                progressBar1.Style = ProgressBarStyle.Marquee;
                UpdateStatus("Обучение модели...");

                // Получаем выбранные признаки
                selectedFeatureIndices.Clear();
                for (int i = 0; i < featuresCheckedListBox.Items.Count; i++)
                {
                    if (featuresCheckedListBox.GetItemChecked(i) &&
                        featuresCheckedListBox.Items[i].ToString() != targetComboBox.SelectedItem.ToString())
                    {
                        selectedFeatureIndices.Add(i);
                    }
                }

                if (selectedFeatureIndices.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы один признак", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем индекс целевой переменной
                int targetIndex = targetComboBox.SelectedIndex;
                if (targetIndex < 0)
                {
                    MessageBox.Show("Выберите целевую переменную", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Определяем тип задачи
                var problemType = problemTypeComboBox.SelectedIndex == 0 ?
                    ProblemType.Classification : ProblemType.Regression;

                // Создаем конфигурацию для Pipeline
                var algorithmParams = new Dictionary<string, object>
                {
                    { "K", (int)numKNN.Value },
                    { "DistanceMetric", DistanceMetric.Euclidean }
                };

                var datasetConfig = new DatasetConfig
                {
                    Name = Path.GetFileNameWithoutExtension(currentFilePath),
                    HasHeader = true,
                    FeatureColumns = selectedFeatureIndices.ToArray(),
                    LabelColumn = targetIndex,
                    ProblemType = problemType,
                    AlgorithmType = typeof(KNN),
                    AlgorithmParameters = algorithmParams
                };

                // Создаем и запускаем Pipeline
                currentPipeline = new Pipeline(datasetConfig);
                currentPipeline.LoadAndTrain(currentFilePath);

                // Получаем препроцессор из Pipeline
                preprocessor = currentPipeline.Preprocessor;

                // Создаем поля для ввода признаков
                CreateInputFields(selectedFeatureIndices.Count);

                // Показываем результаты
                ShowTrainingResults();

                // Активируем кнопку предсказания
                PredictButton.Enabled = true;

                UpdateStatus($"Модель обучена! Использовано признаков: {selectedFeatureIndices.Count}, k = {numKNN.Value}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обучении модели: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Ошибка при обучении");
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 100;
            }
        }

   

        private void ShowTrainingResults()
        {
            try
            {
                if (currentPipeline == null || currentPipeline.Result == null)
                {
                    ResultTextBox.Text = "Модель не обучена";
                    return;
                }

                var result = currentPipeline.Result;

                ResultTextBox.Text =
                    "=== РЕЗУЛЬТАТЫ ОБУЧЕНИЯ ===\n\n" +
                    $"Алгоритм: KNN (k={numKNN.Value})\n" +
                    $"Точность: {result.Accuracy:P2}\n\n" +
                    "=== ВЫБРАННЫЕ ПРИЗНАКИ ===\n" +
                    string.Join("\n", selectedFeatureIndices.Select(i => $"• {columnNames[i]}")) + "\n\n" +
                    "=== ЦЕЛЕВОЙ ПРИЗНАК ===\n" +
                    $"• {targetComboBox.SelectedItem}";

                ResultTextBox.Visible = true;

            }
            catch (Exception ex)
            {
                ResultTextBox.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void CreateInputFields(int count)
        {
            // Удаляем старые поля
            foreach (var field in inputFields)
            {
                if (TabKnn.Controls.Contains(field))
                    TabKnn.Controls.Remove(field);
            }
            inputFields.Clear();

            // Удаляем старые лейблы
            var labelsToRemove = TabKnn.Controls.OfType<Label>()
                .Where(l => l.Text.StartsWith("Признак") || l.Name.StartsWith("inputLabel_"))
                .ToList();
            foreach (var label in labelsToRemove)
            {
                TabKnn.Controls.Remove(label);
            }

            if (count <= 0) return;

            int startX = 12;
            int startY = problemTypeComboBox.Bottom + 10;

            // Загружаем данные для анализа
            var rawData = DataLoader.LoadCSV(currentFilePath, true);

            for (int i = 0; i < count; i++)
            {
                // Получаем имя признака и его индекс
                int featureIndex = selectedFeatureIndices[i];
                string featureName = columnNames[featureIndex];

                // Используем препроцессор для определения типа признака
                bool isNumeric = preprocessor.IsColumnNumeric(rawData, featureIndex);

                // Label для поля ввода
                var label = new Label
                {
                    Text = $"{featureName}:",
                    Location = new Point(startX, startY + i * 30),
                    Size = new Size(100, 20),
                    Name = $"inputLabel_{i}",
                    Font = new Font("Microsoft Sans Serif", 8.25f),
                    Tag = isNumeric ? "numeric" : "text"
                };
                TabKnn.Controls.Add(label);
                label.BringToFront();

                Control inputControl;

                if (isNumeric)
                {
                    // Для числовых признаков - TextBox
                    var textBox = new TextBox
                    {
                        Location = new Point(startX + 105, startY + i * 30),
                        Size = new Size(100, 20),
                        Name = $"inputField_{i}",
                        Font = new Font("Microsoft Sans Serif", 8.25f),
                        Tag = featureIndex
                    };
                    inputControl = textBox;
                }
                else
                {
                    // Для категориальных признаков - ComboBox с доступными значениями
                    var comboBox = new ComboBox
                    {
                        Location = new Point(startX + 105, startY + i * 30),
                        Size = new Size(100, 20),
                        Name = $"inputField_{i}",
                        Font = new Font("Microsoft Sans Serif", 8.25f),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Tag = featureIndex
                    };

                    // Получаем уникальные значения из маппинга препроцессора
                    var mapping = preprocessor.GetColumnMapping(featureIndex);
                    foreach (var kvp in mapping.OrderBy(kvp => kvp.Value))
                    {
                        comboBox.Items.Add(kvp.Key);
                    }
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;

                    inputControl = comboBox;
                }

                TabKnn.Controls.Add(inputControl);
                inputControl.BringToFront();
                inputFields.Add(inputControl);
            }
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            if (currentPipeline == null || preprocessor == null)
            {
                MessageBox.Show("Сначала обучите модель", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Проверяем, все ли поля заполнены
                bool allFieldsFilled = true;
                int emptyFieldIndex = -1;

                for (int i = 0; i < inputFields.Count; i++)
                {
                    var control = inputFields[i];
                    if (control is TextBox textBox)
                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            allFieldsFilled = false;
                            emptyFieldIndex = i;
                            break;
                        }
                    }
                    else if (control is ComboBox comboBox)
                    {
                        if (comboBox.SelectedItem == null)
                        {
                            allFieldsFilled = false;
                            emptyFieldIndex = i;
                            break;
                        }
                    }
                }

                if (!allFieldsFilled)
                {
                    string featureName = columnNames[selectedFeatureIndices[emptyFieldIndex]];
                    MessageBox.Show($"Выберите значение для признака '{featureName}'", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    inputFields[emptyFieldIndex].Focus();
                    return;
                }

                // Собираем значения из полей ввода
                double[] features = new double[inputFields.Count];
                List<string> inputValues = new List<string>();

                for (int i = 0; i < inputFields.Count; i++)
                {
                    var control = inputFields[i];
                    int featureIndex = selectedFeatureIndices[i];
                    string featureName = columnNames[featureIndex];

                    if (control is TextBox textBox)
                    {
                        // Числовой признак
                        if (!double.TryParse(textBox.Text, out double value))
                        {
                            MessageBox.Show($"Некорректное значение в поле '{featureName}'. Введите число.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox.Focus();
                            textBox.SelectAll();
                            return;
                        }
                        features[i] = value;
                        inputValues.Add($"{featureName}: {value}");
                    }
                    else if (control is ComboBox comboBox)
                    {
                        // Категориальный признак - используем сохраненное кодирование
                        string textValue = comboBox.SelectedItem.ToString();

                        // Получаем кодирование из препроцессора
                        var mapping = preprocessor.GetColumnMapping(featureIndex);
                        double numericValue = mapping.ContainsKey(textValue) ? mapping[textValue] : 0;

                        features[i] = numericValue;
                        inputValues.Add($"{featureName}: {textValue}");
                    }
                }

                // Делаем предсказание
                double prediction = currentPipeline.Predict(features);

                // Получаем название класса
                string predictionDisplay;
                if (currentPipeline.Config.ProblemType == ProblemType.Classification)
                {
                    int targetIndex = targetComboBox.SelectedIndex;
                    string predictedClass = currentPipeline.GetCategoryName(targetIndex, Math.Round(prediction));
                    predictionDisplay = $"{predictedClass} )";
                }
                else
                {
                    predictionDisplay = prediction.ToString("F4");
                }

                // Обновляем результат
                string resultText = ResultTextBox.Text;
                resultText += $"\n\n=== ПРЕДСКАЗАНИЕ ===\n";
                resultText += $"Входные данные:\n{string.Join("\n", inputValues)}\n";
                resultText += $"Результат: {predictionDisplay}";

                ResultTextBox.Text = resultText;
                ResultTextBox.ScrollToCaret();

                UpdateStatus("Предсказание выполнено успешно");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка предсказания: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Ошибка предсказания");
            }
        }

        private void UpdateStatus(string message)
        {
            if (progressBar1Label != null && !progressBar1Label.IsDisposed)
            {
                progressBar1Label.Text = $"Статус: {message}";
                progressBar1Label.Refresh();
            }
        }

        private DataTable ConvertToDataTable(string[][] data, string[] columnNames)
        {
            var table = new DataTable();

            foreach (string columnName in columnNames)
            {
                table.Columns.Add(columnName);
            }

            for (int i = 0; i < data.Length; i++)
            {
                if (i < data.Length && data[i].Length == columnNames.Length)
                {
                    table.Rows.Add(data[i]);
                }
            }

            return table;
        }

        private void chosefileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите CSV файл для обучения";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentFilePath = openFileDialog.FileName;

                        var rawData = DataLoader.LoadCSV(currentFilePath, true);
                        columnNames = DataLoader.GetColumnNames(currentFilePath);

                        dataGridView1.DataSource = ConvertToDataTable(rawData, columnNames);
                        dataGridView2.DataSource = ConvertToDataTable(rawData, columnNames);
                        dataGridView3.DataSource = ConvertToDataTable(rawData, columnNames);
                        dataGridView4.DataSource = ConvertToDataTable(rawData, columnNames);
                        dataGridView5.DataSource = ConvertToDataTable(rawData, columnNames);

                        UpdateFeatureSelectionControls(columnNames);

                        UpdateStatus($"Файл загружен: {Path.GetFileName(currentFilePath)} | Строк: {rawData.Length}, Колонок: {columnNames.Length}");

                        PredictButton.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}