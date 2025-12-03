using System;
using System.Linq;

namespace Algorithms.Preprocessing
{
    public enum NormalizationType
    {
        None,       // Без нормирования
        MinMax,     // Линейное 
        ZScore,     // Статистическое 
        LogScale    // Логарифмическое
    }

    public class DataScaler
    {
        private double[] _means;
        private double[] _stds;
        private double[] _mins;
        private double[] _maxs;
        private bool _isFitted;
        private NormalizationType _normalizationType;

        public DataScaler(NormalizationType type = NormalizationType.ZScore)
        {
            _normalizationType = type;
        }

        /// <summary>
        /// Вычисление статистик и преобразование данных
        /// </summary>
        public double[][] FitTransform(double[][] data)
        {
            if (data == null || data.Length == 0)
                return data;

            if (_normalizationType == NormalizationType.None)
            {
                // Если без нормирования, просто возвращаем исходные данные
                _isFitted = true;
                return data;
            }

            int nFeatures = data[0].Length;
            ComputeStatistics(data, nFeatures);
            return Transform(data);
        }

        /// <summary>
        /// Вычисление всех необходимых статистик
        /// </summary>
        private void ComputeStatistics(double[][] data, int nFeatures)
        {
            _means = new double[nFeatures];
            _stds = new double[nFeatures];
            _mins = new double[nFeatures];
            _maxs = new double[nFeatures];

            for (int j = 0; j < nFeatures; j++)
            {
                _mins[j] = double.MaxValue;
                _maxs[j] = double.MinValue;
            }

            for (int j = 0; j < nFeatures; j++)
            {
                double sum = 0;
                double sumSquares = 0;

                for (int i = 0; i < data.Length; i++)
                {
                    double value = data[i][j];

                    sum += value;

                    if (value < _mins[j]) _mins[j] = value;
                    if (value > _maxs[j]) _maxs[j] = value;
                }

                _means[j] = sum / data.Length;

                for (int i = 0; i < data.Length; i++)
                {
                    double diff = data[i][j] - _means[j];
                    sumSquares += diff * diff;
                }

                _stds[j] = Math.Sqrt(sumSquares / data.Length);
                if (_stds[j] == 0) _stds[j] = 1;
            }

            _isFitted = true;
        }

        /// <summary>
        /// Преобразование данных согласно выбранному типу нормализации
        /// </summary>
        public double[][] Transform(double[][] data)
        {
            if (!_isFitted)
                throw new InvalidOperationException("Scaler must be fitted first");

            if (_normalizationType == NormalizationType.None)
            {
                // Без нормирования - возвращаем исходные данные
                return data;
            }

            var result = new double[data.Length][];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new double[data[i].Length];

                for (int j = 0; j < data[i].Length; j++)
                {
                    result[i][j] = NormalizeValue(data[i][j], j);
                }
            }

            return result;
        }

        /// <summary>
        /// Преобразование одного значения
        /// </summary>
        public double[] Transform(double[] data)
        {
            return Transform(new double[][] { data })[0];
        }

        /// <summary>
        /// Применение выбранного метода нормализации
        /// </summary>
        private double NormalizeValue(double value, int featureIndex)
        {
            return _normalizationType switch
            {
                // 0. Без нормирования
                NormalizationType.None => value,

                // 1. Линейное (Min-Max) нормирование
                NormalizationType.MinMax => MinMaxNormalize(value, featureIndex),

                // 2. Статистическое (Z-Score) нормирование
                NormalizationType.ZScore => ZScoreNormalize(value, featureIndex),

                // 3. Логарифмическое нормирование
                NormalizationType.LogScale => LogScaleNormalize(value, featureIndex),

                _ => value // По умолчанию без нормирования
            };
        }

        #region Методы нормирования

        /// <summary>
        /// 1. Линейное нормирование (Min-Max)
        /// Диапазон [0, 1]
        /// </summary>
        private double MinMaxNormalize(double value, int featureIndex)
        {
            double range = _maxs[featureIndex] - _mins[featureIndex];
            if (Math.Abs(range) < double.Epsilon)
                return 0;

            return (value - _mins[featureIndex]) / range;
        }

        /// <summary>
        /// 2. Статистическое нормирование (Z-Score)
        /// Среднее = 0, Стандартное отклонение = 1
        /// </summary>
        private double ZScoreNormalize(double value, int featureIndex)
        {
            if (_stds[featureIndex] == 0)
                return 0;

            return (value - _means[featureIndex]) / _stds[featureIndex];
        }

        /// <summary>
        /// 3. Логарифмическое нормирование
        /// Полезно для данных с экспоненциальным распределением
        /// </summary>
        private double LogScaleNormalize(double value, int featureIndex)
        {
            // Сдвигаем данные, чтобы избежать логарифма от отрицательных чисел
            double shift = _mins[featureIndex] <= 0 ? Math.Abs(_mins[featureIndex]) + 1 : 0;
            double shiftedValue = value + shift;

            // Применяем логарифм (log(x+1) для избежания log(0))
            double logValue = Math.Log(shiftedValue + 1);

            // Нормализуем к диапазону [0, 1]
            double maxShiftedValue = _maxs[featureIndex] + shift;
            double maxLogValue = Math.Log(maxShiftedValue + 1);

            if (Math.Abs(maxLogValue) < double.Epsilon)
                return 0;

            return logValue / maxLogValue;
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Обратное преобразование (денормализация)
        /// </summary>
        public double InverseTransform(double normalizedValue, int featureIndex)
        {
            if (!_isFitted)
                throw new InvalidOperationException("Scaler must be fitted first");

            if (_normalizationType == NormalizationType.None)
            {
                // Без нормирования - возвращаем как есть
                return normalizedValue;
            }

            return _normalizationType switch
            {
                NormalizationType.MinMax => InverseMinMax(normalizedValue, featureIndex),
                NormalizationType.ZScore => InverseZScore(normalizedValue, featureIndex),
                NormalizationType.LogScale => InverseLogScale(normalizedValue, featureIndex),
                _ => normalizedValue
            };
        }

        private double InverseMinMax(double normalized, int featureIndex)
        {
            double range = _maxs[featureIndex] - _mins[featureIndex];
            return normalized * range + _mins[featureIndex];
        }

        private double InverseZScore(double normalized, int featureIndex)
        {
            return normalized * _stds[featureIndex] + _means[featureIndex];
        }

        private double InverseLogScale(double normalized, int featureIndex)
        {
            // Вычисляем максимальное логарифмическое значение
            double shift = _mins[featureIndex] <= 0 ? Math.Abs(_mins[featureIndex]) + 1 : 0;
            double maxShiftedValue = _maxs[featureIndex] + shift;
            double maxLogValue = Math.Log(maxShiftedValue + 1);

            // Восстанавливаем логарифмическое значение
            double logValue = normalized * maxLogValue;

            // Восстанавливаем исходное значение
            double shiftedValue = Math.Exp(logValue) - 1;
            return shiftedValue - shift;
        }

        /// <summary>
        /// Изменение типа нормализации
        /// </summary>
        public void SetNormalizationType(NormalizationType type)
        {
            _normalizationType = type;
        }

        /// <summary>
        /// Проверка, был ли скалер обучен
        /// </summary>
        public bool IsFitted => _isFitted;

        /// <summary>
        /// Получение статистик по признаку
        /// </summary>
        public (double min, double max, double mean, double std) GetFeatureStats(int featureIndex)
        {
            if (!_isFitted)
                throw new InvalidOperationException("Scaler must be fitted first");

            return (_mins[featureIndex], _maxs[featureIndex],
                    _means[featureIndex], _stds[featureIndex]);
        }

        /// <summary>
        /// Визуализация эффекта нормализации (для отладки)
        /// </summary>
        public string PrintNormalizationEffect(double[][] data, int featureIndex, int samples = 5)
        {
            if (!_isFitted) return "Scaler not fitted";

            if (_normalizationType == NormalizationType.None)
            {
                return "=== ЭФФЕКТ НОРМАЛИЗАЦИИ ===\nТип: Без нормирования\n(Исходные данные используются без изменений)";
            }

            string typeName = _normalizationType switch
            {
                NormalizationType.None => "Без нормирования",
                NormalizationType.MinMax => "Линейное (Min-Max)",
                NormalizationType.ZScore => "Статистическое (Z-Score)",
                NormalizationType.LogScale => "Логарифмическое",
                _ => "Неизвестно"
            };

            var result = $"=== ЭФФЕКТ НОРМАЛИЗАЦИИ ===\n";
            result += $"Тип: {typeName}\n";
            result += $"Признак: {featureIndex}\n\n";
            result += "Исходное → Нормализованное:\n";

            samples = Math.Min(samples, data.Length);
            for (int i = 0; i < samples; i++)
            {
                double original = data[i][featureIndex];
                double normalized = NormalizeValue(original, featureIndex);
                result += $"{original,10:F4} → {normalized,10:F4}\n";
            }

            if (_normalizationType != NormalizationType.None)
            {
                // Добавляем статистики только если есть нормирование
                var stats = GetFeatureStats(featureIndex);
                result += $"\nСтатистики признака:\n";
                result += $"  Минимум: {stats.min:F4}\n";
                result += $"  Максимум: {stats.max:F4}\n";
                result += $"  Среднее: {stats.mean:F4}\n";
                result += $"  Стандартное отклонение: {stats.std:F4}\n";
            }

            return result;
        }

        #endregion
    }
}