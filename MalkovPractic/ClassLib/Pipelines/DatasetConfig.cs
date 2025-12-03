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

    }
}