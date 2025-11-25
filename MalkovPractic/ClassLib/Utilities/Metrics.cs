using System;
using System.Linq;

namespace Algorithms.Utilities
{
    public static class Metrics
    {
        public static double Accuracy(double[] predictions, double[] actual)
        {
            int correct = predictions.Zip(actual, (p, a) => Math.Abs(p - a) < 0.5 ? 1 : 0).Sum();
            return (double)correct / predictions.Length;
        }

        public static double Precision(double[] predictions, double[] actual, double positiveClass = 1)
        {
            int truePositives = 0;
            int falsePositives = 0;

            for (int i = 0; i < predictions.Length; i++)
            {
                if (Math.Abs(predictions[i] - positiveClass) < 0.5)
                {
                    if (Math.Abs(actual[i] - positiveClass) < 0.5)
                        truePositives++;
                    else
                        falsePositives++;
                }
            }

            return truePositives + falsePositives == 0 ? 0 : (double)truePositives / (truePositives + falsePositives);
        }

        public static double Recall(double[] predictions, double[] actual, double positiveClass = 1)
        {
            int truePositives = 0;
            int falseNegatives = 0;

            for (int i = 0; i < predictions.Length; i++)
            {
                if (Math.Abs(actual[i] - positiveClass) < 0.5)
                {
                    if (Math.Abs(predictions[i] - positiveClass) < 0.5)
                        truePositives++;
                    else
                        falseNegatives++;
                }
            }

            return truePositives + falseNegatives == 0 ? 0 : (double)truePositives / (truePositives + falseNegatives);
        }

        public static double F1Score(double[] predictions, double[] actual, double positiveClass = 1)
        {
            double precision = Precision(predictions, actual, positiveClass);
            double recall = Recall(predictions, actual, positiveClass);

            return precision + recall == 0 ? 0 : 2 * precision * recall / (precision + recall);
        }

        public static double RMSE(double[] predictions, double[] actual)
        {
            double sum = predictions.Zip(actual, (p, a) => Math.Pow(p - a, 2)).Sum();
            return Math.Sqrt(sum / predictions.Length);
        }

        public static double MAE(double[] predictions, double[] actual)
        {
            double sum = predictions.Zip(actual, (p, a) => Math.Abs(p - a)).Sum();
            return sum / predictions.Length;
        }

        public static double R2Score(double[] predictions, double[] actual)
        {
            double actualMean = actual.Average();
            double totalSumSquares = actual.Sum(a => Math.Pow(a - actualMean, 2));
            double residualSumSquares = predictions.Zip(actual, (p, a) => Math.Pow(a - p, 2)).Sum();

            return 1 - (residualSumSquares / totalSumSquares);
        }
    }
}