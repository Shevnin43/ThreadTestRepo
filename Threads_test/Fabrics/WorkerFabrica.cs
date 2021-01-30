using Threads_test.Implementations;
using Threads_test.Interfaces;

namespace Threads_test.Fabrics
{
    /// <summary>
    /// Фабрика по производству конкретного обработчика
    /// </summary>
    public class WorkerFabrica : AbstractWorkerFabrica
    {
        /// <summary>
        /// Метод получения конкретного обработчика
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public override IWorker CreateWorker(int Id, string dir) => new ExtWorker(Id, dir);
    }
}
