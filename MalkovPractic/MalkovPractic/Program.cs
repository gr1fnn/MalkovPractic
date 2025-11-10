using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MLAlgorithms.Pipelines;
using MLAlgorithms.Core;

namespace MLAlgorithms.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== МАШИННОЕ ОБУЧЕНИЕ - ВЫБОР ДАТАСЕТА ===");

            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TrainWithFileSelection();
                        break;
                    case "2":
                        TestPredefinedDatasets();
                        break;
                    case "3":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Выбрать файл для тренировки");
            Console.WriteLine("2. Протестировать предустановленные датасеты");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите опцию: ");
        }

        static void TrainWithFileSelection()
        {
            Console.WriteLine("\n=== ВЫБОР ФАЙЛА ДЛЯ ТРЕНИРОВКИ ===");

            // Получаем список CSV файлов в текущей директории
            var csvFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            if (csvFiles.Length == 0)
            {
                Console.WriteLine("CSV файлы не найдены в текущей директории.");
                Console.WriteLine("Поместите CSV файлы в папку: " + Directory.GetCurrentDirectory());
                return;
            }

            // Показываем список файлов
            Console.WriteLine("\nДоступные CSV файлы:");
            for (int i = 0; i < csvFiles.Length; i++)
            {
                string fileName = Path.GetFileName(csvFiles[i]);
                Console.WriteLine($"{i + 1}. {fileName}");
            }
            Console.WriteLine($"{csvFiles.Length + 1}. Ввести путь вручную");

            // Выбор файла
            Console.Write("\nВыберите файл: ");
            if (int.TryParse(Console.ReadLine(), out int fileChoice))
            {
                string selectedFile;

                if (fileChoice == csvFiles.Length + 1)
                {
                    // Ручной ввод пути
                    Console.Write("Введите путь к CSV файлу: ");
                    selectedFile = Console.ReadLine();
                }
                else if (fileChoice >= 1 && fileChoice <= csvFiles.Length)
                {
                    selectedFile = csvFiles[fileChoice - 1];
                }
                else
                {
                    Console.WriteLine("Неверный выбор файла.");
                    return;
                }

                // Проверяем существование файла
                if (!File.Exists(selectedFile))
                {
                    Console.WriteLine($"Файл не найден: {selectedFile}");
                    return;
                }

                // Выбор конфигурации (простая версия - пользователь выбирает тип)
                SelectAndTrainDataset(selectedFile);
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }

        static void SelectAndTrainDataset(string filePath)
        {
            Console.WriteLine("\n=== ВЫБЕРТЕ ТИП ДАТАСЕТА ===");
            Console.WriteLine("1. Автомобили (регрессия - цена)");
            Console.WriteLine("2. Дефекты стали (классификация)");
            Console.WriteLine("3. Энергетика (регрессия)");
            Console.WriteLine("4. Настроить вручную");
            Console.Write("Выберите тип датасета: ");

            if (int.TryParse(Console.ReadLine(), out int datasetChoice))
            {
                DatasetConfig config = datasetChoice switch
                {
                    1 => DatasetConfig.CarConfig,
                    2 => DatasetConfig.SteelConfig,
                    3 => DatasetConfig.EnergyConfig,
                    4 => CreateCustomConfig(),
                    _ => null
                };

                if (config != null)
                {
                    TrainWithConfig(filePath, config);
                }
                else
                {
                    Console.WriteLine("Неверный выбор конфигурации.");
                }
            }
        }

        static DatasetConfig CreateCustomConfig()
        {
            Console.WriteLine("\n=== РУЧНАЯ НАСТРОЙКА ===");

            var config = new DatasetConfig
            {
                Name = "Custom Dataset",
                HasHeader = true
            };

            // Выбор типа задачи
            Console.WriteLine("Выберите тип задачи:");
            Console.WriteLine("1. Классификация");
            Console.WriteLine("2. Регрессия");
            Console.Write("Ваш выбор: ");

            if (int.TryParse(Console.ReadLine(), out int problemChoice))
            {
                config.ProblemType = problemChoice == 1 ? ProblemType.Classification : ProblemType.Regression;
            }

            // Выбор алгоритма
            Console.WriteLine("\nВыберите алгоритм:");
            Console.WriteLine("1. KNN");
            Console.WriteLine("2. Взвешенный KNN");
            Console.WriteLine("3. Надарая-Ватсон");
            Console.WriteLine("4. SVM");
            Console.WriteLine("5. STOL");
            Console.Write("Ваш выбор: ");

            if (int.TryParse(Console.ReadLine(), out int algoChoice))
            {
                config.AlgorithmType = algoChoice switch
                {
                    1 => typeof(Algorithms.KNN),
                    2 => typeof(Algorithms.WeightedKNN),
                    3 => typeof(Algorithms.NadarayaWatson),
                    4 => typeof(Algorithms.SVM),
                    5 => typeof(Algorithms.STOL),
                    _ => typeof(Algorithms.KNN)
                };
            }

            // Ввод колонок
            Console.Write("Введите номера колонок для признаков (через запятую): ");
            var featureInput = Console.ReadLine();
            config.FeatureColumns = featureInput.Split(',')
                .Select(s => int.TryParse(s.Trim(), out int result) ? result : -1)
                .Where(i => i >= 0)
                .ToArray();

            Console.Write("Введите номер колонки для метки: ");
            if (int.TryParse(Console.ReadLine(), out int labelColumn))
            {
                config.LabelColumn = labelColumn;
            }

            return config;
        }

        static void TrainWithConfig(string filePath, DatasetConfig config)
        {
            try
            {
                Console.WriteLine($"\n=== ОБУЧЕНИЕ НА ФАЙЛЕ: {Path.GetFileName(filePath)} ===");
                Console.WriteLine($"Конфигурация: {config.Name}");
                Console.WriteLine($"Алгоритм: {config.AlgorithmType.Name}");
                Console.WriteLine($"Тип задачи: {config.ProblemType}");

                var pipeline = new MLPipeline(config);
                pipeline.LoadAndTrain(filePath);

                // Тестовое предсказание
                Console.WriteLine("\n=== ТЕСТОВОЕ ПРЕДСКАЗАНИЕ ===");
                Console.WriteLine("Введите значения признаков для предсказания:");

                var testFeatures = new double[config.FeatureColumns.Length];
                for (int i = 0; i < config.FeatureColumns.Length; i++)
                {
                    Console.Write($"Признак {i + 1}: ");
                    if (double.TryParse(Console.ReadLine(), out double value))
                    {
                        testFeatures[i] = value;
                    }
                    else
                    {
                        testFeatures[i] = 0;
                    }
                }

                double prediction = pipeline.Predict(testFeatures);

                if (config.ProblemType == ProblemType.Classification)
                {
                    Console.WriteLine($"\nПредсказанный класс: {prediction}");
                }
                else
                {
                    Console.WriteLine($"\nПредсказанное значение: {prediction:F2}");
                }

                // Сохранение модели
                Console.Write("\nСохранить модель? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    string modelName = $"{Path.GetFileNameWithoutExtension(filePath)}_model.bin";
                    pipeline.SaveModel(modelName);
                    Console.WriteLine($"Модель сохранена как: {modelName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обучении: {ex.Message}");
            }
        }

        // Старые методы для тестирования предустановленных датасетов (без изменений)
        static void TestPredefinedDatasets()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ПРЕДУСТАНОВЛЕННЫХ ДАТАСЕТОВ ===");

            TestCarDataset();
            TestSteelDataset();
            TestCustomDataset();
        }

        static void TestCarDataset()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ДАТАСЕТА АВТОМОБИЛЕЙ ===");

            var pipeline = new MLPipeline(DatasetConfig.CarConfig);

            try
            {
                if (File.Exists("cars.csv"))
                {
                    pipeline.LoadAndTrain("cars.csv");

                    // Тестовое предсказание
                    double[] testFeatures = new double[] { 2019, 2.5, 50000 }; // Year, Engine Size, Mileage
                    double prediction = pipeline.Predict(testFeatures);
                    Console.WriteLine($"Предсказанная цена: {prediction:C2}");

                    pipeline.SaveModel("car_model.bin");
                }
                else
                {
                    Console.WriteLine("Файл cars.csv не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void TestSteelDataset()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ДАТАСЕТА ДЕФЕКТОВ СТАЛИ ===");

            var pipeline = new MLPipeline(DatasetConfig.SteelConfig);

            try
            {
                if (File.Exists("steel_defects.csv"))
                {
                    pipeline.LoadAndTrain("steel_defects.csv");

                    if (pipeline.Data?.Features.Length > 0)
                    {
                        double[] testFeatures = pipeline.Data.Features[0];
                        double prediction = pipeline.Predict(testFeatures);
                        Console.WriteLine($"Предсказанный тип дефекта: {prediction}");
                    }
                }
                else
                {
                    Console.WriteLine("Файл steel_defects.csv не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void TestCustomDataset()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ДАТАСЕТА ЭНЕРГЕТИКИ ===");

            var customConfig = new DatasetConfig
            {
                Name = "Custom Energy Dataset",
                FeatureColumns = new int[] { 2, 3, 4 }, // temperature, humidity, co2
                LabelColumn = 5, // energy consumption
                HasHeader = true,
                AlgorithmType = typeof(Algorithms.WeightedKNN),
                ProblemType = ProblemType.Regression,
                AlgorithmParameters = new Dictionary<string, object>
                {
                    { "K", 7 },
                    { "DistanceMetric", DistanceMetric.Manhattan }
                }
            };

            var pipeline = new MLPipeline(customConfig);

            try
            {
                if (File.Exists("energy.csv"))
                {
                    pipeline.LoadAndTrain("energy.csv");

                    double[] testFeatures = new double[] { 25.0, 60.0, 300.0 };
                    double prediction = pipeline.Predict(testFeatures);
                    Console.WriteLine($"Предсказанное потребление энергии: {prediction:F2}");
                }
                else
                {
                    Console.WriteLine("Файл energy.csv не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}