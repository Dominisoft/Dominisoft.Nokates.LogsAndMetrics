using Dominisoft.Nokates.Common.Infrastructure.Controllers;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dominisoft.Nokates.LogsAndMetrics.Controllers
{
    [Route("[controller]")]

    public class LogEntryController : BaseController<LogEntry>
    {
        public LogEntryController() : base(RepositoryHelper.CreateRepository<LogEntry>())
        { }
    }
}
