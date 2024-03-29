﻿using System;
using Dominisoft.Nokates.Common.Models;

namespace Dominisoft.Nokates.LogsAndMetrics.Common.DataTransfer
{
    public class RequestMetricDto:Entity
    {
        public Guid RequestTrackingId { get; set; }
        public string RequestType { get; set; }
        public string ServiceName { get; set; }
        public string RequestPath { get; set; }
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
        public string EndpointDesignation { get; set; }
        public int ResponseCode { get; set; }
        public DateTime RequestStart { get; set; }
        /// <summary>
        /// The time in ms for the request to be processed
        /// </summary>
        public long ResponseTime { get; set; }

        public string RequestSource { get; set; }
        public string RemoteIp { get; set; }
    }
}
