using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class WeightedKNN : KNN
    {
        public WeightedKNN(int k = 3, DistanceMetric distanceMetric = DistanceMetric.Euclidean)
            : base(k, distanceMetric) { }

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

            
            var weightedVotes = new Dictionary<double, double>();

            foreach (var neighbor in nearestNeighbors)
            {
                    double weight = 1.0 / (neighbor.distance + 1e-8);
                    if (weightedVotes.ContainsKey(neighbor.label))
                        weightedVotes[neighbor.label] += weight;
                    else
                        weightedVotes[neighbor.label] = weight;
            }

             return weightedVotes.OrderByDescending(v => v.Value).First().Key;
            
        }
    }
}