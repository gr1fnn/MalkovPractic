using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Algorithms.Core;
using Algorithms.Preprocessing;
using Algorithms.Algorithms;

namespace Algorithms.Pipelines
{
    public class Pipeline
    {
        public DatasetConfig Config { get; set; }
        public BaseAlgorithm Algorithm { get; private set; }
        public Dataset TrainingData { get; private set; }
        public Dataset TestData { get; private set; }
        public PipelineResult Result { get; private set; }
        public DefaultDataPreprocessor Preprocessor { get; private set; }
        public NormalizationType NormalizationType { get; set; } = NormalizationType.ZScore;

        public Pipeline(DatasetConfig config)
        {
            Config = config;
            Algorithm = CreateAlgorithm(config.AlgorithmType, config.AlgorithmParameters);
            Result = new PipelineResult();
        }

        private BaseAlgorithm CreateAlgorithm(Type algorithmType, Dictionary<string, object> parameters)
        {
            if (algorithmType == typeof(KNN))
            {
                int k = parameters.ContainsKey("K") ? (int)parameters["K"] : 3;
                var distanceMetric = parameters.ContainsKey("DistanceMetric") ?
                    (DistanceMetric)parameters["DistanceMetric"] : DistanceMetric.Euclidean;

                return new KNN(k, distanceMetric);
            }
            else if (algorithmType == typeof(WeightedKNN))
            {
                int k = parameters.ContainsKey("K") ? (int)parameters["K"] : 3;
                var distanceMetric = parameters.ContainsKey("DistanceMetric") ?
                    (DistanceMetric)parameters["DistanceMetric"] : DistanceMetric.Euclidean;

                return new WeightedKNN(k, distanceMetric);
            }
            else if (algorithmType == typeof(NadarayaWatson))
            {
                double bandwidth = parameters.ContainsKey("Bandwidth") ? (double)parameters["Bandwidth"] : 1.0;
                var kernelType = parameters.ContainsKey("KernelType") ?
                    (KernelType)parameters["KernelType"] : KernelType.Gaussian;

                return new NadarayaWatson(kernelType, bandwidth);
            }
            else if (algorithmType == typeof(SVM))
            {
                double learningRate = parameters.ContainsKey("LearningRate") ? (double)parameters["LearningRate"] : 0.001;
                int epochs = parameters.ContainsKey("Epochs") ? (int)parameters["Epochs"] : 1000;

                double lambda = parameters.ContainsKey("Lambda") ? (double)parameters["Lambda"] : 0.01;
                double c = parameters.ContainsKey("C") ? (double)parameters["C"] : 1.0;

                return new SVM(learningRate, lambda, epochs, c);
            }
            else if (algorithmType == typeof(STOL))
            {
                double confidenceThreshold = parameters.ContainsKey("ConfidenceThreshold") ? (double)parameters["ConfidenceThreshold"] : 0.7;
                int k = parameters.ContainsKey("K") ? (int)parameters["K"] : 3;
                int maxSamples = parameters.ContainsKey("MaxSamples") ? (int)parameters["MaxSamples"] : 1000;

                return new STOL(confidenceThreshold, k, maxSamples);
            }

            throw new ArgumentException($"Unsupported algorithm type: {algorithmType}");
        }

        public void LoadAndTrain(string csvFilePath)
        {
            Console.WriteLine($"=== Запуск пайплайна для {Config.Name} ===");

            string normTypeText = NormalizationType switch
            {
                NormalizationType.None => "Без нормирования",
                NormalizationType.ZScore => "Z-Score (статистическое)",
                NormalizationType.MinMax => "Min-Max (линейное)",
                NormalizationType.LogScale => "Логарифмическое",
                _ => "Неизвестно"
            };

            Console.WriteLine($"Тип нормирования: {normTypeText}");

            // Загрузка данных
            var rawData = DataLoader.LoadCSV(csvFilePath, Config.HasHeader);
            Console.WriteLine($"Загружено {rawData.Length} записей");

            // Получение названий колонок
            string[] columnNames = Config.HasHeader ?
                DataLoader.GetColumnNames(csvFilePath) : null;

            // Подготовка данных с препроцессором - сохраняем его
            Preprocessor = new DefaultDataPreprocessor();
            var (features, labels) = DataLoader.PrepareData(rawData, Config.FeatureColumns, Config.LabelColumn, Preprocessor);

            // Разделяем данные на обучающую (80%) и тестовую (20%) выборки
            var (trainFeatures, testFeatures, trainLabels, testLabels) = SplitData(features, labels, 0.8);

            // Сохраняем данные
            TrainingData = new Dataset(trainFeatures, trainLabels, columnNames, Config.ProblemType);
            TestData = new Dataset(testFeatures, testLabels, columnNames, Config.ProblemType);

            Console.WriteLine($"Разделение: {trainFeatures.Length} обучающих, {testFeatures.Length} тестовых");

            // Обучение алгоритма с выбранной нормализацией
            var stopwatch = Stopwatch.StartNew();

            if (NormalizationType == NormalizationType.None)
            {
                // Без нормирования - используем исходные данные
                Algorithm.Train(TrainingData.Features, TrainingData.Labels, normalize: false);
                Evaluate(TestData.Features, TestData.Labels);
            }
            else
            {
                // С нормированием - создаем скалер
                DataScaler scaler = new DataScaler(NormalizationType);

                // Нормализуем обучающие данные
                var normalizedTrainFeatures = scaler.FitTransform(TrainingData.Features);

                // Обучаем алгоритм на нормализованных данных
                Algorithm.Train(normalizedTrainFeatures, TrainingData.Labels, normalize: false);

                // Оценка на тестовых данных (также нормализованных)
                var normalizedTestFeatures = scaler.Transform(TestData.Features);
                Evaluate(normalizedTestFeatures, TestData.Labels);

                // Показываем информацию о нормализации
                if (trainFeatures.Length > 0 && trainFeatures[0].Length > 0)
                {
                    Console.WriteLine("\n=== ИНФОРМАЦИЯ О НОРМАЛИЗАЦИИ ===");
                    string effect = scaler.PrintNormalizationEffect(trainFeatures, 0, 3);
                    Console.WriteLine(effect);
                }
            }

            stopwatch.Stop();

            // Сохранение результатов
            Result.TrainingTime = stopwatch.Elapsed.TotalSeconds;
            Result.Algorithm = Algorithm;

            Console.WriteLine($"Обучение завершено за {Result.TrainingTime:F2} секунд");
            Console.WriteLine($"Тестовая точность: {Result.Accuracy:P2}");
        }
        public void SetNormalizationType(NormalizationType type)
        {
            NormalizationType = type;
        }

        // Метод для получения маппинга категорий
        public Dictionary<string, Dictionary<string, double>> GetCategoryMappings()
        {
            return Preprocessor?.GetCategoryMappings() ?? new Dictionary<string, Dictionary<string, double>>();
        }

        // Метод для получения маппинга конкретной колонки
        public Dictionary<string, double> GetColumnMapping(int columnIndex)
        {
            return Preprocessor?.GetColumnMapping(columnIndex) ?? new Dictionary<string, double>();
        }

        // Метод для получения обратного маппинга
        public Dictionary<double, string> GetReverseColumnMapping(int columnIndex)
        {
            return Preprocessor?.GetReverseColumnMapping(columnIndex) ?? new Dictionary<double, string>();
        }

        // Метод для получения имени категории
        public string GetCategoryName(int columnIndex, double numericValue)
        {
            return Preprocessor?.GetCategoryName(columnIndex, numericValue) ?? $"Unknown (класс {Math.Round(numericValue)})";
        }

        // Простой метод для разделения данных
        private (double[][], double[][], double[], double[]) SplitData(
            double[][] features, double[] labels, double trainRatio)
        {
            int total = features.Length;
            int trainCount = (int)(total * trainRatio);

            var trainFeatures = new List<double[]>();
            var testFeatures = new List<double[]>();
            var trainLabels = new List<double>();
            var testLabels = new List<double>();

            // Берем первые N записей для обучения, остальные для теста
            for (int i = 0; i < total; i++)
            {
                if (i < trainCount)
                {
                    trainFeatures.Add(features[i]);
                    trainLabels.Add(labels[i]);
                }
                else
                {
                    testFeatures.Add(features[i]);
                    testLabels.Add(labels[i]);
                }
            }

            return (trainFeatures.ToArray(), testFeatures.ToArray(),
                    trainLabels.ToArray(), testLabels.ToArray());
        }

        public double Predict(double[] features)
        {
            if (Algorithm == null)
                throw new InvalidOperationException("Algorithm must be trained first");

            return Algorithm.Predict(features);
        }

        public double[] Predict(double[][] features)
        {
            return features.Select(f => Predict(f)).ToArray();
        }

        private double CalculateAccuracy(double[] predictions, double[] actual)
        {
            if (predictions.Length == 0) return 0;

            int correct = 0;

            if (Config.ProblemType == ProblemType.Classification)
            {
                // Для классификации сравниваем с допуском
                for (int i = 0; i < predictions.Length; i++)
                {
                    // Используем Math.Round для получения целого класса
                    double predictedClass = Math.Round(predictions[i]);
                    double actualClass = Math.Round(actual[i]);

                    // Сравниваем с небольшим допуском
                    if (Math.Abs(predictedClass - actualClass) < 0.001)
                        correct++;
                }
            }
            else
            {
                // Для регрессии точность не рассчитываем
                return 0;
            }

            return (double)correct / predictions.Length;
        }

        public void Evaluate(double[][] testFeatures, double[] testLabels)
        {
            var predictions = Predict(testFeatures);

            if (Config.ProblemType == ProblemType.Classification)
            {
                Result.Accuracy = CalculateAccuracy(predictions, testLabels);

                Console.WriteLine("\n=== ДЕТАЛЬНАЯ ОЦЕНКА ===");
                Console.WriteLine($"Точность: {Result.Accuracy:P2} ({GetCorrectCount(predictions, testLabels)}/{predictions.Length})");

                if (predictions.Length > 0)
                {
                    PrintConfusionMatrix(predictions, testLabels);
                }
            }
            else
            {
                Result.RMSE = CalculateRMSE(predictions, testLabels);
                Result.MAE = CalculateMAE(predictions, testLabels);
                Console.WriteLine($"RMSE: {Result.RMSE:F4}, MAE: {Result.MAE:F4}");
            }
        }

        private int GetCorrectCount(double[] predictions, double[] actual)
        {
            int correct = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                if (Math.Abs(Math.Round(predictions[i]) - Math.Round(actual[i])) < 0.001)
                    correct++;
            }
            return correct;
        }

        private void PrintConfusionMatrix(double[] predictions, double[] actual)
        {
            var uniqueLabels = actual.Distinct().OrderBy(x => x).ToList();

            Console.WriteLine("\nМатрица ошибок:");
            Console.Write("      ");
            foreach (var predLabel in uniqueLabels)
            {
                Console.Write($"{predLabel,7:F0} ");
            }
            Console.WriteLine(" (Предсказано)");
            Console.WriteLine("      " + new string('-', uniqueLabels.Count * 8));

            foreach (var trueLabel in uniqueLabels)
            {
                Console.Write($"{trueLabel,4:F0} |");
                foreach (var predLabel in uniqueLabels)
                {
                    int count = 0;
                    for (int i = 0; i < predictions.Length; i++)
                    {
                        if (Math.Abs(actual[i] - trueLabel) < 0.001 &&
                            Math.Abs(Math.Round(predictions[i]) - predLabel) < 0.001)
                        {
                            count++;
                        }
                    }
                    Console.Write($"{count,7} ");
                }
                Console.WriteLine($"| (Истинный: {trueLabel:F0})");
            }
        }

        private double CalculateRMSE(double[] predictions, double[] actual)
        {
            if (predictions.Length == 0) return 0;

            double sum = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                sum += Math.Pow(predictions[i] - actual[i], 2);
            }
            return Math.Sqrt(sum / predictions.Length);
        }

        private double CalculateMAE(double[] predictions, double[] actual)
        {
            if (predictions.Length == 0) return 0;

            double sum = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                sum += Math.Abs(predictions[i] - actual[i]);
            }
            return sum / predictions.Length;
        }

        public void SaveModel(string filePath)
        {
            Algorithm?.SaveModel(filePath);
        }

        public void LoadModel(string filePath)
        {
            Algorithm?.LoadModel(filePath);
        }
    }

    public class PipelineResult
    {
        public BaseAlgorithm Algorithm { get; set; }
        public double TrainingTime { get; set; }
        public double Accuracy { get; set; }
        public double RMSE { get; set; }
        public double MAE { get; set; }
    }
}