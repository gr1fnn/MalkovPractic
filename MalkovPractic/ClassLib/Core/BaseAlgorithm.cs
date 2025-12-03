using Algorithms.Preprocessing;
using System;
using System.Linq;

namespace Algorithms.Core
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        protected double[][] TrainingFeatures { get; set; }
        protected double[] TrainingLabels { get; set; }
        protected bool IsTrained { get; set; }
        protected DataScaler Scaler { get; set; }

        // Храним информацию о классах
        protected double[] UniqueLabels { get; private set; }
        protected int NumClasses { get; private set; }
        protected int[] ClassCounts { get; private set; }

        // Для большинства алгоритмов мы будем использовать целые метки классов
        protected int[] IntTrainingLabels { get; private set; }

        public virtual void Train(double[][] features, double[] labels, bool normalize = true)
        {
            ValidateData(features, labels);

            // Преобразуем метки в целые числа для классификации
            PrepareLabels(labels);

            if (normalize)
            {
                Scaler = new DataScaler();
                TrainingFeatures = Scaler.FitTransform(features);
            }
            else
            {
                TrainingFeatures = features;
            }

            TrainingLabels = labels;
            IsTrained = true;

            // Логируем информацию о данных
            LogTrainingInfo();
        }

        public virtual double Predict(double[] features)
        {
            if (!IsTrained)
                throw new InvalidOperationException("Algorithm must be trained first");

            double[] scaledFeatures = Scaler?.Transform(new double[][] { features })[0] ?? features;
            return PredictInternal(scaledFeatures);
        }

        protected abstract double PredictInternal(double[] features);

        protected virtual void ValidateData(double[][] features, double[] labels)
        {
            if (features == null || features.Length == 0)
                throw new ArgumentException("Features cannot be null or empty");

            if (labels == null || labels.Length == 0)
                throw new ArgumentException("Labels cannot be null or empty");

            if (features.Length != labels.Length)
                throw new ArgumentException($"Features ({features.Length}) and labels ({labels.Length}) must have same length");

            // Проверяем, что есть хотя бы 2 класса
            var uniqueLabels = labels.Distinct().ToArray();
            if (uniqueLabels.Length < 2)
                throw new ArgumentException($"Need at least 2 classes for classification. Found: {uniqueLabels.Length}");
        }

        protected virtual void PrepareLabels(double[] labels)
        {
            // Получаем уникальные метки и сортируем их
            UniqueLabels = labels.Distinct().OrderBy(x => x).ToArray();
            NumClasses = UniqueLabels.Length;

            // Создаем маппинг метка -> индекс класса
            var labelToIndex = new System.Collections.Generic.Dictionary<double, int>();
            for (int i = 0; i < NumClasses; i++)
            {
                labelToIndex[UniqueLabels[i]] = i;
            }

            // Преобразуем метки в целые индексы
            IntTrainingLabels = new int[labels.Length];
            ClassCounts = new int[NumClasses];

            for (int i = 0; i < labels.Length; i++)
            {
                int classIndex = labelToIndex[labels[i]];
                IntTrainingLabels[i] = classIndex;
                ClassCounts[classIndex]++;
            }
        }

        protected virtual void LogTrainingInfo()
        {
            Console.WriteLine($"=== ИНФОРМАЦИЯ О КЛАССИФИКАЦИИ ===");
            Console.WriteLine($"Количество образцов: {TrainingFeatures.Length}");
            Console.WriteLine($"Количество признаков: {TrainingFeatures[0].Length}");
            Console.WriteLine($"Количество классов: {NumClasses}");
            Console.WriteLine($"Уникальные метки: [{string.Join(", ", UniqueLabels.Select(x => x.ToString("F0")))}]");

            Console.WriteLine("Распределение классов:");
            for (int i = 0; i < NumClasses; i++)
            {
                Console.WriteLine($"  Класс {UniqueLabels[i]}: {ClassCounts[i]} записей ({(double)ClassCounts[i] / TrainingFeatures.Length:P1})");
            }
        }

        // Вспомогательный метод для преобразования индекса класса в оригинальную метку
        protected double GetOriginalLabel(int classIndex)
        {
            if (classIndex >= 0 && classIndex < UniqueLabels.Length)
                return UniqueLabels[classIndex];
            return 0;
        }

        // Вспомогательный метод для получения индекса класса по оригинальной метке
        protected int GetClassIndex(double label)
        {
            for (int i = 0; i < UniqueLabels.Length; i++)
            {
                if (Math.Abs(UniqueLabels[i] - label) < 0.0001)
                    return i;
            }
            return -1;
        }

        // Методы расстояний остаются без изменений
        protected double CalculateEuclideanDistance(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have same dimension");

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += Math.Pow(a[i] - b[i], 2);
            }
            return Math.Sqrt(sum);
        }

        protected double CalculateManhattanDistance(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have same dimension");

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += Math.Abs(a[i] - b[i]);
            }
            return sum;
        }

        protected double CalculateCosineDistance(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have same dimension");

            double dotProduct = 0, normA = 0, normB = 0;
            for (int i = 0; i < a.Length; i++)
            {
                dotProduct += a[i] * b[i];
                normA += Math.Pow(a[i], 2);
                normB += Math.Pow(b[i], 2);
            }
            return 1 - (dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB)));
        }

        protected double CalculateDistance(double[] a, double[] b, DistanceMetric metric = DistanceMetric.Euclidean)
        {
            return metric switch
            {
                DistanceMetric.Manhattan => CalculateManhattanDistance(a, b),
                DistanceMetric.Cosine => CalculateCosineDistance(a, b),
                _ => CalculateEuclideanDistance(a, b)
            };
        }

        public virtual void SaveModel(string filePath)
        {
            Console.WriteLine($"Saving model to {filePath}");
        }

        public virtual void LoadModel(string filePath)
        {
            Console.WriteLine($"Loading model from {filePath}");
        }
    }
}