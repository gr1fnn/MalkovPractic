using System;
using System.Linq;
using System.Collections.Generic;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class SVM : BaseAlgorithm
    {
        private List<double[]> _weightsList;
        private List<double> _biasesList;
        private double _learningRate;
        private double _lambda;
        private int _epochs;
        private double _c;
        private Random _random;

        public SVM(double learningRate = 0.001, double lambda = 0.01, int epochs = 1000, double c = 1.0)
        {
            _learningRate = learningRate;
            _lambda = lambda;
            _epochs = epochs;
            _c = c;
            _weightsList = new List<double[]>();
            _biasesList = new List<double>();
            _random = new Random();
        }

        protected override double PredictInternal(double[] features)
        {
            if (NumClasses == 2)
            {
                // Бинарная классификация
                double score = DotProduct(_weightsList[0], features) + _biasesList[0];
                return score >= 0 ? UniqueLabels[1] : UniqueLabels[0];
            }
            else
            {
                // Многоклассовая: один против всех
                double maxScore = double.NegativeInfinity;
                int predictedClassIdx = 0;

                for (int i = 0; i < _weightsList.Count; i++)
                {
                    double score = DotProduct(_weightsList[i], features) + _biasesList[i];
                    if (score > maxScore)
                    {
                        maxScore = score;
                        predictedClassIdx = i;
                    }
                }

                return UniqueLabels[predictedClassIdx];
            }
        }

        private void TrainBinary(double[][] features, int[] labels)
        {
            int nFeatures = features[0].Length;
            var weights = new double[nFeatures];
            double bias = 0;

            // Преобразуем метки в -1 и 1
            int positiveClass = 1;
            int negativeClass = -1;

            Console.WriteLine($"\n[SVM] Обучение бинарного классификатора");
            Console.WriteLine($"[SVM] Класс {UniqueLabels[1]} -> {positiveClass}, Класс {UniqueLabels[0]} -> {negativeClass}");

            for (int epoch = 0; epoch < _epochs; epoch++)
            {
                int correct = 0;
                double epochLoss = 0;

                // Перемешиваем данные
                var indices = Enumerable.Range(0, features.Length).ToArray();
                Shuffle(indices);

                foreach (int i in indices)
                {
                    int binaryLabel = labels[i] == 1 ? positiveClass : negativeClass;
                    double prediction = DotProduct(weights, features[i]) + bias;
                    double margin = binaryLabel * prediction;

                    if (margin < 1)
                    {
                        // Обновляем веса
                        for (int j = 0; j < weights.Length; j++)
                        {
                            weights[j] = weights[j] * (1 - _learningRate * _lambda) +
                                        _learningRate * _c * binaryLabel * features[i][j];
                        }
                        bias += _learningRate * _c * binaryLabel;
                        epochLoss += 1 - margin;
                    }
                    else
                    {
                        // Только регуляризация
                        for (int j = 0; j < weights.Length; j++)
                        {
                            weights[j] *= (1 - _learningRate * _lambda);
                        }
                    }

                    // Подсчет точности
                    if ((prediction >= 0 && binaryLabel == 1) || (prediction < 0 && binaryLabel == -1))
                        correct++;
                }

                // Адаптивный learning rate
                if (epoch % 100 == 0 && epoch > 0)
                {
                    _learningRate *= 0.99;
                }

                if (epoch % 100 == 0 || epoch == _epochs - 1)
                {
                    double accuracy = (double)correct / features.Length;
                    double avgLoss = epochLoss / features.Length;
                    Console.WriteLine($"[SVM] Эпоха {epoch,4}: Точность = {accuracy:P2}, Потеря = {avgLoss:F4}");
                }
            }

            _weightsList.Add(weights);
            _biasesList.Add(bias);
        }

        private void TrainOneVsAll(double[][] features, int[] labels)
        {
            Console.WriteLine($"\n[SVM] Обучение {NumClasses} бинарных классификаторов (один против всех)");

            for (int classIdx = 0; classIdx < NumClasses; classIdx++)
            {
                Console.WriteLine($"[SVM] Класс {UniqueLabels[classIdx]} против всех");

                // Бинарные метки для текущего класса
                int[] binaryLabels = new int[labels.Length];
                for (int i = 0; i < labels.Length; i++)
                {
                    binaryLabels[i] = labels[i] == classIdx ? 1 : -1;
                }

                int nFeatures = features[0].Length;
                var weights = new double[nFeatures];
                double bias = 0;
                double currentLR = _learningRate;

                for (int epoch = 0; epoch < _epochs; epoch++)
                {
                    var indices = Enumerable.Range(0, features.Length).ToArray();
                    Shuffle(indices);

                    foreach (int i in indices)
                    {
                        double prediction = DotProduct(weights, features[i]) + bias;
                        double margin = binaryLabels[i] * prediction;

                        if (margin < 1)
                        {
                            for (int j = 0; j < weights.Length; j++)
                            {
                                weights[j] = weights[j] * (1 - currentLR * _lambda) +
                                            currentLR * _c * binaryLabels[i] * features[i][j];
                            }
                            bias += currentLR * _c * binaryLabels[i];
                        }
                        else
                        {
                            for (int j = 0; j < weights.Length; j++)
                            {
                                weights[j] *= (1 - currentLR * _lambda);
                            }
                        }
                    }

                    if (epoch % 100 == 0 && epoch > 0)
                    {
                        currentLR *= 0.99;
                    }
                }

                _weightsList.Add(weights);
                _biasesList.Add(bias);
            }
        }

        private void Shuffle(int[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        private double DotProduct(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }

        public override void Train(double[][] features, double[] labels, bool normalize = true)
        {
            base.Train(features, labels, normalize);

            Console.WriteLine($"\n[SVM] Начинаем обучение SVM");
            Console.WriteLine($"[SVM] {NumClasses} классов, {features.Length} образцов");

            if (NumClasses == 2)
            {
                TrainBinary(TrainingFeatures, IntTrainingLabels);
            }
            else
            {
                TrainOneVsAll(TrainingFeatures, IntTrainingLabels);
            }

            Console.WriteLine($"[SVM] Обучение завершено");
        }

        // Метод для получения вероятностей классов
        public double[] GetClassProbabilities(double[] features)
        {
            double[] scores = new double[NumClasses];

            if (NumClasses == 2)
            {
                double score = DotProduct(_weightsList[0], features) + _biasesList[0];
                double prob = 1.0 / (1.0 + Math.Exp(-score));
                return new double[] { 1 - prob, prob };
            }
            else
            {
                for (int i = 0; i < _weightsList.Count; i++)
                {
                    scores[i] = DotProduct(_weightsList[i], features) + _biasesList[i];
                }

                // Softmax
                double maxScore = scores.Max();
                double sumExp = 0;
                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i] = Math.Exp(scores[i] - maxScore);
                    sumExp += scores[i];
                }

                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i] /= sumExp;
                }

                return scores;
            }
        }
    }
}