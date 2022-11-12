using System;
using Dominisoft.Nokates.Common.Infrastructure.Client;
using Dominisoft.Nokates.Common.Models;
using LogEntryDto = Dominisoft.Nokates.LogsAndMetrics.Client.DataTransfer.LogEntryDto;

namespace Dominisoft.Nokates.LogsAndMetrics.Client
{
    public interface ILogEntryClient:IBaseClient<LogEntryDto>
    {

    }
    public class LogEntryClient:BaseClient<LogEntryDto>, ILogEntryClient
    {
        public new LogEntryDto Update(LogEntryDto entry)
            => throw new Exception("Not Allowed");
        public new bool Delete(LogEntryDto entry)
            => throw new Exception("Not Allowed");
    }
}
