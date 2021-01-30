using System;
using System.Text;
using System.Threading;
using Threads_test.Interfaces;

namespace Threads_test.Implementations
{
    /// <summary>
    /// Класс получателя (формирователя) отчета
    /// </summary>
    public class ReportBuilder : IBuilder
    {
        /// <summary>
        /// Рандомайзер
        /// </summary>
        private Random Rand = new Random();

        /// <summary>
        /// Единственный метод - получение (формирование) отчета
        /// </summary>
        /// <returns></returns>
        public byte[] Build()
        {
            var timeRnd = Rand.Next(5, 45);
            for (var i = 0; i < timeRnd; i++)
            {
                Thread.Sleep(1000);
            }
            if (Rand.Next(1, 100) % 5 == 0)         // да, вот так вот интересно получаю 20% вероятность ошибки
            {
                throw new Exception("Report failed");
            }
            else
            {
                return Encoding.UTF8.GetBytes($"Report ready at {timeRnd} s.");
            }
        }
    }
}
