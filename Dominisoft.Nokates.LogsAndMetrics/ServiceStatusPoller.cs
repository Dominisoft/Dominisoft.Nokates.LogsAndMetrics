
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Dominisoft.Nokates.Common.Infrastructure.Client;
using Dominisoft.Nokates.Common.Infrastructure.Configuration;
using Dominisoft.Nokates.Common.Infrastructure.Helpers;
using Dominisoft.Nokates.Common.Models;

namespace Dominisoft.Nokates.LogsAndMetrics
{
    public class ServiceStatusPoller
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _servicesStatusUrl;
        private readonly Timer _timer;
        private readonly AuthenticationClient _authenticationClient;

        public ServiceStatusPoller(int interval, string username, string password, string rootUrl)
        {
            _username = username;
            _password = password;
            var authenticationUrl = ConfigurationValues.Values["AuthenticationUrl"];
            _servicesStatusUrl = rootUrl;
            _authenticationClient = new AuthenticationClient(authenticationUrl);
            _timer = new Timer(60000 * interval);
            _timer.AutoReset = true;
            _timer.Elapsed+=OnTimer;
            _timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                LogDebug($"Checking Service Status at {DateTime.Now}");
                var token = _authenticationClient.GetToken(_username, _password);
                var result = HttpHelper.Get<List<ServiceStatus>>(_servicesStatusUrl, token);
                var statuses = result.Object??new List<ServiceStatus>();
                var online = statuses.Count(s => s.IsOnline);
                var offline = statuses.Where(s => !s.IsOnline).ToList();
                LogDebug($"Result: {online} Services Online, {offline.Count} Offline");
                foreach (var service in offline)
                {
                    LogDebug($"{service.Name} Offline");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                LoggingHelper.LogMessage(exception.Message);
            }

        }

        private void LogDebug(string message)
        {
            ConfigurationValues.Values.TryGetValue("IsDebug", out var isDebug);
                if (isDebug.ToLower() != "true") return;
                LoggingHelper.LogMessage(message);
        }
    }
}
