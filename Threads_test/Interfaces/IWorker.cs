using System;

namespace Threads_test.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика отчета
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Ай-ди обработчика
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Свойство - класс который будет получать (формировать) отчет
        /// </summary>
        public IBuilder ReportBuilder { get; set; }

        /// <summary>
        /// Свойство - класс, который будет сохранять отчет
        /// </summary>
        public IReporter ReportWriter { get; set; }

        /// <summary>
        /// Событие окончания процесса обработки (нужно для исключения соответствующего обработчика из списка
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Метод - команда на начало обработки процесса
        /// </summary>
        public void StartWorker();

        /// <summary>
        /// Метод - команда на окончание (логическое или прерванное пользователем) процесса обработки 
        /// </summary>
        /// <param name="action"></param>
        public void StopWorker(Action action);
    }
}
