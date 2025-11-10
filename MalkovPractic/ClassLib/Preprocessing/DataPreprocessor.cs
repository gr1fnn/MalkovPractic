using MLAlgorithms.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MLAlgorithms.Preprocessing
{
    public class DefaultDataPreprocessor : IDataPreprocessor
    {
        private readonly Dictionary<string, Dictionary<string, double>> _categoryMappings;

        public DefaultDataPreprocessor()
        {
            _categoryMappings = new Dictionary<string, Dictionary<string, double>>();
        }

        public double[][] PreprocessFeatures(string[][] rawData, int[] featureColumns)
        {
            if (rawData == null || rawData.Length == 0)
                return new double[0][];

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

        private double ConvertToNumeric(string value, int columnIndex)
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
                _categoryMappings[columnKey][value] = _categoryMappings[columnKey].Count;
            }

            return _categoryMappings[columnKey][value];
        }
    }
}