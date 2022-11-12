﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dominisoft.Nokates.Common.Infrastructure.Configuration;
using Dominisoft.Nokates.Common.Infrastructure.Repositories;
using Dominisoft.Nokates.Common.Infrastructure.RepositoryConnections;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Models;
using RequestMetric = Dominisoft.Nokates.LogsAndMetrics.Models.RequestMetric;

namespace Dominisoft.Nokates.LogsAndMetrics.Application
{
    public interface IMetricsManagementService :ISqlRepository<RequestMetric>
    {
        List<RequestMetricSummary> GetMetricSummaryByServiceName(string serviceName);
        List<RequestMetric> GetMetricsByRequestId(Guid requestId);
        List<RequestMetric> SearchRequestMetrics(object searchParameters);
        List<RequestMetricSummary> GetEndpointMetricsSummaryByServiceName(string serviceName);
    }
    public class MetricsManagementService : SqlRepository<RequestMetric>, IMetricsManagementService
    {
        private static string spGetMetricsByService = "spGetMetricsByService";
        private static string spGetEndpointMetricsByService = "spGetEndpointMetricsByService";

        private string _connectionString;
        public MetricsManagementService(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public MetricsManagementService(IConnectionString connectionStringProvider) : base(connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }

        public List<RequestMetricSummary> GetMetricSummaryByServiceName(string serviceName)
        {
            var metrics = new List<RequestMetricSummary>();
            using (var connection = new SqlConnection(_connectionString))
                metrics = connection.Query<RequestMetricSummary>(spGetMetricsByService, new { ServiceName = serviceName }, commandType: System.Data.CommandType.StoredProcedure).ToList();
            

            for (var i = 0; i < 168; i++)
            {

                if (metrics.All(m => m.Index != i))
                    metrics.Add(new RequestMetricSummary
                    {
                        Index = i,
                        AverageResponseTime = 0,
                        Errors = 0,
                        RequestCount = 0,
                        Name = serviceName
                    });
            }


            return metrics.OrderBy(m => m.Index).ToList();
        }

        public List<RequestMetric> GetMetricsByRequestId(Guid requestId)
        {
            var metrics = GetAllMatchingFilter(new {RequestId = requestId});
            return metrics;
        }

        public List<RequestMetric> SearchRequestMetrics(object searchParameters)
            => GetAllMatchingFilter(searchParameters);

        public List<RequestMetricSummary> GetEndpointMetricsSummaryByServiceName(string serviceName)
        {
            List<RequestMetricSummary> metrics;
            using (var connection = new SqlConnection(_connectionString))
                metrics = connection.Query<RequestMetricSummary>(spGetEndpointMetricsByService, new { serviceName }, commandType: System.Data.CommandType.StoredProcedure).ToList();
            var allEndpoints = metrics.Select(m => m.Name).ToList();
            var endpoints = allEndpoints.Distinct();
            foreach (var endpoint in endpoints)
                for (var i = 0; i < 168; i++)
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