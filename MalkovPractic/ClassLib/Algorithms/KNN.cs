using System;
using System.Collections.Generic;
using System.Linq;
using MLAlgorithms.Core;

namespace MLAlgorithms.Algorithms
{
    public class KNN : BaseAlgorithm
    {
        protected int _k;
        protected DistanceMetric _distanceMetric;

        public KNN(int k = 3, DistanceMetric distanceMetric = DistanceMetric.Euclidean)
        {
            _k = k;
            _distanceMetric = distanceMetric;
        }

        protected override double PredictInternal(double[] features)
        {
            var distances = new List<(double distance, double label)>();

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateDistance(features, TrainingFeatures[i], _distanceMetric);
                distances.Add((distance, TrainingLabels[i]));
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(_k)
                .ToList();

            if (ProblemType == ProblemType.Classification)
            {
                // Классификация - голосование большинства
                return nearestNeighbors
                    .GroupBy(n => n.label)
                    .OrderByDescending(g => g.Count())
                    .First()
                    .Key;
            }
            else
            {
                // Регрессия - среднее значение
                return nearestNeighbors.Average(n => n.label);
            }
        }

        public (double[] distances, double[] labels) GetNeighbors(double[] features, int k = -1)
        {
            if (k <= 0) k = _k;

            var distances = new List<(double distance, double label)>();

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateDistance(features, TrainingFeatures[i], _distanceMetric);
                distances.Add((distance, TrainingLabels[i]));
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(k)
                .ToList();

            return (nearestNeighbors.Select(n => n.distance).ToArray(),
                    nearestNeighbors.Select(n => n.label).ToArray());
        }
    }
}