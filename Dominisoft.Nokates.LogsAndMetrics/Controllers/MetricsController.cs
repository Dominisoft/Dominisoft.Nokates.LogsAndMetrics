using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dominisoft.Nokates.Common.Infrastructure.Configuration;
using Dominisoft.Nokates.LogsAndMetrics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dominisoft.Nokates.LogsAndMetrics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private static string spGetMetricsByService = "spGetMetricsByService";
        private static string spGetEndpointMetricsByService = "spGetEndpointMetricsByService";
        

        public MetricsController()
        {
        }
        [HttpGet("{ServiceName}")]
        public virtual List<RequestMetricSummary> GetMetrics(string ServiceName)
        {
            var metrics = new List<RequestMetricSummary>();
            using (var connection = new SqlConnection(ConfigurationValues.Values["ConnectionString"]))
                metrics = connection.Query<RequestMetricSummary>(spGetMetricsByService, new { ServiceName }, commandType: System.Data.CommandType.StoredProcedure).ToList();



            for (int i = 0; i < 168; i++)
            {

                if (!metrics.Any(m => m.Index == i))
                    metrics.Add(new RequestMetricSummary
                    {
                        Index = i,
                        AverageResponseTime = 0,
                        Errors = 0,
                        RequestCount = 0,
                        Name = ServiceName
                    });
            }


            return metrics.OrderBy(m => m.Index).ToList();
        }
        [HttpGet("{ServiceName}/endpoints")]
        public virtual List<RequestMetricSummary> GetEndpointMetrics(string ServiceName)
        {
            var metrics = new List<RequestMetricSummary>();
            using (var connection = new SqlConnection(ConfigurationValues.Values["ConnectionString"]))
                metrics = connection.Query<RequestMetricSummary>(spGetEndpointMetricsByService, new { ServiceName }, commandType: System.Data.CommandType.StoredProcedure).ToList();
            var allEndpoints = metrics.Select(m => m.Name).ToList();
            var endpoints = allEndpoints.Distinct();
            foreach(var endpoint in endpoints)
            for (int i = 0; i < 168; i++)
            {

                if (!metrics.Any(m => m.Index == i && m.Name == endpoint))
                    metrics.Add(new RequestMetricSummary
                    {
                        Index = i,
                        AverageResponseTime = 0,
                        Errors = 0,
                        RequestCount = 0,
                        Name = endpoint
                    });
            }


            return metrics.OrderBy(m => m.Name).ThenBy(m => m.Index).ToList();
        }
    }
}
