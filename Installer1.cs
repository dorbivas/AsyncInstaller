namespace multiEnvInstaller
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.IO;
    public class Installer 
    {
        ILogger _logger;
        public Installer(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task MockInstaller(List<string> hosts)
        {
            int failCount = 0;
            await _logger.Log("Starting installation...", "INFO");
            foreach (string host in hosts)
            {
                if (!await Install(host))
                    failCount++;   
            }
            if (failCount == 0)
                await _logger.Log("Installation complete!", "INFO");
            else
                await _logger.Log($"Installation failed on {failCount} hosts!", "ERROR");

        }
        private async Task<bool> Install(string host) // Accept fail count by reference
        {
            int num = new Random().Next(1, 4);
            try 
            {
                await _logger.Log($"Installing on {host}...", "INFO");
                
                if (num == 2)
                {
                    await _logger.Log($"Installation failed on {host}!", "ERROR");  
                    throw new Exception("Installation failed!");
                }
                await Task.Delay(1000);
                await _logger.Log($"Installed on {host}!", "INFO");
                return true;
            }
            catch (Exception e)
            {
                await _logger.Log($"{e.Message}", "ERROR");
                return false;
            }
        }
    }
}