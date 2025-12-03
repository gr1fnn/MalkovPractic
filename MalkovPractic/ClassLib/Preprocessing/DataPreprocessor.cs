using Algorithms.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Algorithms.Preprocessing
{
    public class DefaultDataPreprocessor : IDataPreprocessor
    {
        private readonly Dictionary<string, Dictionary<string, double>> _categoryMappings;
        private readonly Dictionary<string, List<string>> _uniqueValues; // Для хранения уникальных значений

        public DefaultDataPreprocessor()
        {
            _categoryMappings = new Dictionary<string, Dictionary<string, double>>();
            _uniqueValues = new Dictionary<string, List<string>>();
        }

        public double[][] PreprocessFeatures(string[][] rawData, int[] featureColumns)
        {
            if (rawData == null || rawData.Length == 0)
                return new double[0][];

            // Сначала собираем уникальные значения для каждого столбца
            CollectUniqueValues(rawData, featureColumns);

            var features = new List<double[]>();

            foreach (var row in rawData)
            {
                var featureRow = new List<double>();

                foreach (int col in featureColumns)
                {
                    if (col < row.Length)
                    {
                        double value = ConvertToNumeric(row[col], col);
                        featureRow.Add(value);
                    }
                    else
                    {
                        featureRow.Add(0);
                    }
                }

                features.Add(featureRow.ToArray());
            }

            return features.ToArray();
        }

        public double[] PreprocessLabels(string[][] rawData, int labelColumn)
        {
            if (rawData == null || rawData.Length == 0)
                return new double[0];

            // Собираем уникальные значения для целевой переменной
            CollectUniqueValuesForColumn(rawData, labelColumn);

            var labels = new List<double>();

            foreach (var row in rawData)
            {
                if (labelColumn < row.Length)
                {
                    double value = ConvertToNumeric(row[labelColumn], labelColumn);
                    labels.Add(value);
                }
                else
                {
                    labels.Add(0);
                }
            }

            return labels.ToArray();
        }

        public Dictionary<string, double>[] PreprocessCategorical(string[][] rawData, int[] categoricalColumns)
        {
            var result = new Dictionary<string, double>[rawData.Length];

            for (int i = 0; i < rawData.Length; i++)
            {
                result[i] = new Dictionary<string, double>();

                foreach (int col in categoricalColumns)
                {
                    if (col < rawData[i].Length)
                    {
                        string value = rawData[i][col];
                        string columnKey = $"col_{col}";

                        if (!_categoryMappings.ContainsKey(columnKey))
                        {
                            _categoryMappings[columnKey] = new Dictionary<string, double>();
                        }

                        if (!_categoryMappings[columnKey].ContainsKey(value))
                        {
                            _categoryMappings[columnKey][value] = _categoryMappings[columnKey].Count;
                        }

                        result[i][$"cat_{col}_{value}"] = _categoryMappings[columnKey][value];
                    }
                }
            }

            return result;
        }

        // Делаем метод публичным для использования в форме
        public double ConvertToNumeric(string value, int columnIndex)
        {
            // Пытаемся преобразовать как число
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue))
            {
                return numericValue;
            }

            // Преобразуем категориальные значения
            string columnKey = $"col_{columnIndex}";
            if (!_categoryMappings.ContainsKey(columnKey))
            {
                _categoryMappings[columnKey] = new Dictionary<string, double>();
            }

            if (!_categoryMappings[columnKey].ContainsKey(value))
            {
                // Для новых значений, которых не было в обучающих данных
                _categoryMappings[columnKey][value] = _categoryMappings[columnKey].Count;
            }

            return _categoryMappings[columnKey][value];
        }

        // Новый метод для определения типа столбца
        public bool IsColumnNumeric(string[][] rawData, int columnIndex)
        {
            if (rawData == null || rawData.Length == 0)
                return false;

            // Берем первые 20 строк для анализа или все, если меньше
            int sampleSize = Math.Min(20, rawData.Length);
            int numericCount = 0;
            int nonNumericCount = 0;

            for (int i = 0; i < sampleSize; i++)
            {
                if (columnIndex < rawData[i].Length)
                {
                    string value = rawData[i][columnIndex];
                    if (string.IsNullOrWhiteSpace(value))
                        continue;

                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                    {
                        numericCount++;
                    }
                    else
                    {
                        nonNumericCount++;
                    }
                }
            }

            // Если больше 80% значений числовые - считаем столбец числовым
            return numericCount > 0 && (numericCount * 1.0 / (numericCount + nonNumericCount)) > 0.8;
        }

        // Метод для получения уникальных значений столбца
        public List<string> GetUniqueValuesForColumn(string[][] rawData, int columnIndex)
        {
            var uniqueValues = new List<string>();

            if (rawData == null || columnIndex < 0)
                return uniqueValues;

            var valuesSet = new HashSet<string>();

            foreach (var row in rawData)
            {
                if (columnIndex < row.Length)
                {
                    string value = row[columnIndex];
                    if (!string.IsNullOrWhiteSpace(value) && !valuesSet.Contains(value))
                    {
                        valuesSet.Add(value);
                    }
                }
            }

            uniqueValues = valuesSet.ToList();
            uniqueValues.Sort();

            // Сохраняем для будущего использования
            string columnKey = $"col_{columnIndex}";
            _uniqueValues[columnKey] = uniqueValues;

            return uniqueValues;
        }

        // Вспомогательный метод для сбора уникальных значений
        private void CollectUniqueValues(string[][] rawData, int[] columns)
        {
            foreach (int col in columns)
            {
                GetUniqueValuesForColumn(rawData, col);
            }
        }

        // Вспомогательный метод для одного столбца
        private void CollectUniqueValuesForColumn(string[][] rawData, int columnIndex)
        {
            GetUniqueValuesForColumn(rawData, columnIndex);
        }

        // Метод для получения всех категориальных маппингов
        public Dictionary<string, Dictionary<string, double>> GetCategoryMappings()
        {
            return _categoryMappings;
        }

        // Метод для получения маппинга конкретного столбца
        public Dictionary<string, double> GetColumnMapping(int columnIndex)
        {
            string columnKey = $"col_{columnIndex}";
            return _categoryMappings.ContainsKey(columnKey) ? _categoryMappings[columnKey] : new Dictionary<string, double>();
        }

        // Метод для получения обратного маппинга (число -> категория)
        public Dictionary<double, string> GetReverseColumnMapping(int columnIndex)
        {
            var forwardMapping = GetColumnMapping(columnIndex);
            var reverseMapping = new Dictionary<double, string>();

            foreach (var kvp in forwardMapping)
            {
                reverseMapping[kvp.Value] = kvp.Key;
            }

            return reverseMapping;
        }

        // Метод для получения названия категории по числу
        public string GetCategoryName(int columnIndex, double numericValue)
        {
            try
            {
                var reverseMapping = GetReverseColumnMapping(columnIndex);

                if (reverseMapping == null || reverseMapping.Count == 0)
                    return $"Unknown (нет маппинга для колонки {columnIndex})";

                // Ищем ближайшее целое значение
                int intValue = (int)Math.Round(numericValue);

                // Сначала пробуем точное совпадение
                if (reverseMapping.ContainsKey(intValue))
                    return reverseMapping[intValue];

                // Ищем ближайшее значение в маппинге
                var nearestKey = reverseMapping.Keys
                    .OrderBy(k => Math.Abs(k - intValue))
                    .FirstOrDefault();

                if (nearestKey != default)
                    return reverseMapping[nearestKey];

                return $"Unknown (класс {intValue}) - нет в маппинге";
            }
            catch (Exception ex)
            {
                return $"Unknown (ошибка: {ex.Message})";
            }
        }

        // Добавляем метод для отладки
        public string DebugMappings()
        {
            var result = "=== ОТЛАДКА МАППИНГОВ ===\n";
            foreach (var columnKvp in _categoryMappings)
            {
                result += $"\nКолонка {columnKvp.Key}:\n";
                foreach (var valueKvp in columnKvp.Value)
                {
                    result += $"  '{valueKvp.Key}' -> {valueKvp.Value}\n";
                }
            }
            return result;
        }
    }
}
