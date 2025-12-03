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
        // Общие данные
        public string CurrentFilePath { get; private set; }
        public string[] ColumnNames { get; private set; }
        public DefaultDataPreprocessor SharedPreprocessor { get; set; }

        // Обработчики вкладок
        private Dictionary<TabPage, AlgorithmTabHandler> _tabHandlers;

        // Для графика
        private Button plotButton;
        private ComboBox xAxisComboBox;
        private ComboBox yAxisComboBox;

        public MainForm()
        {
            InitializeComponent();
            InitializePlotControls();
            InitializeTabHandlers();
        }

        private void InitializePlotControls()
        {
            // Кнопка графика
            plotButton = new Button
            {
                Text = "📊 Построить график",
                Location = new Point(chosefileButton.Right + 120, chosefileButton.Top),
                Size = new Size(130, 23),
                Font = new Font("Microsoft Sans Serif", 9),
                BackColor = Color.LightGreen
            };
            plotButton.Click += ShowPlotButton_Click;
            this.Controls.Add(plotButton);

            // Выбор оси X
            var xLabel = new Label
            {
                Text = "Ось X:",
                Location = new Point(chosefileButton.Left, chosefileButton.Bottom + 10),
                Size = new Size(40, 20),
                Font = new Font("Microsoft Sans Serif", 8)
            };
            this.Controls.Add(xLabel);

            xAxisComboBox = new ComboBox
            {
                Location = new Point(xLabel.Right, chosefileButton.Bottom + 8),
                Size = new Size(120, 21),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Microsoft Sans Serif", 8)
            };
            this.Controls.Add(xAxisComboBox);

            // Выбор оси Y
            var yLabel = new Label
            {
                Text = "Ось Y:",
                Location = new Point(xAxisComboBox.Right + 5, chosefileButton.Bottom + 10),
                Size = new Size(40, 20),
                Font = new Font("Microsoft Sans Serif", 8)
            };
            this.Controls.Add(yLabel);

            yAxisComboBox = new ComboBox
            {
                Location = new Point(yLabel.Right, chosefileButton.Bottom + 8),
                Size = new Size(120, 21),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Microsoft Sans Serif", 8)
            };
            this.Controls.Add(yAxisComboBox);
        }

        private void InitializeTabHandlers()
        {
            _tabHandlers = new Dictionary<TabPage, AlgorithmTabHandler>
            {
                [TabKnn] = AlgorithmTabHandlerFactory.CreateHandler(
                    AlgorithmType.KNN,
                    this, TabKnn,
                    featuresCheckedListBox, targetComboBox,
                    problemTypeComboBox, normalizationComboBoxKNN, // Передаем соответствующий ComboBox
                    numKNN, ResultTextBox,
                    PredictButton, progressBar1, progressBar1Label),

                [WeightKnnPage] = AlgorithmTabHandlerFactory.CreateHandler(
                    AlgorithmType.WeightedKNN,
                    this, WeightKnnPage,
                    featuresCheckedListBox1, targetComboBoxWeightKNN,
                    problemTypeComboBoxWeightKNN, normalizationComboBoxWeightKNN,
                    numWeightKNN, ResultTextBoxWeighKNN,
                    PredictButtonWeightKNN, progressBar2, label5),

                [STOLPage] = AlgorithmTabHandlerFactory.CreateHandler(
                    AlgorithmType.STOL,
                    this, STOLPage,
                    featuresCheckedListBox2, problemTypeComboBoxSTOL,
                    targetComboBoxSTOL, normalizationComboBoxSTOL,
                    numSTOL, ResultTextBoxSTOL,
                    PredictButtonSTOL, progressBar3, label17,
                    confidenceThresholdSTOL, maxSamplesSTOL),

                [SVMPage] = AlgorithmTabHandlerFactory.CreateHandler(
                    AlgorithmType.SVM,
                    this, SVMPage,
                    featuresCheckedListBox3, problemTypeComboBoxSVM,
                    targetComboBoxSVM, normalizationComboBoxSVM,
                    numSVM, ResultTextBoxSVM,
                    PredictButtonSVM, progressBar4, label29,
                    learningRateSVM, lambdaSVM, cParamSVM),

                [TabNadarayaWatsona] = AlgorithmTabHandlerFactory.CreateHandler(
                AlgorithmType.NadarayaWatson,
                this, TabNadarayaWatsona,
                featuresCheckedListBox4, targetComboBoxNadaraya, 
                problemTypeComboBoxNadaraya, normalizationComboBoxNadaraya,
                numNadaraya, ResultTextBoxNadaraya,
                PredictButtonNadaraya, progressBar5, label41,
                kernelType: kernelTypeNadaraya)
            };

            TrainButton.Click += (s, e) => _tabHandlers[TabKnn].Train();
            TrainButtonWeightKNN.Click += (s, e) => _tabHandlers[WeightKnnPage].Train();
            TrainButtonSTOL.Click += (s, e) => _tabHandlers[STOLPage].Train();
            TrainButtonSVM.Click += (s, e) => _tabHandlers[SVMPage].Train();
            TrainButtonNadaraya.Click += (s, e) => _tabHandlers[TabNadarayaWatsona].Train();

            PredictButton.Click += (s, e) => _tabHandlers[TabKnn].Predict();
            PredictButtonWeightKNN.Click += (s, e) => _tabHandlers[WeightKnnPage].Predict();
            PredictButtonSTOL.Click += (s, e) => _tabHandlers[STOLPage].Predict();
            PredictButtonSVM.Click += (s, e) => _tabHandlers[SVMPage].Predict();
            PredictButtonNadaraya.Click += (s, e) => _tabHandlers[TabNadarayaWatsona].Predict();
        }

        private void UpdateAllFeatureSelectionControls()
        {
            foreach (var handler in _tabHandlers.Values)
            {
                handler.UpdateFeatureSelection(ColumnNames);
            }

            // Обновляем списки осей для графика
            UpdateAxisComboBoxes();
        }

        private void UpdateAxisComboBoxes()
        {
            xAxisComboBox.Items.Clear();
            yAxisComboBox.Items.Clear();

            if (ColumnNames != null)
            {
                foreach (var columnName in ColumnNames)
                {
                    xAxisComboBox.Items.Add(columnName);
                    yAxisComboBox.Items.Add(columnName);
                }

                if (xAxisComboBox.Items.Count > 0) xAxisComboBox.SelectedIndex = 0;
                if (yAxisComboBox.Items.Count > 1) yAxisComboBox.SelectedIndex = 1;
                else if (yAxisComboBox.Items.Count > 0) yAxisComboBox.SelectedIndex = 0;
            }
        }

        private void chosefileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите CSV файл";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(openFileDialog.FileName);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            try
            {
                CurrentFilePath = filePath;
                var rawData = DataLoader.LoadCSV(filePath, true);
                ColumnNames = DataLoader.GetColumnNames(filePath);

                var table = ConvertToDataTable(rawData, ColumnNames);
                dataGridView1.DataSource = table;
                dataGridView2.DataSource = table;
                dataGridView3.DataSource = table;
                dataGridView4.DataSource = table;
                dataGridView5.DataSource = table;

                UpdateAllFeatureSelectionControls();
                SharedPreprocessor = null;

                // Сбрасываем кнопки предсказания
                foreach (var handler in _tabHandlers.Values)
                {
                    handler.PredictButton.Enabled = false;
                }

                // Сбрасываем нормализацию на всех вкладках на значение по умолчанию
                ResetNormalizationComboBoxes();

                UpdateStatus($"Файл загружен: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetNormalizationComboBoxes()
        {
            // Сбрасываем все ComboBox нормирования на значение по умолчанию
            var allComboBoxes = new[]
            {
                normalizationComboBoxKNN,
                normalizationComboBoxWeightKNN,
                normalizationComboBoxSTOL,
                normalizationComboBoxSVM,
                normalizationComboBoxNadaraya
            };

            foreach (var comboBox in allComboBoxes)
            {
                if (comboBox != null && comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0; // "Без нормирования" по умолчанию
                }
            }
        }

        private void UpdateStatus(string message)
        {
            if (progressBar1Label != null && !progressBar1Label.IsDisposed)
            {
                progressBar1Label.Text = $"Статус: {message}";
            }
        }

        private DataTable ConvertToDataTable(string[][] data, string[] columnNames)
        {
            var table = new DataTable();
            foreach (string columnName in columnNames)
            {
                table.Columns.Add(columnName);
            }
            foreach (var row in data)
            {
                if (row.Length == columnNames.Length)
                {
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private void ShowPlotButton_Click(object sender, EventArgs e)
        {
            if (xAxisComboBox.SelectedIndex < 0 || yAxisComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите признаки для осей X и Y", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (xAxisComboBox.SelectedIndex == yAxisComboBox.SelectedIndex)
            {
                MessageBox.Show("Выберите разные признаки для осей X и Y", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var currentTab = Tab.SelectedTab;
            if (_tabHandlers.TryGetValue(currentTab, out var handler))
            {
                if (handler.Pipeline == null)
                {
                    MessageBox.Show("Сначала обучите модель", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    // Создаем график с выбранными осями
                    var plotForm = new PlotForm(handler.Pipeline,
                        ColumnNames[xAxisComboBox.SelectedIndex],
                        ColumnNames[yAxisComboBox.SelectedIndex],
                        xAxisComboBox.SelectedIndex,
                        yAxisComboBox.SelectedIndex);
                    plotForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при построении графика: {ex.Message}", "Ошибка");
                }
            }
        }
    }
}

    // КЛАСС ДЛЯ ОКНА ГРАФИКА
    public class PlotForm : Form
    {
        private Pipeline _pipeline;
        private string _xAxisName;
        private string _yAxisName;
        private int _xAxisIndex;
        private int _yAxisIndex;
        private PictureBox _pictureBox;

        public PlotForm(Pipeline pipeline, string xAxisName, string yAxisName, int xAxisIndex, int yAxisIndex)
        {
            _pipeline = pipeline;
            _xAxisName = xAxisName;
            _yAxisName = yAxisName;
            _xAxisIndex = xAxisIndex;
            _yAxisIndex = yAxisIndex;

            InitializeForm();
            DrawPlot();
        }

        private void InitializeForm()
        {
            this.Text = $"График: {_xAxisName} vs {_yAxisName}";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(600, 500);

            // Панель для графика
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            _pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            panel.Controls.Add(_pictureBox);
            this.Controls.Add(panel);

            // Панель кнопок
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.LightGray
            };

            var saveButton = new Button
            {
                Text = "💾 Сохранить",
                Size = new Size(100, 30),
                Location = new Point(10, 5),
                Font = new Font("Microsoft Sans Serif", 9)
            };
            saveButton.Click += SaveButton_Click;

            var closeButton = new Button
            {
                Text = "Закрыть",
                Size = new Size(80, 30),
                Location = new Point(120, 5),
                Font = new Font("Microsoft Sans Serif", 9)
            };
            closeButton.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(saveButton);
            buttonPanel.Controls.Add(closeButton);
            this.Controls.Add(buttonPanel);
        }

        private void DrawPlot()
        {
            try
            {
                var image = CreatePlotImage();
                _pictureBox.Image = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании графика: {ex.Message}", "Ошибка");
            }
        }

        private Image CreatePlotImage()
        {
            var trainingData = _pipeline.TrainingData;
            if (trainingData == null || trainingData.Features.Length == 0)
                throw new Exception("Нет данных для построения графика");

            // Создаем изображение
            var bitmap = new Bitmap(750, 650);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // РАССЧИТЫВАЕМ ДИАПАЗОНЫ ДАННЫХ
                var (xValues, yValues) = ExtractAxisValues(trainingData);
                var xRange = CalculateRange(xValues);
                var yRange = CalculateRange(yValues);

                // ОБЛАСТЬ ГРАФИКА (с отступами для подписей)
                var plotRect = new RectangleF(80, 60, bitmap.Width - 120, bitmap.Height - 120);

                // ЗАГОЛОВОК
                DrawTitle(g, bitmap.Width);

                // РИСУЕМ СЕТКУ
                DrawGrid(g, plotRect, xRange, yRange);

                // РИСУЕМ ОСИ
                DrawAxes(g, plotRect, xRange, yRange);

                // РИСУЕМ ТОЧКИ ДАННЫХ
                DrawDataPoints(g, plotRect, trainingData, xRange, yRange);

                // РИСУЕМ ГРАНИЦУ РЕШЕНИЯ (если возможно)
                DrawDecisionBoundary(g, plotRect, xRange, yRange);

                // ЛЕГЕНДА
                DrawLegend(g, trainingData, bitmap.Width - 200, 70);
            }

            return bitmap;
        }

        private (List<double> xValues, List<double> yValues) ExtractAxisValues(Dataset data)
        {
            var xValues = new List<double>();
            var yValues = new List<double>();

            foreach (var features in data.Features)
            {
                if (_xAxisIndex < features.Length && _yAxisIndex < features.Length)
                {
                    xValues.Add(features[_xAxisIndex]);
                    yValues.Add(features[_yAxisIndex]);
                }
            }

            return (xValues, yValues);
        }

        private (double min, double max, double range) CalculateRange(List<double> values)
        {
            if (values.Count == 0) return (0, 1, 1);

            double min = values.Min();
            double max = values.Max();

            // Добавляем 10% отступа с каждой стороны
            double padding = Math.Max(0.1, (max - min) * 0.1);
            if (padding == 0) padding = 0.1;

            min -= padding;
            max += padding;

            return (min, max, max - min);
        }

        private void DrawTitle(Graphics g, int width)
        {
            string title = $"График распределения: {_xAxisName} vs {_yAxisName}";
            string subtitle = $"Модель: {_pipeline.Algorithm.GetType().Name} | Точность: {_pipeline.Result.Accuracy:P2}";

            using (var titleFont = new Font("Arial", 14, FontStyle.Bold))
            using (var subtitleFont = new Font("Arial", 10))
            {
                var titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.Black, new PointF((width - titleSize.Width) / 2, 15));

                var subtitleSize = g.MeasureString(subtitle, subtitleFont);
                g.DrawString(subtitle, subtitleFont, Brushes.Blue, new PointF((width - subtitleSize.Width) / 2, 40));
            }
        }

        private void DrawGrid(Graphics g, RectangleF plotRect, (double min, double max, double range) xRange,
            (double min, double max, double range) yRange)
        {
            using (var gridPen = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
            {
                // Вертикальные линии сетки
                for (int i = 1; i < 10; i++)
                {
                    float x = plotRect.Left + i * plotRect.Width / 10;
                    g.DrawLine(gridPen, x, plotRect.Top, x, plotRect.Bottom);
                }

                // Горизонтальные линии сетки
                for (int i = 1; i < 10; i++)
                {
                    float y = plotRect.Top + i * plotRect.Height / 10;
                    g.DrawLine(gridPen, plotRect.Left, y, plotRect.Right, y);
                }
            }
        }

        private void DrawAxes(Graphics g, RectangleF plotRect, (double min, double max, double range) xRange,
            (double min, double max, double range) yRange)
        {
            // Ось X
            using (var axisPen = new Pen(Color.Black, 2))
            {
                g.DrawLine(axisPen, plotRect.Left, plotRect.Bottom, plotRect.Right, plotRect.Bottom);
            }

            // Ось Y
            using (var axisPen = new Pen(Color.Black, 2))
            {
                g.DrawLine(axisPen, plotRect.Left, plotRect.Top, plotRect.Left, plotRect.Bottom);
            }

            // Метки на оси X
            using (var labelFont = new Font("Arial", 8))
            {
                for (int i = 0; i <= 10; i++)
                {
                    float x = plotRect.Left + i * plotRect.Width / 10;
                    double value = xRange.min + i * xRange.range / 10;

                    // Черточка
                    g.DrawLine(Pens.Black, x, plotRect.Bottom - 3, x, plotRect.Bottom + 3);

                    // Подпись значения
                    string label = value.ToString("F2");
                    var labelSize = g.MeasureString(label, labelFont);
                    g.DrawString(label, labelFont, Brushes.Black,
                        new PointF(x - labelSize.Width / 2, plotRect.Bottom + 5));
                }
            }

            // Метки на оси Y
            using (var labelFont = new Font("Arial", 8))
            {
                for (int i = 0; i <= 10; i++)
                {
                    float y = plotRect.Top + i * plotRect.Height / 10;
                    double value = yRange.max - i * yRange.range / 10;

                    // Черточка
                    g.DrawLine(Pens.Black, plotRect.Left - 3, y, plotRect.Left + 3, y);

                    // Подпись значения
                    string label = value.ToString("F2");
                    var labelSize = g.MeasureString(label, labelFont);
                    g.DrawString(label, labelFont, Brushes.Black,
                        new PointF(plotRect.Left - labelSize.Width - 5, y - labelSize.Height / 2));
                }
            }

            // Подписи осей
            using (var axisFont = new Font("Arial", 10, FontStyle.Bold))
            {
                var xLabelSize = g.MeasureString(_xAxisName, axisFont);
                g.DrawString(_xAxisName, axisFont, Brushes.Black,
                    new PointF(plotRect.Left + plotRect.Width / 2 - xLabelSize.Width / 2, plotRect.Bottom + 25));

                var yLabelSize = g.MeasureString(_yAxisName, axisFont);

                // Поворачиваем текст для оси Y
                var state = g.Save();
                g.TranslateTransform(30, plotRect.Top + plotRect.Height / 2);
                g.RotateTransform(-90);
                g.DrawString(_yAxisName, axisFont, Brushes.Black, new PointF(-yLabelSize.Height / 2, 0));
                g.Restore(state);
            }
        }

        private void DrawDataPoints(Graphics g, RectangleF plotRect, Dataset data,
            (double min, double max, double range) xRange, (double min, double max, double range) yRange)
        {
            var uniqueLabels = data.Labels.Distinct().OrderBy(l => l).ToList();
            var colors = GetClassColors(uniqueLabels);

            // Размер точек
            int pointSize = 8;

            for (int i = 0; i < data.Features.Length; i++)
            {
                if (_xAxisIndex < data.Features[i].Length && _yAxisIndex < data.Features[i].Length)
                {
                    double xValue = data.Features[i][_xAxisIndex];
                    double yValue = data.Features[i][_yAxisIndex];
                    double label = data.Labels[i];

                    // ПРЕОБРАЗУЕМ В КООРДИНАТЫ ГРАФИКА
                    float x = plotRect.Left + (float)((xValue - xRange.min) / xRange.range * plotRect.Width);
                    float y = plotRect.Bottom - (float)((yValue - yRange.min) / yRange.range * plotRect.Height);

                    // Проверяем, что точка в пределах графика
                    if (x >= plotRect.Left && x <= plotRect.Right && y >= plotRect.Top && y <= plotRect.Bottom)
                    {
                        Color pointColor = colors.ContainsKey(label) ? colors[label] : Color.Gray;

                        // Рисуем точку
                        using (var brush = new SolidBrush(pointColor))
                        {
                            g.FillEllipse(brush, x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
                        }
                        g.DrawEllipse(Pens.Black, x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
                    }
                }
            }
        }

        private void DrawDecisionBoundary(Graphics g, RectangleF plotRect,
            (double min, double max, double range) xRange, (double min, double max, double range) yRange)
        {
            if (_pipeline.Algorithm is Algorithms.Algorithms.SVM svm)
            {
                DrawSVMBoundary(g, plotRect, xRange, yRange);
            }
            else if (_pipeline.Algorithm is Algorithms.Algorithms.KNN knn)
            {
                DrawKNNBoundary(g, plotRect, xRange, yRange);
            }
        }

        private void DrawSVMBoundary(Graphics g, RectangleF plotRect,
            (double min, double max, double range) xRange, (double min, double max, double range) yRange)
        {
            // Для SVM рисуем разделяющую прямую
            using (var linePen = new Pen(Color.Red, 3))
            {
                // Берем две точки на границах графика
                float x1 = plotRect.Left;
                float x2 = plotRect.Right;

                // Для демонстрации рисуем линию посередине
                float yMid = plotRect.Top + plotRect.Height / 2;

                g.DrawLine(linePen, x1, yMid, x2, yMid);

                // Подпись
                using (var font = new Font("Arial", 10, FontStyle.Bold))
                {
                    g.DrawString("Граница SVM", font, Brushes.Red,
                        new PointF(plotRect.Left + 10, plotRect.Top + 10));
                }
            }
        }

        private void DrawKNNBoundary(Graphics g, RectangleF plotRect,
            (double min, double max, double range) xRange, (double min, double max, double range) yRange)
        {
            // Для KNN рисуем области решений
            int gridSize = 30;

            using (var font = new Font("Arial", 10, FontStyle.Bold))
            {
                g.DrawString("Области KNN", font, Brushes.Blue,
                    new PointF(plotRect.Left + 10, plotRect.Top + 10));
            }

            // Можно добавить пунктирные линии или заливку областей
            using (var areaPen = new Pen(Color.FromArgb(100, 0, 0, 255), 1))
            {
                areaPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                // Вертикальная разделительная линия
                float midX = plotRect.Left + plotRect.Width / 2;
                g.DrawLine(areaPen, midX, plotRect.Top, midX, plotRect.Bottom);

                // Горизонтальная разделительная линия
                float midY = plotRect.Top + plotRect.Height / 2;
                g.DrawLine(areaPen, plotRect.Left, midY, plotRect.Right, midY);
            }
        }

        private Dictionary<double, Color> GetClassColors(List<double> uniqueLabels)
        {
            var colors = new Dictionary<double, Color>();
            var colorPalette = new[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Orange,
                Color.Purple, Color.Brown, Color.Pink, Color.Teal,
                Color.Magenta, Color.DarkCyan, Color.DarkOrange, Color.DarkGreen
            };

            for (int i = 0; i < uniqueLabels.Count; i++)
            {
                colors[uniqueLabels[i]] = colorPalette[i % colorPalette.Length];
            }

            return colors;
        }

        private void DrawLegend(Graphics g, Dataset data, int x, int y)
        {
            var uniqueLabels = data.Labels.Distinct().OrderBy(l => l).ToList();
            var colors = GetClassColors(uniqueLabels);

            using (var titleFont = new Font("Arial", 10, FontStyle.Bold))
            using (var labelFont = new Font("Arial", 9))
            {
                g.DrawString("Легенда:", titleFont, Brushes.Black, new PointF(x, y));

                int boxSize = 12;
                int spacing = 20;

                for (int i = 0; i < Math.Min(10, uniqueLabels.Count); i++)
                {
                    var label = uniqueLabels[i];
                    var color = colors[label];
                    int yPos = y + 25 + i * spacing;

                    // Квадратик цвета
                    using (var brush = new SolidBrush(color))
                    {
                        g.FillRectangle(brush, x, yPos, boxSize, boxSize);
                    }
                    g.DrawRectangle(Pens.Black, x, yPos, boxSize, boxSize);

                    // Надпись
                    string labelText = $"Класс {label}";
                    g.DrawString(labelText, labelFont, Brushes.Black, new PointF(x + boxSize + 5, yPos - 2));
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_pictureBox.Image == null) return;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG файлы|*.png|JPEG файлы|*.jpg|BMP файлы|*.bmp";
                dialog.Title = "Сохранить график";
                dialog.FileName = $"graph_{_xAxisName}_vs_{_yAxisName}_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var format = dialog.FilterIndex switch
                        {
                            1 => System.Drawing.Imaging.ImageFormat.Png,
                            2 => System.Drawing.Imaging.ImageFormat.Jpeg,
                            3 => System.Drawing.Imaging.ImageFormat.Bmp,
                            _ => System.Drawing.Imaging.ImageFormat.Png
                        };

                        _pictureBox.Image.Save(dialog.FileName, format);
                        MessageBox.Show($"График сохранен:\n{dialog.FileName}",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
                    }
                }
            }
        }
    }
