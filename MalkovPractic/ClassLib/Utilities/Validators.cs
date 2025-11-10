using System;

namespace MLAlgorithms.Utilities
{
    public static class Validators
    {
        public static void ValidateFeatures(double[][] features)
        {
            if (features == null || features.Length == 0)
                throw new ArgumentException("Features cannot be null or empty");

            int expectedLength = features[0].Length;
            for (int i = 1; i < features.Length; i++)
            {
                if (features[i].Length != expectedLength)
                    throw new ArgumentException("All feature vectors must have the same length");
            }
        }

        public static void ValidateLabels(double[] labels)
        {
            if (labels == null || labels.Length == 0)
                throw new ArgumentException("Labels cannot be null or empty");
        }

        public static void ValidateFeatureVector(double[] features, int expectedLength)
        {
            if (features == null)
                throw new ArgumentException("Feature vector cannot be null");

            if (features.Length != expectedLength)
                throw new ArgumentException($"Feature vector must have length {expectedLength}");
        }

        public static void ValidateKNNParameters(int k, int sampleCount)
        {
            if (k <= 0)
                throw new ArgumentException("K must be positive");

            if (k > sampleCount)
                throw new ArgumentException("K cannot be larger than the number of samples");
        }

        public static void ValidateBandwidth(double bandwidth)
        {
            if (bandwidth <= 0)
                throw new ArgumentException("Bandwidth must be positive");
        }
    }
}