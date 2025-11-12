using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using MLAlgorithms.Pipelines;
using MLAlgorithms.Core;
using MLAlgorithms.Preprocessing;

namespace MyForm
{
    public partial class MainForm : Form
    {
        private MLPipeline currentPipeline;
        private string currentFilePath;
        private List<TextBox> inputFields = new List<TextBox>();

        public MainForm()
        {
            InitializeComponent();
            ViewAllElements(false);
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Заполняем комбобоксы значениями
            TypeCombobox.Items.AddRange(new string[] { "Классификация", "Регрессия" });
            TypeCombobox.SelectedIndex = 0;

            algorytmComboBox.Items.AddRange(new string[] { "KNN", "Взвешенный KNN", "Надарая-Ватсон", "SVM" });
            algorytmComboBox.SelectedIndex = 0;

            MetricCombobox.Items.AddRange(new string[] { "Евклидова", "Манхэттен", "Косинусная" });
            MetricCombobox.SelectedIndex = 0;

            TypeKermel.Items.AddRange(new string[] { "Гауссово", "Линейное", "Епанечникова" });
            TypeKermel.SelectedIndex = 0;

            // Устанавливаем значения по умолчанию
            numK.Value = 3;
            numBandwidth.Value = 1.0m;
            numLearningRate.Value = 0.001m;
            numEpochs.Value = 1000;
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

                var task = System.Threading.Tasks.Task.Run(() => TrainModel());
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

        private void TrainModel()
        {
            var config = CreateDatasetConfig();

            currentPipeline = new MLPipeline(config);
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

            // Настройка алгоритма
            string algorithm = algorytmComboBox.SelectedItem?.ToString() ?? "KNN";
            config.AlgorithmType = algorithm switch
            {
                "KNN" => typeof(MLAlgorithms.Algorithms.KNN),
                "Взвешенный KNN" => typeof(MLAlgorithms.Algorithms.WeightedKNN),
                "Надарая-Ватсон" => typeof(MLAlgorithms.Algorithms.NadarayaWatson),
                "SVM" => typeof(MLAlgorithms.Algorithms.SVM),
                _ => typeof(MLAlgorithms.Algorithms.KNN)
            };

            // Параметры алгоритма
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
            if (label9 != null)
            {
                label9.Text = message;
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
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
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
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                StudyModelButton.Visible = true;
            }
        }
    }
}