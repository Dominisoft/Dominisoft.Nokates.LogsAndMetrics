﻿using System;

namespace Dominisoft.Nokates.LogsAndMetrics.Models
{
    public class RequestMetricSummary
    {
        public string Name { get; set; }
        public int RequestCount { get; set; }
        public DateTime FirstRequest { get; set; }
        public DateTime LastRequest { get; set; }
        public int Index { get; set; }
        public int AverageResponseTime { get; set; }
        public int Errors { get; set; }
    }
}
