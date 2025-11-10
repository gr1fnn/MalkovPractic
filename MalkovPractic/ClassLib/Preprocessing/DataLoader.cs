using MLAlgorithms.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MLAlgorithms.Preprocessing
{
    public class DataLoader
    {
        public static string[][] LoadCSV(string filePath, bool hasHeader = true, char separator = ',')
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var lines = File.ReadAllLines(filePath);
            var data = new List<string[]>();

            int startIndex = hasHeader ? 1 : 0;

            for (int i = startIndex; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                var values = lines[i].Split(separator)
                    .Select(v => v.Trim('\"', ' ', '\t'))
                    .ToArray();

                data.Add(values);
            }

            return data.ToArray();
        }

        public static (double[][] features, double[] labels) PrepareData(
            string[][] rawData,
            int[] featureColumns,
            int labelColumn,
            IDataPreprocessor preprocessor = null)
        {
            preprocessor ??= new DefaultDataPreprocessor();

            var features = preprocessor.PreprocessFeatures(rawData, featureColumns);
            var labels = preprocessor.PreprocessLabels(rawData, labelColumn);

            return (features, labels);
        }

        public static string[] GetColumnNames(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var firstLine = File.ReadLines(filePath).First();
            return firstLine.Split(',').Select(c => c.Trim('\"', ' ')).ToArray();
        }
    }
}