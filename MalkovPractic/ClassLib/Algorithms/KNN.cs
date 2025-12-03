using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class KNN : BaseAlgorithm
    {
        protected int _k;
        protected DistanceMetric _distanceMetric;

        public KNN(int k, DistanceMetric distanceMetric = DistanceMetric.Euclidean)
        {
            _k = k;
            _distanceMetric = distanceMetric;
        }

        protected override double PredictInternal(double[] features)
        {
            var distances = new List<(double distance, double label, int index)>();

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateDistance(features, TrainingFeatures[i], _distanceMetric);
                distances.Add((distance, TrainingLabels[i], i));
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(_k)
                .ToList();

            // Отладка для понимания что происходит
            if (nearestNeighbors.Count > 0)
            {
                Console.WriteLine($"Ближайшие соседи (первые 3 из {nearestNeighbors.Count}):");
                for (int i = 0; i < Math.Min(3, nearestNeighbors.Count); i++)
                {
                    Console.WriteLine($"  Сосед {i}: расстояние={nearestNeighbors[i].distance:F4}, метка={nearestNeighbors[i].label}");
                }
            }

            if (ProblemType == ProblemType.Classification)
            {
                return PredictClassification(nearestNeighbors);
            }
            else
            {
                return nearestNeighbors.Average(n => n.label);
            }
        }

        private double PredictClassification(List<(double distance, double label, int index)> neighbors)
        {
            var groups = neighbors.GroupBy(n => n.label);

            var mostCommonGroup = groups.OrderByDescending(g => g.Count()).First();

            var topGroups = groups.Where(g => g.Count() == mostCommonGroup.Count()).ToList();
            if (topGroups.Count > 1)
            {
                var bestGroup = topGroups.OrderBy(g => g.Average(n => n.distance)).First();
                return bestGroup.Key;
            }

            return mostCommonGroup.Key;
        }

        public (double[] distances, double[] labels, int[] indices) GetNeighbors(double[] features, int k = -1)
        {
            if (k <= 0) k = _k;

            var distances = new List<(double distance, double label, int index)>();

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateDistance(features, TrainingFeatures[i], _distanceMetric);
                distances.Add((distance, TrainingLabels[i], i));
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(k)
                .ToList();

            return (nearestNeighbors.Select(n => n.distance).ToArray(),
                    nearestNeighbors.Select(n => n.label).ToArray(),
                    nearestNeighbors.Select(n => n.index).ToArray());
        }
    }
}