using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core;
using Algorithms.Algorithms;

namespace Algorithms.Pipelines
{
    public class DatasetConfig
    {
        public string Name { get; set; }
        public int[] FeatureColumns { get; set; }
        public int LabelColumn { get; set; }
        public bool HasHeader { get; set; }
        public Type AlgorithmType { get; set; }
        public ProblemType ProblemType { get; set; }
        public Dictionary<string, object> AlgorithmParameters { get; set; }

        public DatasetConfig()
        {
            AlgorithmParameters = new Dictionary<string, object>();
        }

        public BaseAlgorithm CreateAlgorithm()
        {
            var algorithm = (BaseAlgorithm)Activator.CreateInstance(AlgorithmType);

            // Устанавливаем параметры алгоритма
            foreach (var param in AlgorithmParameters)
            {
                var property = AlgorithmType.GetProperty(param.Key);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(algorithm, param.Value);
                }
            }

            return algorithm;
        }

        // Предустановленные конфигурации для разных датасетов
        public static readonly DatasetConfig CarConfig = new DatasetConfig
        {
            Name = "Cars",
            FeatureColumns = new int[] { 2, 3, 6 }, // Year, Engine Size, Mileage
            LabelColumn = 8, // Price
            HasHeader = true,
            AlgorithmType = typeof(NadarayaWatson),
            ProblemType = ProblemType.Regression,
            AlgorithmParameters = new Dictionary<string, object>
            {
                { "Bandwidth", 0.5 }
            }
        };

        public static readonly DatasetConfig SteelConfig = new DatasetConfig
        {
            Name = "Steel Defects",
            FeatureColumns = Enumerable.Range(0, 27).ToArray(),
            LabelColumn = 27, // First defect type
            HasHeader = true,
            AlgorithmType = typeof(KNN),
            ProblemType = ProblemType.Classification,
            AlgorithmParameters = new Dictionary<string, object>
            {
                { "K", 5 },
                { "DistanceMetric", DistanceMetric.Euclidean }
            }
        };

        public static readonly DatasetConfig EnergyConfig = new DatasetConfig
        {
            Name = "Energy",
            FeatureColumns = new int[] { 2, 3, 4, 6, 7, 8 }, // temp, humidity, co2, renewable, population, industry
            LabelColumn = 5, // Energy consumption
            HasHeader = true,
            AlgorithmType = typeof(NadarayaWatson),
            ProblemType = ProblemType.Regression,
            AlgorithmParameters = new Dictionary<string, object>
            {
                { "KernelType", KernelType.Gaussian },
                { "Bandwidth", 0.8 }
            }
        };
    }
}