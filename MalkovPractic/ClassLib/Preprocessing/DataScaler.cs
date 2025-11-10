using System;
using System.Linq;

namespace MLAlgorithms.Preprocessing
{
    public class DataScaler
    {
        private double[] _means;
        private double[] _stds;
        private double[] _mins;
        private double[] _maxs;
        private bool _isFitted;

        public double[][] FitTransform(double[][] data, bool useStandardization = true)
        {
            if (data == null || data.Length == 0)
                return data;

            int nFeatures = data[0].Length;
            _means = new double[nFeatures];
            _stds = new double[nFeatures];
            _mins = new double[nFeatures];
            _maxs = new double[nFeatures];

            // Инициализация min/max
            for (int j = 0; j < nFeatures; j++)
            {
                _mins[j] = double.MaxValue;
                _maxs[j] = double.MinValue;
            }

            // Вычисляем статистики
            for (int j = 0; j < nFeatures; j++)
            {
                double sum = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    double value = data[i][j];
                    sum += value;
                    if (value < _mins[j]) _mins[j] = value;
                    if (value > _maxs[j]) _maxs[j] = value;
                }
                _means[j] = sum / data.Length;
            }

            if (useStandardization)
            {
                // Стандартизация (z-score)
                for (int j = 0; j < nFeatures; j++)
                {
                    double sumSquares = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        sumSquares += Math.Pow(data[i][j] - _means[j], 2);
                    }
                    _stds[j] = Math.Sqrt(sumSquares / data.Length);
                    if (_stds[j] == 0) _stds[j] = 1;
                }
            }

            _isFitted = true;
            return Transform(data, useStandardization);
        }

        public double[][] Transform(double[][] data, bool useStandardization = true)
        {
            if (!_isFitted)
                throw new InvalidOperationException("Scaler must be fitted first");

            var result = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new double[data[i].Length];
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (useStandardization)
                    {
                        result[i][j] = (_stds[j] == 0) ? 0 : (data[i][j] - _means[j]) / _stds[j];
                    }
                    else
                    {
                        // Min-Max нормализация
                        double range = _maxs[j] - _mins[j];
                        result[i][j] = (range == 0) ? 0 : (data[i][j] - _mins[j]) / range;
                    }
                }
            }
            return result;
        }

        public double[] Transform(double[] data, bool useStandardization = true)
        {
            return Transform(new double[][] { data }, useStandardization)[0];
        }
    }
}