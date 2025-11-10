using System;
using System.Collections.Generic;
using System.Linq;
using MLAlgorithms.Core;

namespace MLAlgorithms.Algorithms
{
    public class STOL : BaseAlgorithm
    {
        private List<double[]> _featuresList;
        private List<double> _labelsList;
        private KNN _baseClassifier;
        private double _confidenceThreshold;
        private int _maxSamples;

        public STOL(double confidenceThreshold = 0.7, int k = 3, int maxSamples = 1000)
        {
            _featuresList = new List<double[]>();
            _labelsList = new List<double>();
            _baseClassifier = new KNN(k);
            _confidenceThreshold = confidenceThreshold;
            _maxSamples = maxSamples;
        }

        public override void Train(double[][] features, double[] labels, bool normalize = true)
        {
            base.Train(features, labels, normalize);

            _featuresList.Clear();
            _labelsList.Clear();
            _featuresList.AddRange(TrainingFeatures);
            _labelsList.AddRange(TrainingLabels);

            UpdateBaseClassifier();
        }

        protected override double PredictInternal(double[] features)
        {
            return _baseClassifier.Predict(features);
        }

        public void OnlineLearn(double[] features, double? label = null)
        {
            if (label.HasValue)
            {
                // Обучение с учителем
                AddSample(features, label.Value);
            }
            else
            {
                // Самообучение - предсказываем с уверенностью
                var prediction = PredictWithConfidence(features);
                if (prediction.confidence > _confidenceThreshold)
                {
                    AddSample(features, prediction.label);
                }
            }

            UpdateBaseClassifier();
        }

        private void AddSample(double[] features, double label)
        {
            if (_featuresList.Count >= _maxSamples)
            {
                // Удаляем самый старый образец
                _featuresList.RemoveAt(0);
                _labelsList.RemoveAt(0);
            }

            _featuresList.Add(features);
            _labelsList.Add(label);
        }

        private void UpdateBaseClassifier()
        {
            if (_featuresList.Count > 0)
            {
                _baseClassifier.Train(_featuresList.ToArray(), _labelsList.ToArray(), false);
            }
        }

        private (double label, double confidence) PredictWithConfidence(double[] features)
        {
            var neighbors = _baseClassifier.GetNeighbors(features, 3);

            if (neighbors.distances.Length == 0)
                return (0, 0);

            // Расчет уверенности на основе дистанций
            double totalDistance = neighbors.distances.Sum();
            double avgDistance = totalDistance / neighbors.distances.Length;
            double confidence = 1.0 / (1.0 + avgDistance);

            // Определяем наиболее частую метку
            var labelGroups = neighbors.labels.GroupBy(x => x);
            var mostCommonLabel = labelGroups.OrderByDescending(g => g.Count()).First().Key;

            return (mostCommonLabel, confidence);
        }

        public int GetCurrentSampleCount() => _featuresList.Count;
    }
}