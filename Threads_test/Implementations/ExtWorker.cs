using System;
using System.Threading;
using System.Timers;
using Threads_test.Implementations;
using Threads_test.Interfaces;
using BTimer = System.Timers.Timer;

namespace Threads_test.Implementations
{
    /// <summary>
    /// Класс обработчика процесса отчета
    /// </summary>
    public class ExtWorker : IWorker
    {
        /// <summary>
        /// Ай-ди процесса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Обработчик получения (формирования) отчета
        /// </summary>
        public IBuilder ReportBuilder { get; set; }

        /// <summary>
        /// Обработчик записи отчета в файл
        /// </summary>
        public IReporter ReportWriter { get; set; }

        /// <summary>
        /// Поток в котором создается отчет
        /// </summary>
        private Thread BuilderThread { get; }

        /// <summary>
        /// Флаг отмены процесса (по разным причинам: от пользователя, либо результат уже получен)
        /// </summary>
        private bool Canceled { get; set; } = false;

        /// <summary>
        /// Событие завершения процесса создания отчета
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Тот самый таймер который отсчитывает 20 с
        /// </summary>
        private readonly BTimer BuildTimer = new BTimer(20000)
        {
            AutoReset = false
        };

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"></param>
        public ExtWorker(int id, string dir)
        {
            Id = id;
            BuilderThread = new Thread(() => MakeReport());
            ReportBuilder = new ReportBuilder();
            ReportWriter = new Reporter(dir);
        }

        /// <summary>
        /// Команда на начало выполнения процесса создания отчета
        /// </summary>
        public void StartWorker() => BuilderThread.Start();

        /// <summary>
        /// Метод непосредственного создания отчета.
        /// </summary>
        public void MakeReport()
        {
            try
            {
                byte[] result = null;
                BuildTimer.Elapsed += (object source, ElapsedEventArgs e) =>
                {
                    if (!Canceled)
                    {
                        StopWorker(() => ReportWriter.ReportTimeout(Id));
                    }
                };
                BuildTimer.Start();
                result = ReportBuilder.Build();
                if (!Canceled)
                {
                    StopWorker(() => ReportWriter.ReportSuccess(result, Id));
                }
            }
            catch (Exception ex) when (ex.Message == "Report failed")
            {
                if (!Canceled)
                {
                    StopWorker(() => ReportWriter.ReportError(Id));
                }
            }
        }

        /// <summary>
        /// Команда на завершение создания отчета
        /// </summary>
        /// <param name="action"></param>
        public void StopWorker(Action action = null)
        {
            Canceled = true;
            BuildTimer.Stop();
            action?.Invoke();
            Completed(this, EventArgs.Empty);
            try
            {
                BuilderThread.Abort();
            }
            catch { }
        }
    }
}
