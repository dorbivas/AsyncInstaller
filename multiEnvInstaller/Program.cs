using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace multiEnvInstaller
{
    class Program 
    {
        public static Logger logr = new Logger();
        private static Installer installer = new Installer(logr);
        public async Task Main(string[] args)
        {
            List<string> hosts = args.ToList();
            await installer.MockInstaller(hosts);
        }
    }

    public class Logger : ILogger
    {
        public async Task Log(string message, string level)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                await sw.WriteLineAsync($"{level} | {Task.CurrentId} | {message}");
            }
        }
    }
}
