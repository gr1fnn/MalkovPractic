using System;

namespace MLAlgorithms.Core
{
    public interface IAlgorithm
    {
        void Train(double[][] features, double[] labels, bool normalize = true);
        double Predict(double[] features);
        void SaveModel(string filePath);
        void LoadModel(string filePath);
    }

    public interface IDataPreprocessor
    {
        double[][] PreprocessFeatures(string[][] rawData, int[] featureColumns);
        double[] PreprocessLabels(string[][] rawData, int labelColumn);
        Dictionary<string, double>[] PreprocessCategorical(string[][] rawData, int[] categoricalColumns);
    }

    public interface IModelEvaluator
    {
        double CalculateAccuracy(double[] predictions, double[] actual);
        double CalculateRMSE(double[] predictions, double[] actual);
        double CalculateMAE(double[] predictions, double[] actual);
    }
}