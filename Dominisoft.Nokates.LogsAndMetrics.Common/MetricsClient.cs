using System;
using System.Collections.Generic;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Common.DataTransfer;

namespace Dominisoft.Nokates.LogsAndMetrics.Common
{
    public interface IMetricsClient
    {

        RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string,string> searchParameters);
        RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName);
        RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName);
        RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId);
        RestResponse<List<RequestMetric>> GetRecentErrors();
        RestResponse<List<RequestMetric>> GetOverview();
    }
    public class MetricsClient: IMetricsClient
    {
        private readonly string _serviceRootUri;

        public MetricsClient(string serviceRootUri, string authToken)
        {
            _serviceRootUri = serviceRootUri;
            HttpHelper.SetToken(authToken);
        }
        public MetricsClient(string serviceRootUri)
        {
            _serviceRootUri = serviceRootUri;
        }


        public RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string, string> searchParameters)
            => HttpHelper.Post<List<RequestMetricDto>>($"{_serviceRootUri}/Search",searchParameters);

        public RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/{serviceName}");


        public RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/{serviceName}");


        public RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId)
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/request/{requestId}");

        public RestResponse<List<RequestMetric>> GetRecentErrors()
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Errors"); 
        public RestResponse<List<RequestMetric>> GetOverview()
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Overview");

    }
}
