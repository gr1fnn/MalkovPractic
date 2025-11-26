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
        public Dataset Data { get; private set; }
        public PipelineResult Result { get; private set; }

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

                // Добавляем параметры по умолчанию для SVM
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

            // Загрузка данных
            var rawData = DataLoader.LoadCSV(csvFilePath, Config.HasHeader);
            Console.WriteLine($"Загружено {rawData.Length} samples");

            // Получение названий колонок
            string[] columnNames = Config.HasHeader ?
                DataLoader.GetColumnNames(csvFilePath) : null;

            // Подготовка данных
            var (features, labels) = DataLoader.PrepareData(rawData, Config.FeatureColumns, Config.LabelColumn);
            Data = new Dataset(features, labels, columnNames, Config.ProblemType);

            // Обучение алгоритма
            var stopwatch = Stopwatch.StartNew();
            Algorithm.Train(Data.Features, Data.Labels);
            stopwatch.Stop();

            // Сохранение результатов
            Result.TrainingTime = stopwatch.Elapsed.TotalSeconds;
            Result.Algorithm = Algorithm;

            // Оценка на тренировочных данных (для демонстрации)
            Evaluate(Data.Features, Data.Labels);

            Console.WriteLine($"Обучение завершено за {Result.TrainingTime:F2} секунд");
            Console.WriteLine($"Размер данных: {Data.Features.Length} samples, {Data.Features[0].Length} features");
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

        public void Evaluate(double[][] testFeatures, double[] testLabels)
        {
            var predictions = Predict(testFeatures);

            if (Config.ProblemType == ProblemType.Classification)
            {
                Result.Accuracy = CalculateAccuracy(predictions, testLabels);
                Console.WriteLine($"Accuracy: {Result.Accuracy:P2}");
            }
            else
            {
                Result.RMSE = CalculateRMSE(predictions, testLabels);
                Result.MAE = CalculateMAE(predictions, testLabels);
                Console.WriteLine($"RMSE: {Result.RMSE:F2}, MAE: {Result.MAE:F2}");
            }
        }

        private double CalculateAccuracy(double[] predictions, double[] actual)
        {
            int correct = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                if (Math.Abs(predictions[i] - actual[i]) < 0.5) 
                    correct++;
            }
            return (double)correct / predictions.Length;
        }

        private double CalculateRMSE(double[] predictions, double[] actual)
        {
            double sum = 0;
            for (int i = 0; i < predictions.Length; i++)
            {
                sum += Math.Pow(predictions[i] - actual[i], 2);
            }
            return Math.Sqrt(sum / predictions.Length);
        }

        private double CalculateMAE(double[] predictions, double[] actual)
        {
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