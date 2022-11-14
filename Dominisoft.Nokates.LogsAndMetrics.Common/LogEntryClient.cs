using System;
using Dominisoft.Nokates.Common.Infrastructure.Client;
using LogEntryDto = Dominisoft.Nokates.LogsAndMetrics.Common.DataTransfer.LogEntryDto;

namespace Dominisoft.Nokates.LogsAndMetrics.Common
{
    public interface ILogEntryClient:IBaseClient<DataTransfer.LogEntryDto>
    {

    }
    public class LogEntryClient:BaseClient<DataTransfer.LogEntryDto>, ILogEntryClient
    {
        public LogEntryClient(string baseUrl)
        {
            BaseUrl = baseUrl + "\\Log";
        }
        public new LogEntryDto Update(LogEntryDto entry)
            => throw new Exception("Not Allowed");
        public new bool Delete(LogEntryDto entry)
            => throw new Exception("Not Allowed");
        public new LogEntryDto Update(LogEntryDto entry,string token)
            => throw new Exception("Not Allowed");
        public new bool Delete(LogEntryDto entry,string token)
            => throw new Exception("Not Allowed");
    }
}
