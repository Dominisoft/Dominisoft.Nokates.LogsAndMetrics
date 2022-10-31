using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dominisoft.Nokates.Common.Infrastructure.Configuration;
using Dominisoft.Nokates.Common.Infrastructure.Controllers;
using Dominisoft.Nokates.Common.Infrastructure.Repositories;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Models;
using Dominisoft.Nokates.LogsAndMetrics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dominisoft.Nokates.LogsAndMetrics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private static string spGetMetricsByService = "spGetMetricsByService";
        private static string spGetMetricsByRequest = "spGetMetricsByRequest";
        private static string spGetEndpointMetricsByService = "spGetEndpointMetricsByService";

        private readonly IMetricsRepository _metricsRepository;

        public MetricsController(IMetricsRepository metricsRepository) 
        {
            _metricsRepository = metricsRepository;
        }

        [HttpGet("{serviceName}")]
        public virtual List<RequestMetricSummary> GetMetrics(string serviceName)
        => _metricsRepository.GetAllMatchingFilter(new { ServiceName = serviceName,RequestStart  }).CalculateRequestMetricSummaryByService();


        [HttpGet("request/{requestId}")]
        public virtual List<RequestMetric> GetMetricsByRequestId(Guid requestId)
         => _metricsRepository.GetAllMatchingFilter(new { RequestId = requestId });
        [HttpGet("{serviceName}/endpoints")]
        public virtual List<RequestMetricSummary> GetEndpointMetrics(string serviceName)
            => _metricsRepository.GetAllMatchingFilter(new { ServiceName = serviceName }).CalculateRequestMetricSummaryByEndpoint();


    }
}
