﻿using System;
using Dominisoft.Nokates.Common.Models;

namespace Dominisoft.Nokates.LogsAndMetrics.Common.DataTransfer
{
    public class LogEntryDto : Entity
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }

    }
}
