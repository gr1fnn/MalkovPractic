using System;
using System.Diagnostics;
using System.Linq;
using Algorithms.Core;
using Algorithms.Preprocessing;
using Algorithms.Algorithms;

namespace Algorithms.Pipelines
{
    public class MLPipeline
    {
        public DatasetConfig Config { get; set; }
        public BaseAlgorithm Algorithm { get; private set; }
        public Dataset Data { get; private set; }
        public TrainingResult Result { get; private set; }

        public MLPipeline(DatasetConfig config)
        {
            Config = config;
            Algorithm = config.CreateAlgorithm();
            Result = new TrainingResult(); // Инициализация Result
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
                if (Math.Abs(predictions[i] - actual[i]) < 0.5) // для классификации
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
}