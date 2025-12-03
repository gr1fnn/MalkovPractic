using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Core;

namespace Algorithms.Algorithms
{
    public class KNN : BaseAlgorithm
    {
        protected int _k;
        protected DistanceMetric _distanceMetric;

        // Кэш для быстрого поиска
        private Dictionary<int, List<int>> _classSamples;

        public KNN(int k = 3, DistanceMetric distanceMetric = DistanceMetric.Euclidean)
        {
            _k = k;
            _distanceMetric = distanceMetric;
        }

        protected override void PrepareLabels(double[] labels)
        {
            base.PrepareLabels(labels);

            // Создаем индекс: класс -> список индексов образцов
            _classSamples = new Dictionary<int, List<int>>();
            for (int i = 0; i < NumClasses; i++)
            {
                _classSamples[i] = new List<int>();
            }

            for (int i = 0; i < IntTrainingLabels.Length; i++)
            {
                _classSamples[IntTrainingLabels[i]].Add(i);
            }
        }

        protected override double PredictInternal(double[] features)
        {
            if (_k > TrainingFeatures.Length)
            {
                Console.WriteLine($"Предупреждение: k={_k} больше количества образцов={TrainingFeatures.Length}. Использую k={TrainingFeatures.Length}");
                _k = TrainingFeatures.Length;
            }

            var distances = new List<(double distance, int classIndex, int sampleIndex)>();

            // Ускоренный поиск: сначала проверяем образцы из каждого класса
            foreach (var classEntry in _classSamples)
            {
                foreach (var sampleIndex in classEntry.Value)
                {
                    double distance = CalculateDistance(features, TrainingFeatures[sampleIndex], _distanceMetric);
                    distances.Add((distance, classEntry.Key, sampleIndex));
                }
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(_k)
                .ToList();

            // Отладка
            Console.WriteLine($"\n[KNN] Найдено {nearestNeighbors.Count} ближайших соседей:");
            var neighborGroups = nearestNeighbors.GroupBy(n => n.classIndex);
            foreach (var group in neighborGroups.OrderByDescending(g => g.Count()))
            {
                double avgDistance = group.Average(n => n.distance);
                Console.WriteLine($"  Класс {GetOriginalLabel(group.Key)}: {group.Count()} соседей, среднее расстояние: {avgDistance:F4}");
            }

            // Голосование с учетом расстояний
            return PredictWithWeightedVoting(nearestNeighbors);
        }

        private double PredictWithWeightedVoting(List<(double distance, int classIndex, int sampleIndex)> neighbors)
        {
            var classWeights = new Dictionary<int, double>();

            foreach (var neighbor in neighbors)
            {
                // Вес = 1 / (расстояние + небольшое значение для избежания деления на 0)
                double weight = 1.0 / (neighbor.distance + 0.0001);

                if (classWeights.ContainsKey(neighbor.classIndex))
                    classWeights[neighbor.classIndex] += weight;
                else
                    classWeights[neighbor.classIndex] = weight;
            }

            // Находим класс с максимальным весом
            var bestClass = classWeights.OrderByDescending(cw => cw.Value).First();

            Console.WriteLine($"\n[KNN] Результат голосования:");
            foreach (var classWeight in classWeights.OrderByDescending(cw => cw.Value))
            {
                Console.WriteLine($"  Класс {GetOriginalLabel(classWeight.Key)}: вес = {classWeight.Value:F4}");
            }
            Console.WriteLine($"[KNN] Предсказанный класс: {GetOriginalLabel(bestClass.Key)} (вес: {bestClass.Value:F4})");

            return GetOriginalLabel(bestClass.Key);
        }

        public (double[] distances, double[] labels, int[] indices) GetNeighbors(double[] features, int k = -1)
        {
            if (k <= 0) k = _k;
            if (k > TrainingFeatures.Length) k = TrainingFeatures.Length;

            var distances = new List<(double distance, int classIndex, int sampleIndex)>();

            for (int i = 0; i < TrainingFeatures.Length; i++)
            {
                double distance = CalculateDistance(features, TrainingFeatures[i], _distanceMetric);
                distances.Add((distance, IntTrainingLabels[i], i));
            }

            var nearestNeighbors = distances
                .OrderBy(d => d.distance)
                .Take(k)
                .ToList();

            return (
                nearestNeighbors.Select(n => n.distance).ToArray(),
                nearestNeighbors.Select(n => GetOriginalLabel(n.classIndex)).ToArray(),
                nearestNeighbors.Select(n => n.sampleIndex).ToArray()
            );
        }

        // Метод для нахождения оптимального k
        public int FindOptimalK(int maxK = 20)
        {
            if (TrainingFeatures.Length < 10)
                return Math.Min(3, TrainingFeatures.Length);

            int optimalK = 3;
            double bestAccuracy = 0;

            // Простая кросс-валидация
            int foldSize = Math.Max(5, TrainingFeatures.Length / 5);

            for (int k = 1; k <= Math.Min(maxK, TrainingFeatures.Length - foldSize); k++)
            {
                double totalAccuracy = 0;
                int folds = 0;

                // Простая проверка на нескольких фолдах
                for (int fold = 0; fold < Math.Min(5, TrainingFeatures.Length / foldSize); fold++)
                {
                    // Разделяем данные
                    var testIndices = Enumerable.Range(fold * foldSize, foldSize).ToList();
                    var trainIndices = Enumerable.Range(0, TrainingFeatures.Length)
                        .Where(i => !testIndices.Contains(i))
                        .ToList();

                    // Создаем временный KNN с текущим k
                    var tempKnn = new KNN(k, _distanceMetric);

                    // Обучаем на тренировочных данных
                    var trainFeatures = trainIndices.Select(i => TrainingFeatures[i]).ToArray();
                    var trainLabels = trainIndices.Select(i => TrainingLabels[i]).ToArray();
                    tempKnn.Train(trainFeatures, trainLabels, false);

                    // Тестируем
                    int correct = 0;
                    foreach (int testIdx in testIndices)
                    {
                        double prediction = tempKnn.Predict(TrainingFeatures[testIdx]);
                        if (Math.Abs(prediction - TrainingLabels[testIdx]) < 0.001)
                            correct++;
                    }

                    totalAccuracy += (double)correct / testIndices.Count;
                    folds++;
                }

                double avgAccuracy = totalAccuracy / folds;
                Console.WriteLine($"  k={k,2}: точность = {avgAccuracy:P2}");

                if (avgAccuracy > bestAccuracy)
                {
                    bestAccuracy = avgAccuracy;
                    optimalK = k;
                }
            }

            Console.WriteLine($"\n[KNN] Оптимальное k найдено: {optimalK} (точность: {bestAccuracy:P2})");
            return optimalK;
        }
    }
}