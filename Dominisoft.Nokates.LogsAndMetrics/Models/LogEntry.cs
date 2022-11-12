using System;
using Dominisoft.Nokates.Common.Models;

namespace Dominisoft.Nokates.LogsAndMetrics.Models
{
    public class LogEntry : Entity
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }

    }
}
