using System;
using Dominisoft.Nokates.Common.Infrastructure.Client;
using Dominisoft.Nokates.Common.Models;
using LogEntry = Dominisoft.Nokates.LogsAndMetrics.Client.Models.LogEntry;

namespace Dominisoft.Nokates.LogsAndMetrics.Client
{
    public interface ILogEntryClient:IBaseClient<LogEntry>
    {

    }
    public class LogEntryClient:BaseClient<LogEntry>, ILogEntryClient
    {
        public new LogEntry Update(LogEntry entry)
            => throw new Exception("Not Allowed");
        public new bool Delete(LogEntry entry)
            => throw new Exception("Not Allowed");
    }
}
