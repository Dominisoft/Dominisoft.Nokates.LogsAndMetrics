﻿using System;
using System.Collections.Generic;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Common.DataTransfer;

namespace Dominisoft.Nokates.LogsAndMetrics.Common
{
    public interface IMetricsClient
    {

        RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string, string> searchParameters);
        RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName);
        RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName);
        RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId);
        RestResponse<List<RequestMetric>> GetRecentErrors();
        RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string, string> searchParameters,string token);
        RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName, string token);
        RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName, string token);
        RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId, string token);
        RestResponse<List<RequestMetric>> GetRecentErrors(string token);
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
            => HttpHelper.Post<List<RequestMetricDto>>($"{_serviceRootUri}/Metrics/Search", searchParameters);

        public RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/Metrics/{serviceName}");


        public RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/Metrics/{serviceName}");


        public RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId)
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Metrics/request/{requestId}");

        public RestResponse<List<RequestMetric>> GetRecentErrors()
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Metrics/Errors");

        public RestResponse<List<RequestMetricDto>> SearchRequestMetrics(Dictionary<string, string> searchParameters,string token)
            => HttpHelper.Post<List<RequestMetricDto>>($"{_serviceRootUri}/Metrics/Search", searchParameters,token);

        public RestResponse<List<RequestMetricSummaryDto>> GetEndpointMetricsSummaryByServiceName(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/Metrics/{serviceName}",token);


        public RestResponse<List<RequestMetricSummaryDto>> GetMetricSummaryByServiceName(string serviceName, string token)
            => HttpHelper.Get<List<RequestMetricSummaryDto>>($"{_serviceRootUri}/Metrics/{serviceName}",token);


        public RestResponse<List<RequestMetric>> GetMetricsByRequestId(Guid requestId, string token)
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Metrics/request/{requestId}",token);

        public RestResponse<List<RequestMetric>> GetRecentErrors(string token)
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Metrics/Errors",token);

        public RestResponse<List<RequestMetric>> GetOverview()
            => HttpHelper.Get<List<RequestMetric>>($"{_serviceRootUri}/Overview");


    }
}
