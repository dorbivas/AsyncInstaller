namespace multiEnvInstaller
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.IO;
    public class Installer
    {
        private readonly ILogger _logger;

        public Installer(ILogger logger)
        {
            _logger = logger;
        }

        public async Task MockInstaller(List<string> hosts)
        {
            await _logger.Log("Starting installation...", "INFO");

            // Create a list of tasks for each host
            var tasks = hosts.Select(host => Install(host)).ToList();

            // Wait for all the tasks to complete in parallel
            var results = await Task.WhenAll(tasks);

            // Count how many were unsuccessful
            int failCount = results.Count(success => !success);

            if (failCount == 0)
                await _logger.Log("Installation complete!", "INFO");
            else
                await _logger.Log($"Installation failed on {failCount} host(s)!", "ERROR");
        }

        private async Task<bool> Install(string host)
        {
            try
            {
                await _logger.Log($"Installing on {host}...", "INFO");
                await Task.Delay(1000); // Simulate work

                // Random failure
                if (new Random().Next(1, 4) == 2)
                {
                    await _logger.Log($"Installation failed on {host}!", "ERROR");
                    throw new Exception("Installation failed!");
                }

                await _logger.Log($"Installed on {host}!", "INFO");
                return true;
            }
            catch (Exception e)
            {
                await _logger.Log($"{e.Message} (Host: {host})", "ERROR"); 
                return false;
            }
        }
    }
}