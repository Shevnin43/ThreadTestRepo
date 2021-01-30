using System;
using System.IO;
using System.Text;
using Threads_test.Interfaces;

namespace Threads_test.Implementations
{
    /// <summary>
    /// Класс записи отчета на диск
    /// </summary>
    public class Reporter : IReporter
    {
        /// <summary>
        /// Относительный путь записи всех результатов
        /// </summary>
        public string Dir { get; }

        /// <summary>
        /// Текст ошибки - массив байт (используется в двух местах, а посему вынесен в свойство))
        /// </summary>
        private readonly byte[] ErrorData = Encoding.UTF8.GetBytes("Report error");

        /// <summary>
        /// Конструктор принимающий относительный путь каталога для записи
        /// </summary>
        /// <param name="dir"></param>
        public Reporter(string dir) => Dir = dir;

        /// <summary>
        /// Метод записи ошибки при формировании отчета (те самые 20%)
        /// </summary>
        /// <param name="Id"></param>
        public void ReportError(int Id)
        {
            WriteReport($"{Dir}Error_{Id}.txt", ErrorData);
            //Console.WriteLine($"--{Id}-- Error");                 Исключительно для отладки
        }

        /// <summary>
        /// Метод записи удачного формирования отчета
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Id"></param>
        public void ReportSuccess(byte[] Data, int Id)
        {
            WriteReport($"{Dir}Report_{Id}.txt", Data);
            //Console.WriteLine($"--{Id}-- Success");               Исключительно для отладки
        }

        /// <summary>
        /// Метод записи ошибки по истечении 20с при формировании отчета
        /// </summary>
        /// <param name="Id"></param>
        public void ReportTimeout(int Id)
        {
            WriteReport($"{Dir}Timeout_{Id}.txt", ErrorData);
            //Console.WriteLine($"--{Id}-- Timeout");               Исключительно для отладки
        }

        /// <summary>
        /// Метод непосредственной записи информации на диск (вынес в отдельный метод исключительно ради "try")
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        private void WriteReport(string fileName, byte[] data)
        {
            try
            {
                File.WriteAllBytes(fileName, data);
            }
            catch { }
        }
    }
}
