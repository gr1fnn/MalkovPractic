using Algorithms.Core;
using Algorithms.Preprocessing;

namespace MyForm
{
    public class KNNHandler : AlgorithmTabHandler
    {
        public KNNHandler(MainForm mainForm, TabPage tabPage,
            CheckedListBox featuresListBox, ComboBox targetComboBox,
            ComboBox problemTypeComboBox, ComboBox normalizationComboBox,
            NumericUpDown kControl, TextBox resultTextBox, Button predictButton,
            ProgressBar progressBar, Label statusLabel)
            : base(mainForm, tabPage, featuresListBox, targetComboBox,
                  problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                  predictButton, progressBar, statusLabel)
        {
        }

        protected override int GetDefaultKValue() => 3;
        protected override Type GetAlgorithmType() => typeof(Algorithms.Algorithms.KNN);

        protected override Dictionary<string, object> GetAlgorithmParameters()
        {
            return new Dictionary<string, object>
            {
                { "K", (int)KControl.Value },
                { "DistanceMetric", DistanceMetric.Euclidean }
            };
        }
    }

    public class WeightedKNNHandler : AlgorithmTabHandler
    {
        public WeightedKNNHandler(MainForm mainForm, TabPage tabPage,
            CheckedListBox featuresListBox, ComboBox targetComboBox,
            ComboBox problemTypeComboBox, ComboBox normalizationComboBox,
            NumericUpDown kControl, TextBox resultTextBox, Button predictButton,
            ProgressBar progressBar, Label statusLabel)
            : base(mainForm, tabPage, featuresListBox, targetComboBox,
                  problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                  predictButton, progressBar, statusLabel)
        {
        }

        protected override int GetDefaultKValue() => 3;
        protected override Type GetAlgorithmType() => typeof(Algorithms.Algorithms.WeightedKNN);

        protected override Dictionary<string, object> GetAlgorithmParameters()
        {
            return new Dictionary<string, object>
            {
                { "K", (int)KControl.Value },
                { "DistanceMetric", DistanceMetric.Euclidean }
            };
        }
    }

    public class STOLHandler : AlgorithmTabHandler
    {
        private NumericUpDown _confidenceThreshold;
        private NumericUpDown _maxSamples;

        public STOLHandler(MainForm mainForm, TabPage tabPage,
            CheckedListBox featuresListBox, ComboBox targetComboBox,
            ComboBox problemTypeComboBox, ComboBox normalizationComboBox,
            NumericUpDown kControl, TextBox resultTextBox, Button predictButton,
            ProgressBar progressBar, Label statusLabel,
            NumericUpDown confidenceThreshold, NumericUpDown maxSamples)
            : base(mainForm, tabPage, featuresListBox, targetComboBox,
                  problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                  predictButton, progressBar, statusLabel)
        {
            _confidenceThreshold = confidenceThreshold;
            _maxSamples = maxSamples;

            // Настройка дополнительных контролов
            _confidenceThreshold.Minimum = 0;
            _confidenceThreshold.Maximum = 1;
            _confidenceThreshold.DecimalPlaces = 2;
            _confidenceThreshold.Increment = 0.05M;
            _confidenceThreshold.Value = 0.7M;

            _maxSamples.Minimum = 100;
            _maxSamples.Maximum = 10000;
            _maxSamples.Value = 1000;
        }

        protected override int GetDefaultKValue() => 3;
        protected override Type GetAlgorithmType() => typeof(Algorithms.Algorithms.STOL);

        protected override Dictionary<string, object> GetAlgorithmParameters()
        {
            return new Dictionary<string, object>
            {
                { "K", (int)KControl.Value },
                { "ConfidenceThreshold", (double)_confidenceThreshold.Value },
                { "MaxSamples", (int)_maxSamples.Value }
            };
        }
    }

    public class SVMHandler : AlgorithmTabHandler
    {
        private NumericUpDown _learningRate;
        private NumericUpDown _lambda;
        private NumericUpDown _cParam;

        public SVMHandler(MainForm mainForm, TabPage tabPage,
            CheckedListBox featuresListBox, ComboBox targetComboBox,
            ComboBox problemTypeComboBox, ComboBox normalizationComboBox,
            NumericUpDown epochsControl, TextBox resultTextBox, Button predictButton,
            ProgressBar progressBar, Label statusLabel,
            NumericUpDown learningRate, NumericUpDown lambda, NumericUpDown cParam)
            : base(mainForm, tabPage, featuresListBox, targetComboBox,
                  problemTypeComboBox, normalizationComboBox, epochsControl, resultTextBox,
                  predictButton, progressBar, statusLabel)
        {
            _learningRate = learningRate;
            _lambda = lambda;
            _cParam = cParam;

            // Настройка параметров SVM
            _learningRate.Minimum = 0.0001M;
            _learningRate.Maximum = 0.1M;
            _learningRate.DecimalPlaces = 4;
            _learningRate.Increment = 0.0001M;
            _learningRate.Value = 0.001M;

            if (_lambda != null)
            {
                _lambda.Minimum = 0.001M;
                _lambda.Maximum = 1.0M;
                _lambda.DecimalPlaces = 3;
                _lambda.Increment = 0.001M;
                _lambda.Value = 0.01M;
            }

            if (_cParam != null)
            {
                _cParam.Minimum = 0.1M;
                _cParam.Maximum = 10.0M;
                _cParam.DecimalPlaces = 2;
                _cParam.Increment = 0.1M;
                _cParam.Value = 1.0M;
            }
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();
            // SVM работает только с классификацией
            ProblemTypeComboBox.Items.Clear();
            ProblemTypeComboBox.Items.Add("Классификация");
            ProblemTypeComboBox.SelectedIndex = 0;

            // Переименуем KControl для ясности (это количество эпох)
            KControl.Minimum = 100;
            KControl.Maximum = 10000;
            KControl.Value = 1000;
        }

        protected override int GetDefaultKValue() => 1000; // epochs для SVM
        protected override Type GetAlgorithmType() => typeof(Algorithms.Algorithms.SVM);

        protected override Dictionary<string, object> GetAlgorithmParameters()
        {
            return new Dictionary<string, object>
            {
                { "LearningRate", _learningRate != null ? (double)_learningRate.Value : 0.001 },
                { "Epochs", (int)KControl.Value },
                { "Lambda", _lambda != null ? (double)_lambda.Value : 0.01 },
                { "C", _cParam != null ? (double)_cParam.Value : 1.0 }
            };
        }
    }

    public class NadarayaWatsonHandler : AlgorithmTabHandler
    {
        private ComboBox _kernelType;

        public NadarayaWatsonHandler(MainForm mainForm, TabPage tabPage,
            CheckedListBox featuresListBox, ComboBox targetComboBox,
            ComboBox problemTypeComboBox, ComboBox normalizationComboBox,
            NumericUpDown bandwidthControl, TextBox resultTextBox, Button predictButton,
            ProgressBar progressBar, Label statusLabel,
            ComboBox kernelType)
            : base(mainForm, tabPage, featuresListBox, targetComboBox,
                  problemTypeComboBox, normalizationComboBox, bandwidthControl, resultTextBox,
                  predictButton, progressBar, statusLabel)
        {
            _kernelType = kernelType;
            if (_kernelType.Items.Count == 0)
            {
                _kernelType.Items.Add("Линейное");
                _kernelType.Items.Add("Гауссово");
                _kernelType.Items.Add("Епанечникова");
                _kernelType.SelectedIndex = 1; // Гауссово по умолчанию
            }

            // Настройка параметра Bandwidth
            KControl.Minimum = 0.1M;
            KControl.Maximum = 10.0M;
            KControl.DecimalPlaces = 2;
            KControl.Increment = 0.1M;
            KControl.Value = 1.0M;
        }

        protected override int GetDefaultKValue() => 1; // Bandwidth по умолчанию
        protected override Type GetAlgorithmType() => typeof(Algorithms.Algorithms.NadarayaWatson);

        protected override Dictionary<string, object> GetAlgorithmParameters()
        {
            KernelType kernel = _kernelType.SelectedIndex switch
            {
                0 => KernelType.Linear,
                1 => KernelType.Gaussian,
                2 => KernelType.Epanechnikov,
                _ => KernelType.Gaussian
            };

            return new Dictionary<string, object>
            {
                { "Bandwidth", (double)KControl.Value },
                { "KernelType", kernel }
            };
        }

        protected override void ShowTrainingResults(NormalizationType normType = NormalizationType.ZScore)
        {
            base.ShowTrainingResults(normType);
            if (Pipeline?.Result != null)
            {
                string currentText = ResultTextBox.Text;
                currentText += $"\n\nДополнительные параметры:\n" +
                              $"Полоса пропускания: {KControl.Value:F2}\n" +
                              $"Тип ядра: {_kernelType.SelectedItem}\n" +
                              $"Нормирование: {NormalizationComboBox.SelectedItem}";
                ResultTextBox.Text = currentText;
            }
        }
    
    }
}