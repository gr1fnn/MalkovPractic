using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using Algorithms.Pipelines;
using Algorithms.Core;
using Algorithms.Preprocessing;

namespace MyForm
{
    public partial class MainForm : Form
    {
        private Pipeline currentPipeline;
        private string currentFilePath;
        private List<TextBox> inputFields = new List<TextBox>();

        private string originalNumBandwidthLabelText;
        private string originalNumLearningRateLabelText;
        private string originalNumEpochsLabelText;

        public MainForm()
        {
            InitializeComponent();
            ViewAllElements(false);
            InitializeComboBoxes();
            SaveOriginalLabels();
            UpdateAlgorithmParametersVisibility();
        }

        private void SaveOriginalLabels()
        {
            originalNumBandwidthLabelText = numBandwidthLabel.Text;
            originalNumLearningRateLabelText = numLearningRateLabel.Text;
            originalNumEpochsLabelText = numEpochsLabel.Text;
        }

        private void InitializeComboBoxes()
        {
            TypeCombobox.Items.AddRange(new string[] { "Классификация" });
            TypeCombobox.SelectedIndex = 0;

            algorytmComboBox.Items.AddRange(new string[] { "KNN", "Взвешенный KNN", "Надарая-Ватсон", "SVM", "STOL" });
            algorytmComboBox.SelectedIndex = 0;
            algorytmComboBox.SelectedIndexChanged += AlgorytmComboBox_SelectedIndexChanged;

            MetricCombobox.Items.AddRange(new string[] { "Евклидова", "Манхэттен", "Косинусная" });
            MetricCombobox.SelectedIndex = 0;

            TypeKermel.Items.AddRange(new string[] { "Гауссово", "Линейное", "Епанечникова" });
            TypeKermel.SelectedIndex = 0;

            numK.Value = 3;
            numBandwidth.Value = 1.0m;
            numLearningRate.Value = 0.001m;
            numEpochs.Value = 1000;
        }

        private void AlgorytmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                UpdateAlgorithmParametersVisibility();
            }
        }

        private void UpdateAlgorithmParametersVisibility()
        {
            string algorithm = algorytmComboBox.SelectedItem?.ToString() ?? "KNN";

            RestoreOriginalLabels();
            ResetParameterValues();

            switch (algorithm)
            {
                case "KNN":
                    ShowKNNParameters();
                    break;
                case "Взвешенный KNN":
                    ShowKNNParameters();
                    break;
                case "Надарая-Ватсон":
                    ShowNadarayaWatsonParameters();
                    break;
                case "SVM":
                    ShowSVMParameters();
                    break;
                case "STOL":
                    ShowSTOLParameters();
                    break;
            }
        }

        private void RestoreOriginalLabels()
        {
            numBandwidthLabel.Text = originalNumBandwidthLabelText;
            numLearningRateLabel.Text = originalNumLearningRateLabelText;
            numEpochsLabel.Text = originalNumEpochsLabelText;
        }

        private void ResetParameterValues()
        {
            numK.Minimum = 1;
            numK.Maximum = 50;
            numK.DecimalPlaces = 0;

            numBandwidth.Minimum = 0;
            numBandwidth.Maximum = 10;
            numBandwidth.DecimalPlaces = 4;

            numLearningRate.Minimum = 0.0001m;
            numLearningRate.Maximum = 1;
            numLearningRate.DecimalPlaces = 4;

            numEpochs.Minimum = 100;
            numEpochs.Maximum = 10000;
            numEpochs.DecimalPlaces = 0;

            numK.Value = 3;
            numBandwidth.Value = 1.0m;
            numLearningRate.Value = 0.001m;
            numEpochs.Value = 1000;
        }

        private void ShowKNNParameters()
        {
            // Показываем только необходимые параметры для KNN
            numKLabel.Visible = true;
            numKLabel.Text = "Количество соседей";
            numK.Visible = true;
            MetricComboboxLabel.Visible = true;
            MetricCombobox.Visible = true;

            // Скрываем остальные
            numBandwidthLabel.Visible = false;
            numBandwidth.Visible = false;
            numLearningRateLabel.Visible = false;
            numLearningRate.Visible = false;
            numEpochsLabel.Visible = false;
            numEpochs.Visible = false;
            TypeKermelLabel.Visible = false;
            TypeKermel.Visible = false;
        }

        private void ShowNadarayaWatsonParameters()
        {
            // Показываем параметры для Надарая-Ватсон
            TypeKermelLabel.Visible = true;
            TypeKermel.Visible = true;
            numBandwidthLabel.Visible = true;
            numBandwidth.Visible = true;

            // Скрываем остальные
            numKLabel.Visible = false;
            numK.Visible = false;
            MetricComboboxLabel.Visible = false;
            MetricCombobox.Visible = false;
            numLearningRateLabel.Visible = false;
            numLearningRate.Visible = false;
            numEpochsLabel.Visible = false;
            numEpochs.Visible = false;
        }

        private void ShowSVMParameters()
        {
            // Показываем все параметры SVM
            numKLabel.Visible = true;
            numKLabel.Text = "Параметр штрафа ";
            numK.Visible = true;
            numK.Minimum = 1;
            numK.Maximum = 50;
            numK.DecimalPlaces = 0;
            numK.Value = 1;

            numBandwidthLabel.Text = "Лямбда ";
            numBandwidthLabel.Visible = true;
            numBandwidth.Visible = true;
            numBandwidth.Minimum = 0.01m;
            numBandwidth.Maximum = 100;
            numBandwidth.DecimalPlaces = 2;
            numBandwidth.Value = 1.0m;

            numLearningRateLabel.Text = "Скорость обучения:";
            numLearningRateLabel.Visible = true;
            numLearningRate.Visible = true;
            numLearningRate.Minimum = 0.0001m;
            numLearningRate.Maximum = 1;
            numLearningRate.DecimalPlaces = 4;
            numLearningRate.Value = 0.001m;

            numEpochsLabel.Text = "Количество эпох:";
            numEpochsLabel.Visible = true;
            numEpochs.Visible = true;
            numEpochs.Minimum = 100;
            numEpochs.Maximum = 10000;
            numEpochs.DecimalPlaces = 0;
            numEpochs.Value = 1000;

            // Скрываем ненужные
            MetricComboboxLabel.Visible = false;
            MetricCombobox.Visible = false;
            TypeKermelLabel.Visible = false;
            TypeKermel.Visible = false;
        }

        private void ShowSTOLParameters()
        {
            // Показываем параметры для STOL
            numKLabel.Visible = true;
            numK.Visible = true;
            numK.Minimum = 1;
            numK.Maximum = 50;
            numK.DecimalPlaces = 0;
            numK.Value = 3;

            numBandwidthLabel.Text = "Порог уверенности:";
            numBandwidthLabel.Visible = true;
            numBandwidth.Visible = true;
            numBandwidth.Minimum = 0;
            numBandwidth.Maximum = 1;
            numBandwidth.DecimalPlaces = 2;
            numBandwidth.Value = 0.7m;

            numLearningRateLabel.Text = "Макс. samples:";
            numLearningRateLabel.Visible = true;
            numLearningRate.Visible = true;
            numLearningRate.Minimum = 100;
            numLearningRate.Maximum = 10000;
            numLearningRate.DecimalPlaces = 0;
            numLearningRate.Value = 1000;

            MetricComboboxLabel.Visible = true;
            MetricCombobox.Visible = true;

            numEpochsLabel.Visible = false;
            numEpochs.Visible = false;
            TypeKermelLabel.Visible = false;
            TypeKermel.Visible = false;
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
                        var columnNames = DataLoader.GetColumnNames(currentFilePath);

                        dataGridView1.DataSource = ConvertToDataTable(rawData, columnNames);

                        checkedListBox1.Items.Clear();
                        foreach (string columnName in columnNames)
                        {
                            checkedListBox1.Items.Add(columnName, true);
                        }

                        ViewAllElements(true);
                        UpdateAlgorithmParametersVisibility();

                        UpdateStatus($"Файл загружен: {Path.GetFileName(currentFilePath)} | Строк: {rawData.Length}, Колонок: {columnNames.Length}");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void StudyModel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Сначала выберите файл для обучения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один признак", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                StudyModelButton.Enabled = false;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.Visible = true;
                UpdateStatus("Обучение модели...");

                var config = CreateDatasetConfig();

                var task = System.Threading.Tasks.Task.Run(() => TrainModel(config));
                task.ContinueWith(t =>
                {
                    this.Invoke(new Action(() =>
                    {
                        if (t.IsFaulted)
                        {
                            UpdateStatus($"Ошибка обучения: {t.Exception?.Message}");
                            MessageBox.Show($"Ошибка обучения: {t.Exception?.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            UpdateStatus("Обучение завершено успешно!");
                            CreateTestInputs();
                        }

                        StudyModelButton.Enabled = true;
                        progressBar1.Visible = false;
                    }));
                });
            }
            catch (Exception ex)
            {
                UpdateStatus($"Ошибка: {ex.Message}");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                StudyModelButton.Enabled = true;
                progressBar1.Visible = false;
            }
        }

        private void TrainModel(DatasetConfig config)
        {
            currentPipeline = new Pipeline(config);
            currentPipeline.LoadAndTrain(currentFilePath);

            this.Invoke(new Action(UpdateTrainingResults));
        }

        private DatasetConfig CreateDatasetConfig()
        {
            var config = new DatasetConfig
            {
                Name = "UI Configuration",
                HasHeader = true,
                FeatureColumns = checkedListBox1.CheckedIndices.Cast<int>().ToArray(),
                LabelColumn = checkedListBox1.Items.Count - 1,
                ProblemType = TypeCombobox.SelectedIndex == 0 ? ProblemType.Classification : ProblemType.Regression
            };

            string algorithm = algorytmComboBox.SelectedItem?.ToString() ?? "KNN";
            config.AlgorithmType = algorithm switch
            {
                "KNN" => typeof(Algorithms.Algorithms.KNN),
                "Взвешенный KNN" => typeof(Algorithms.Algorithms.WeightedKNN),
                "Надарая-Ватсон" => typeof(Algorithms.Algorithms.NadarayaWatson),
                "SVM" => typeof(Algorithms.Algorithms.SVM),
                "STOL" => typeof(Algorithms.Algorithms.STOL),
                _ => typeof(Algorithms.Algorithms.KNN)
            };

            config.AlgorithmParameters = new Dictionary<string, object>();

            if (algorithm.Contains("KNN"))
            {
                config.AlgorithmParameters["K"] = (int)numK.Value;
                config.AlgorithmParameters["DistanceMetric"] = (DistanceMetric)MetricCombobox.SelectedIndex;
            }
            else if (algorithm == "Надарая-Ватсон")
            {
                config.AlgorithmParameters["Bandwidth"] = (double)numBandwidth.Value;
                config.AlgorithmParameters["KernelType"] = (KernelType)TypeKermel.SelectedIndex;
            }
            else if (algorithm == "SVM")
            {
                config.AlgorithmParameters["LearningRate"] = (double)numLearningRate.Value;
                config.AlgorithmParameters["Epochs"] = (int)numEpochs.Value;
                config.AlgorithmParameters["Lambda"] = (double)numBandwidth.Value; // фиксированное значение
                config.AlgorithmParameters["C"] = (double)numK.Value; // используем numBandwidth для параметра C
            }
            else if (algorithm == "STOL")
            {
                config.AlgorithmParameters["K"] = (int)numK.Value;
                config.AlgorithmParameters["DistanceMetric"] = (DistanceMetric)MetricCombobox.SelectedIndex;
                config.AlgorithmParameters["ConfidenceThreshold"] = (double)numBandwidth.Value;
                config.AlgorithmParameters["MaxSamples"] = (int)numLearningRate.Value;
            }

            return config;
        }

        private void UpdateTrainingResults()
        {
            if (currentPipeline?.Result != null)
            {
                var result = currentPipeline.Result;
                string resultsText = $"=== РЕЗУЛЬТАТЫ ОБУЧЕНИЯ ===\n";
                resultsText += $"Время обучения: {result.TrainingTime:F2} сек\n";
                resultsText += $"Тип задачи: {currentPipeline.Config.ProblemType}\n";
                resultsText += $"Алгоритм: {currentPipeline.Config.AlgorithmType.Name}\n";

                if (currentPipeline.Config.ProblemType == ProblemType.Classification)
                {
                    resultsText += $"Точность: {result.Accuracy:P2}\n";
                }
                else
                {
                    resultsText += $"RMSE: {result.RMSE:F2}\n";
                    resultsText += $"MAE: {result.MAE:F2}\n";
                }

                ResultTextBox.Text = resultsText;
            }
        }

        // Остальные методы (CreateTestInputs, PredictButton_Click, UpdateStatus, ConvertToDataTable, ViewAllElements) 
        // остаются без изменений, так как они уже корректно работают
        private void CreateTestInputs()
        {
            // Очищаем предыдущие поля
            foreach (var field in inputFields)
            {
                if (field.Parent != null)
                {
                    field.Parent.Controls.Remove(field);
                }
            }
            inputFields.Clear();

            if (currentPipeline?.Data?.Features == null || currentPipeline.Data.Features.Length == 0)
                return;

            // Создаем поля для ввода тестовых данных
            int featureCount = currentPipeline.Data.Features[0].Length;
            int startY = ResultTextBox.Location.Y + ResultTextBox.Height + 20;
            int currentY = startY;

            // Создаем label для тестового раздела
            var testLabel = new Label()
            {
                Text = "ТЕСТОВОЕ ПРЕДСКАЗАНИЕ:",
                Location = new Point(ResultTextBox.Location.X, currentY),
                Size = new Size(200, 20),
                Font = new Font(this.Font, FontStyle.Bold)
            };
            this.Controls.Add(testLabel);
            currentY += 30;

            for (int i = 0; i < featureCount; i++)
            {
                var label = new Label()
                {
                    Text = $"Признак {i + 1}:",
                    Location = new Point(ResultTextBox.Location.X, currentY),
                    Size = new Size(80, 20)
                };

                var textBox = new TextBox()
                {
                    Location = new Point(ResultTextBox.Location.X + 85, currentY),
                    Size = new Size(100, 20),
                    Text = "0"
                };

                this.Controls.Add(label);
                this.Controls.Add(textBox);
                inputFields.Add(textBox);

                currentY += 30;
            }

            // Кнопка предсказания
            var predictButton = new Button()
            {
                Text = "ПРЕДСКАЗАТЬ",
                Location = new Point(ResultTextBox.Location.X, currentY),
                Size = new Size(100, 30)
            };
            predictButton.Click += PredictButton_Click;
            this.Controls.Add(predictButton);

            // Поле для результата
            var resultLabel = new Label()
            {
                Text = "Результат:",
                Location = new Point(ResultTextBox.Location.X + 110, currentY + 5),
                Size = new Size(60, 20)
            };
            this.Controls.Add(resultLabel);

            var predictionResult = new TextBox()
            {
                Location = new Point(ResultTextBox.Location.X + 175, currentY),
                Size = new Size(100, 25),
                ReadOnly = true,
                Font = new Font(this.Font, FontStyle.Bold),
                BackColor = Color.LightYellow
            };
            this.Controls.Add(predictionResult);
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            if (currentPipeline == null)
            {
                MessageBox.Show("Сначала обучите модель", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Собираем значения из полей ввода
                double[] features = new double[inputFields.Count];
                for (int i = 0; i < inputFields.Count; i++)
                {
                    if (!double.TryParse(inputFields[i].Text, out double value))
                    {
                        MessageBox.Show($"Некорректное значение в поле 'Признак {i + 1}'", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    features[i] = value;
                }

                // Делаем предсказание
                double prediction = currentPipeline.Predict(features);

                // Находим поле результата и обновляем его
                var resultTextBox = this.Controls.OfType<TextBox>()
                    .FirstOrDefault(tb => tb.Location.X == ResultTextBox.Location.X + 175 && tb.ReadOnly);

                if (resultTextBox != null)
                {
                    resultTextBox.Text = prediction.ToString("F4");

                    // Подкрашиваем в зависимости от типа задачи
                    if (currentPipeline.Config.ProblemType == ProblemType.Classification)
                    {
                        resultTextBox.BackColor = prediction == 1 ? Color.LightGreen : Color.LightCoral;
                    }
                    else
                    {
                        resultTextBox.BackColor = Color.LightBlue;
                    }
                }

                UpdateStatus("Предсказание выполнено успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка предсказания: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus(string message)
        {
            if (progressBar1Label != null)
            {
                progressBar1Label.Text = message;
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
                if (i < data.Length)
                {
                    table.Rows.Add(data[i]);
                }
            }

            return table;
        }

        private void ViewAllElements(bool visible)
        {
            if (!visible)
            {
                dataGridView1.Visible = false;
                ResultTextBox.Visible = false;
                progressBar1.Visible = false;
                checkedListBox1.Visible = false;
                algorytmComboBox.Visible = false;
                TypeCombobox.Visible = false;
                TypeKermel.Visible = false;
                MetricCombobox.Visible = false;
                numK.Visible = false;
                numEpochs.Visible = false;
                numBandwidth.Visible = false;
                numLearningRate.Visible = false;
                TypeCombobxLabel.Visible = false;
                algorytmComboBoxlabel.Visible = false;
                MetricComboboxLabel.Visible = false;
                numKLabel.Visible = false;
                numBandwidthLabel.Visible = false;
                numLearningRateLabel.Visible = false;
                numEpochsLabel.Visible = false;
                dataGridView1Label.Visible = false;
                progressBar1Label.Visible = false;
                TypeKermelLabel.Visible = false;
                ResultTextBoxLabel.Visible = false;
                checkedListBox1Label.Visible = false;
                StudyModelButton.Visible = false;
            }
            else
            {
                dataGridView1.Visible = true;
                ResultTextBox.Visible = true;
                progressBar1.Visible = true;
                checkedListBox1.Visible = true;
                algorytmComboBox.Visible = true;
                TypeCombobox.Visible = true;
                TypeKermel.Visible = true;
                MetricCombobox.Visible = true;
                numK.Visible = true;
                numEpochs.Visible = true;
                numBandwidth.Visible = true;
                numLearningRate.Visible = true;
                TypeCombobxLabel.Visible = true;
                algorytmComboBoxlabel.Visible = true;
                MetricComboboxLabel.Visible = true;
                numKLabel.Visible = true;
                numBandwidthLabel.Visible = true;
                numLearningRateLabel.Visible = true;
                numEpochsLabel.Visible = true;
                dataGridView1Label.Visible = true;
                progressBar1Label.Visible = true;
                TypeKermelLabel.Visible = true;
                ResultTextBoxLabel.Visible = true;
                checkedListBox1Label.Visible = true;
                StudyModelButton.Visible = true;

            }
        }
    }
}