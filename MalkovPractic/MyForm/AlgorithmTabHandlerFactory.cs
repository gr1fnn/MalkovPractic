namespace MyForm
{
    public static class AlgorithmTabHandlerFactory
    {
        public static AlgorithmTabHandler CreateHandler(
            AlgorithmType algorithmType,
            MainForm mainForm,
            TabPage tabPage,
            CheckedListBox featuresListBox,
            ComboBox targetComboBox,
            ComboBox problemTypeComboBox,
            ComboBox normalizationComboBox,  // ДОБАВЛЯЕМ ПАРАМЕТР
            NumericUpDown kControl,
            TextBox resultTextBox,
            Button predictButton,
            ProgressBar progressBar,
            Label statusLabel,
            NumericUpDown confidenceThreshold = null,
            NumericUpDown maxSamples = null,
            NumericUpDown learningRate = null,
            NumericUpDown lambda = null,
            NumericUpDown cParam = null,
            ComboBox kernelType = null)
        {
            return algorithmType switch
            {
                AlgorithmType.KNN => new KNNHandler(mainForm, tabPage, featuresListBox,
                    targetComboBox, problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                    predictButton, progressBar, statusLabel),

                AlgorithmType.WeightedKNN => new WeightedKNNHandler(mainForm, tabPage, featuresListBox,
                    targetComboBox, problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                    predictButton, progressBar, statusLabel),

                AlgorithmType.STOL => new STOLHandler(mainForm, tabPage, featuresListBox,
                    targetComboBox, problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                    predictButton, progressBar, statusLabel,
                    confidenceThreshold, maxSamples),

                AlgorithmType.SVM => new SVMHandler(mainForm, tabPage, featuresListBox,
                    targetComboBox, problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                    predictButton, progressBar, statusLabel,
                    learningRate, lambda, cParam),

                AlgorithmType.NadarayaWatson => new NadarayaWatsonHandler(mainForm, tabPage, featuresListBox,
                    targetComboBox, problemTypeComboBox, normalizationComboBox, kControl, resultTextBox,
                    predictButton, progressBar, statusLabel,
                    kernelType),

                _ => throw new ArgumentException($"Неизвестный тип алгоритма: {algorithmType}")
            };
        }
    }

    public enum AlgorithmType
    {
        KNN,
        WeightedKNN,
        STOL,
        SVM,
        NadarayaWatson
    }
}