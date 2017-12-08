using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    /// <summary>
    /// Тротлинг - ограничение запросов от пользователя после превышения лимита
    /// </summary>
    public interface ILogSaver
    {
        Task SaveLogEntry(string applicationId, LogEntry logentry);
    }

    public sealed class ElasticsearchLogSaver : ILogSaver
    {
        public Task SaveLogEntry(string applicationId, LogEntry logentry)
        {
            // Сохраняем переданную запись
            return Task.FromResult<object> (null);
        }
    }

    public abstract class LogSaverDecorator : ILogSaver
    {
        protected readonly ILogSaver _decoratee;
        protected LogSaverDecorator (ILogSaver decoratee)
        {
            this._decoratee = decoratee;
        }
        public abstract Task SaveLogEntry(string applicationId, LogEntry logEntry);
    }

    public class ThrottlingLogSaverDecorator : LogSaverDecorator
    {
        public ThrottlingLogSaverDecorator (ILogSaver decoratee) : base (decoratee)
        {

        }

      

        public override async Task SaveLogEntry(string applicationId, LogEntry logEntry)
        {
            if (!QuotaReached(applicationId))
            {
                IncrementUsedQuota();
                // Сохраняем записи, обращаясь к декорируемому объекту
                await _decoratee.SaveLogEntry(applicationId, logEntry);
                return;
            }
            // Сохранение невозможно, ибо лимит исчерпан
            throw new OutOfMemoryException();
        }

        private void IncrementUsedQuota()
        {
            throw new NotImplementedException();
        }

        private bool QuotaReached(string applicationId)
        {
            throw new NotImplementedException();
        }
    }

    // Декоратор для замера времени
    public class TraceLogSaverDecorator : LogSaverDecorator
    {
        public TraceLogSaverDecorator (ILogSaver decoratee): base (decoratee)
        { }

        public override async Task SaveLogEntry(string applicationId, LogEntry logEntry)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _decoratee.SaveLogEntry(applicationId, logEntry);
            }
            finally
            {
                Trace.TraceInformation("Операция сохранения завершена за {0} мс", sw.ElapsedMilliseconds);
            }
        }
    }

    public class Client
    {
        public void Function()
        {
            ILogSaver logSaver = new ThrottlingLogSaverDecorator(new ElasticsearchLogSaver());
            ILogSaver logSaverWithTrace = new ThrottlingLogSaverDecorator(new TraceLogSaverDecorator(new ElasticsearchLogSaver()));
        }
    }

    public class LogEntry
    {
    }
}