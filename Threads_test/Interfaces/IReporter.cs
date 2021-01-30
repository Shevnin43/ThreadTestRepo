
namespace Threads_test.Interfaces
{
    /// <summary>
    /// Обработчик записи отчета на диск
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Относительный путь записи результатов
        /// </summary>
        public string Dir { get; }

        /// <summary>
        /// Метод записи удачного формирования отчета
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Id"></param>
        public void ReportSuccess(byte[] Data, int Id);

        /// <summary>
        /// Метод записи ошибки при формировании отчета (те самые 20%)
        /// </summary>
        /// <param name="Id"></param>
        public void ReportError(int Id);

        /// <summary>
        /// Метод записи ошибки по истечении 20с при формировании отчета
        /// </summary>
        /// <param name="Id"></param>
        public void ReportTimeout(int Id);
    }
}
