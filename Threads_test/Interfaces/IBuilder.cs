
namespace Threads_test.Interfaces
{
    /// <summary>
    /// Интерфейс получателя (формирователя) отчета
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Единственный метод - получение (формирование) отчета
        /// </summary>
        /// <returns></returns>
        public byte[] Build();
    }
}
