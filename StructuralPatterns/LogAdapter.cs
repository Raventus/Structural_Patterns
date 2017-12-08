using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralPatterns
{
    public class SqlServerLogSaverAdapter: ILogAdapter
    {
        private readonly SqlServerLogSaver _sqlServerLogSaver = new SqlServerLogSaver();

        public void Save (LogEntry logEntry)
        {
            var simpleEntry = logEntry as SimpleLogEntry;
            if (simpleEntry != null)
            {
                _sqlServerLogSaver.Save(simpleEntry.EntryDateTime, simpleEntry.Severity.ToString(), simpleEntry.Message);
                return;
            }
            var exceptionEntry = (ExceptionLogEntry)logEntry;
            _sqlServerLogSaver.SaveException(exceptionEntry.EntryDateTime, exceptionEntry.Message, exceptionEntry.Exception);

        }
    }

    internal class SimpleLogEntry : LogEntry
    {
    }

    public class LogEntry
    {
    }

    internal class SqlServerLogSaver
    {
    }

    public interface ILogAdapter
    {
    }


    // Адаптер применяется для посследовательного перехода на новый класс. Алгоритм:
    // 1. ПРоектируем новый интерфейс IAsyncLogSaver с набором нужных асинхронных методов
    // 2. Создаем первую реализацию AsyncLogSaverAdapter, которая реализует IAsyncLogSaver, но делегирует все операции старой реализации
    // 3. Переводим клиентов иерархии LogSaver на асинхронные рельсы и проверяем, что клиенты работают нормально
    // 4. Создаем полноценную асинхронную реализацию сохранения логов и удаляем AsyncLogSaverAdapter за ненадобностью.
}
