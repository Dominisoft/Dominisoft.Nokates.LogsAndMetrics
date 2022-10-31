using Dominisoft.Nokates.Common.Models;
using Dominisoft.Nokates.LogsAndMetrics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominisoft.Nokates.LogsAndMetrics
{
    public static class Extensions
    {
        private const int HistoryLengthInHours = 168;
        public static List<RequestMetricSummary> FillGapsInHistory(this List<RequestMetricSummary> metrics)
        {
            //    var serviceName = metrics.FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.Name))?.Name??"Service";


            var names = metrics.Select(m => m.Name).Distinct();
            foreach(var name in names)
            for (int i = 0; i < HistoryLengthInHours; i++)
            {

                if (!metrics.Any(m => m.Index == i))
                    metrics.Add(new RequestMetricSummary
                    {
                        Index = i,
                        AverageResponseTime = 0,
                        Errors = 0,
                        RequestCount = 0,
                        Name = name
                    });
            }


            return metrics.OrderBy(m => m.Index).ToList();
        }

        public static List<RequestMetricSummary> CalculateRequestMetricSummaryByService(this List<RequestMetric> requests)
        {
            var result = new List<RequestMetricSummary>();

            return result.FillGapsInHistory();
        }

        public static List<RequestMetricSummary> CalculateRequestMetricSummaryByEndpoint(this List<RequestMetric> requests)
        {
            var result = new List<RequestMetricSummary>();

            return result.FillGapsInHistory();
        }
    }
}
