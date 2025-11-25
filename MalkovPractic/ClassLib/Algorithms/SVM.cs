using System;
using System.Linq;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class SVM : BaseAlgorithm
    {
        private double[] _weights;
        private double _bias;
        private double _learningRate;
        private double _lambda;
        private int _epochs;
        private double _c; // параметр регуляризации

        public SVM(double learningRate = 0.001, double lambda = 0.01, int epochs = 1000, double c = 1.0)
        {
            _learningRate = learningRate;
            _lambda = lambda;
            _epochs = epochs;
            _c = c;
        }

        public override void Train(double[][] features, double[] labels, bool normalize = true)
        {
            base.Train(features, labels, normalize);

            // Преобразуем метки в -1 и 1 для бинарной классификации
            double[] binaryLabels = new double[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                binaryLabels[i] = (Math.Abs(labels[i] - 0) < 0.0001) ? -1.0 : 1.0;
            }

            // Остальной код без изменений...
            int nFeatures = TrainingFeatures[0].Length;
            _weights = new double[nFeatures];
            _bias = 0;

            for (int epoch = 0; epoch < _epochs; epoch++)
            {
                for (int i = 0; i < TrainingFeatures.Length; i++)
                {
                    double prediction = DotProduct(_weights, TrainingFeatures[i]) + _bias;
                    double condition = binaryLabels[i] * prediction;

                    if (condition >= 1)
                    {
                        for (int j = 0; j < _weights.Length; j++)
                        {
                            _weights[j] -= _learningRate * (2 * _lambda * _weights[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _weights.Length; j++)
                        {
                            _weights[j] -= _learningRate * (2 * _lambda * _weights[j] -
                                        _c * binaryLabels[i] * TrainingFeatures[i][j]);
                        }
                        _bias -= _learningRate * (-_c * binaryLabels[i]);
                    }
                }

                if (epoch % 100 == 0)
                {
                    _learningRate *= 0.9;
                }
            }
        }

        protected override double PredictInternal(double[] features)
        {
            double prediction = DotProduct(_weights, features) + _bias;
            return prediction >= 0 ? 1 : 0;
        }

        private double DotProduct(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }

        public double[] GetWeights() => _weights?.ToArray();
        public double GetBias() => _bias;
        public double GetMargin() => _weights == null ? 0 : 1.0 / (Math.Sqrt(_weights.Sum(w => w * w)) + 1e-8);
    }
}