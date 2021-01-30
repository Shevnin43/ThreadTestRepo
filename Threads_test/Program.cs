using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using Threads_test.Fabrics;
using Threads_test.Interfaces;

namespace Threads_test
{
    public class Program
    {
        /// <summary>
        /// Каталог для файлов-результатов (относительный путь
        /// </summary>
        private const string Dir = "results/";

        /// <summary>
        /// Фабрика по производству обработчиков
        /// </summary>
        private static readonly AbstractWorkerFabrica Fabrica = new WorkerFabrica();

        /// <summary>
        /// Счетчик ID для обработчиков
        /// </summary>
        private static readonly Counter counter = new Counter();

        /// <summary>
        /// Потокобезопасный список обработчиков
        /// </summary>
        public static ConcurrentBag<IWorker> Workers { get; set; } = new ConcurrentBag<IWorker>();

        /// <summary>
        /// Флаг того что подготовка и очистка каталога уже выполнена
        /// </summary>
        private static bool HasPrepareDir { get; set; } = false;

        /// <summary>
        /// Основной метод программы
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Команда:");
                var command = Console.ReadLine();
                if (command.ToLower() == "build")
                {
                    if (!HasPrepareDir)
                    {
                        PrepareEndClearDir();
                    }
                    var worker = Fabrica.CreateWorker(counter.ID, Dir);
                        Workers.Add(worker);
                        worker.Completed += (object sender, EventArgs e) =>
                        {
                            var removingWorker = worker as IWorker;
                            Workers.TryTake(out removingWorker);
                        };
                        worker.StartWorker();
                    Console.WriteLine($"Ваш запрос за номером {worker.Id} в обработке ...");
                }
                else if (command.ToLower().StartsWith("stop") 
                    && command.Split(' ').Length > 1
                    && int.TryParse(command.Split(' ')[1], out var id))
                {
                    var worker = Workers.FirstOrDefault(x => x.Id == id);
                    worker?.StopWorker(null);
                    Console.WriteLine($"Принят запрос на остановку процесса обработки номер {id}");
                }
                else
                {
                    Console.WriteLine("Не понял команду");
                }
            }
        }

        /// <summary>
        /// Подготовка каталога (с очисткой в начале каждого сеанса)
        /// </summary>
        private static void PrepareEndClearDir()
        {
            try 
            {
                if (!Directory.Exists(Dir))
                {
                    Directory.CreateDirectory(Dir);
                    return;
                }
                HasPrepareDir = true;
                var filesArray = new DirectoryInfo(Dir).GetFiles();
                foreach (var file in filesArray)
                {
                    file.Delete();
                }
            }
            catch
            {

            }
        }
    }

    /// <summary>
    /// Вспомогательный класс-счетчик чтобы мы брали только последовательные индексы для наших обработчиков
    /// </summary>
    public class Counter
    {
        private int id { get; set; } = 0;
        public  int ID { get => id++; }
    }
}
