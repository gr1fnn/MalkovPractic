using System.Collections.Generic;

namespace MLAlgorithms.Core
{
    public class Dataset
    {
        public double[][] Features { get; set; }
        public double[] Labels { get; set; }
        public string[] FeatureNames { get; set; }
        public ProblemType Type { get; set; }

        public Dataset(double[][] features, double[] labels, string[] featureNames = null, ProblemType type = ProblemType.Regression)
        {
            Features = features;
            Labels = labels;
            FeatureNames = featureNames;
            Type = type;
        }
    }

    public class TrainingResult
    {
        public double TrainingTime { get; set; }
        public double Accuracy { get; set; }
        public double RMSE { get; set; }
        public double MAE { get; set; } 
        public Dictionary<string, double> AdditionalMetrics { get; set; }

        public TrainingResult()
        {
            AdditionalMetrics = new Dictionary<string, double>();
            Accuracy = 0;
            RMSE = 0;
            MAE = 0;
            TrainingTime = 0;
        }
    }
}