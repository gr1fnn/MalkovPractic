using System;
using System.Linq;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class NadarayaWatson : BaseAlgorithm
    {
        private KernelType _kernelType;
        private double _bandwidth;

        public NadarayaWatson(KernelType kernelType = KernelType.Gaussian, double bandwidth = 1.0)
        {
            _kernelType = kernelType;
            _bandwidth = bandwidth;
        }

        protected override double PredictInternal(double[] features)
        {
            double numerator = 0;
            double denominator = 0;

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateEuclideanDistance(features, TrainingFeatures[i]);
                double kernelValue = CalculateKernel(distance / _bandwidth);

                numerator += TrainingLabels[i] * kernelValue;
                denominator += kernelValue;
            }

            return denominator == 0 ? 0 : numerator / denominator;
        }

        private double CalculateKernel(double u)
        {
            return _kernelType switch
            {
                KernelType.Linear => LinearKernel(u),
                KernelType.Gaussian => GaussianKernel(u),
                KernelType.Epanechnikov => EpanechnikovKernel(u),
                _ => GaussianKernel(u)
            };
        }

        private double LinearKernel(double u)
        {
            return Math.Max(0, 1 - Math.Abs(u));
        }

        private double GaussianKernel(double u)
        {
            return Math.Exp(-0.5 * u * u) / Math.Sqrt(2 * Math.PI);
        }

        private double EpanechnikovKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 0.75 * (1 - u * u) : 0;
        }

        public void SetBandwidth(double bandwidth) => _bandwidth = bandwidth;
        public void SetKernelType(KernelType kernelType) => _kernelType = kernelType;
    }
}