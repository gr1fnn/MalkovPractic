using ClassLib;
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
        public ProblemType ProblemType { get; protected set; }

        public virtual void Train(double[][] features, double[] labels, bool normalize = true)
        {
            ValidateData(features, labels);

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

            // Автоматическое определение типа задачи
            ProblemType = DetermineProblemType(labels);
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
                throw new ArgumentException("Features and labels must have same length");
        }

        protected virtual ProblemType DetermineProblemType(double[] labels)
        {
            // Если все метки - целые числа, считаем это классификацией
            bool allInteger = labels.All(label => Math.Abs(label - Math.Round(label)) < 0.0001);
            return allInteger ? ProblemType.Classification : ProblemType.Regression;
        }

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
            // Реализация сохранения модели
        }

        public virtual void LoadModel(string filePath)
        {
            Console.WriteLine($"Loading model from {filePath}");
            // Реализация загрузки модели
        }
    }
}