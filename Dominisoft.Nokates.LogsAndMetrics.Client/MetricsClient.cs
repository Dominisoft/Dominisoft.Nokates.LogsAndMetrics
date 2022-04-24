using System.Collections.Generic;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.LogsAndMetrics.Models;

namespace Dominisoft.Nokates.LogsAndMetrics.Client
{
    public interface IMetricsClient
    {
        List<RequestMetricSummary> GetMetrics(string serviceName,string token);
        List<RequestMetricSummary> GetEndpointMetrics(string serviceName, string token);
    }
    public class MetricsClient: IMetricsClient
    {
        private readonly string _serviceRootUri;

        public MetricsClient(string serviceRootUri)
        {
            _serviceRootUri = serviceRootUri;
        }

        public List<RequestMetricSummary> GetMetrics(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummary>>($"{_serviceRootUri}/metrics/{serviceName}",token);

        public List<RequestMetricSummary> GetEndpointMetrics(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummary>>($"{_serviceRootUri}/metrics/{serviceName}/endpoints", token);

    }
}
