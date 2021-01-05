using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Darayas.Maps.Logger
{
    public class NLogger
    {
        // A Logger dispenser for the current assembly (Remember to call Flush on application exit)
        public static LogFactory Instance { get { return _instance.Value; } }
        private static Lazy<LogFactory> _instance = new Lazy<LogFactory>(BuildLogFactory);

        // 
        // Use a config file located next to our current assembly dll 
        // eg, if the running assembly is c:\path\to\MyComponent.dll 
        // the config filepath will be c:\path\to\MyComponent.nlog 
        // 
        // WARNING: This will not be appropriate for assemblies in the GAC 
        // 
        private static LogFactory BuildLogFactory()
        {
            // Use name of current assembly to construct NLog config filename 
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string configFilePath = Path.GetDirectoryName(thisAssembly.Location) + @"\nlog.config";

            LogFactory logFactory = new LogFactory();
            logFactory.Configuration = new XmlLoggingConfiguration(configFilePath, true, logFactory);
            return logFactory;
        }
    }
}
