using System;
using System.Collections.Generic;
using System.Text;
using Threads_test.Interfaces;

namespace Threads_test.Fabrics
{
    /// <summary>
    /// Абстрактная фабрика по производству обработчиков
    /// </summary>
    public abstract class AbstractWorkerFabrica
    {
        /// <summary>
        /// Метод получения конкретного обработчика
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public abstract IWorker CreateWorker(int Id, string dir);
    }
}
