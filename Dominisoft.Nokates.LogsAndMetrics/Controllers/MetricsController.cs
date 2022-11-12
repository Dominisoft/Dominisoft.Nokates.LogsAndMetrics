using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dominisoft.Nokates.Common.Infrastructure.Configuration;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Application;
using Dominisoft.Nokates.LogsAndMetrics.Models;
using Microsoft.AspNetCore.Mvc;
using RequestMetric = Dominisoft.Nokates.LogsAndMetrics.Models.RequestMetric;

namespace Dominisoft.Nokates.LogsAndMetrics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsManagementService _metricsManagementService;
     

        public MetricsController(IMetricsManagementService metricsManagementService)
        {
            this._metricsManagementService = metricsManagementService;
        }

        [HttpGet("{serviceName}")]
        public List<RequestMetricSummary> GetMetricSummaryByServiceName(string serviceName)
            => _metricsManagementService.GetMetricSummaryByServiceName(serviceName);


        [HttpGet("request/{requestId}")]
        public virtual List<RequestMetric> GetMetricsByRequestId(Guid requestId)
            => _metricsManagementService.GetMetricsByRequestId(requestId);

        [HttpGet("{serviceName}/endpoints")]
        public virtual List<RequestMetricSummary> GetEndpointMetricsSummaryByServiceName(string serviceName)
            => _metricsManagementService.GetEndpointMetricsSummaryByServiceName(serviceName);

        [HttpPost("Search")]
        public List<RequestMetric> SearchRequestMetrics([FromBody] object searchParameters)
            => _metricsManagementService.SearchRequestMetrics(searchParameters);

        [HttpGet("Errors")]
        public List<RequestMetric> GetRecentErrors()
            => _metricsManagementService.GetRecentErrors();
    }
}
