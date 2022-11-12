using System;
using System.Collections.Generic;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Client.DataTransfer;

namespace Dominisoft.Nokates.LogsAndMetrics.Client
{
    public interface IMetricsClient
    {

        RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string,string> searchParameters,string token);
        RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName,string token);
        RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName,string token);
        RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId, string token);
    }
    public class MetricsClient: IMetricsClient
    {
        private readonly string _serviceRootUri;

        public MetricsClient(string serviceRootUri, string authToken)
        {
            _serviceRootUri = serviceRootUri;
            HttpHelper.SetToken(authToken);
        }


        public RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string, string> searchParameters, string token)
            => HttpHelper.Post<List<RequestMetricDto>>($"{_serviceRootUri}/Search",searchParameters);

        public RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/{serviceName}");


        public RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/{serviceName}");


        public RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId, string token)
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/request/{requestId}");

    }
}
