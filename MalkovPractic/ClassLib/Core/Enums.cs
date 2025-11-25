namespace Algorithms.Core
{
    public enum KernelType
    {
        Linear,
        Gaussian,
        Epanechnikov
    }

    public enum DistanceMetric
    {
        Euclidean,
        Manhattan,
        Cosine
    }

    public enum ProblemType
    {
        Classification,
        Regression
    }
}