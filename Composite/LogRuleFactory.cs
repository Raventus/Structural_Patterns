using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Composite
{
    public static class LogRuleFactory
    {
        public static LogImportRule Import (Func<LogEntry, bool> predicate)
        {
            return new SingleLogImportRule(predicate);
        }

        public static LogImportRule Or (this LogImportRule left, Func<LogEntry, bool> predicate)
        {
            return new OrLogImportRule(left, Import(predicate));
        }

        public static LogImportRule And(this LogImportRule left, Func<LogEntry, bool> predicate)
        {
            return new AndLogImportRule(left, Import(predicate));
        }

        public static LogImportRuleRejectOldEntriesWithLowSeverity (TimeSpan preriod)
        {
            return Import(le => le is ExceptionLogEntry).Or(le => (DateTime.Now - le.EntryDateTime) > preiod).And(le => le.Severity >= LogImportRuleRejectOldEntriesWithLowSeverity.Warning)
                .Or(le => (DateTime.Now = le.EntryDateTime) <= preiod);
        }

    }
}