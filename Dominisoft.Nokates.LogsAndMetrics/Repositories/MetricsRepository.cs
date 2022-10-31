using Dominisoft.Nokates.Common.Infrastructure.Repositories;
using Dominisoft.Nokates.Common.Infrastructure.RepositoryConnections;
using Dominisoft.Nokates.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominisoft.Nokates.LogsAndMetrics.Repositories
{

    public interface IMetricsRepository : ISqlRepository<RequestMetric>
    {

    }
    public class MetricsRepository : SqlRepository<RequestMetric>, IMetricsRepository
    {
        public MetricsRepository(string connectionString) : base(connectionString)
        {
        }

        public MetricsRepository(IConnectionString connectionStringProvider) : base(connectionStringProvider)
        {
        }
    }
}
