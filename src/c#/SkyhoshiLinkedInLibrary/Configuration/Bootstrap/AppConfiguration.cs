using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace SkyhoshiLinkedInLibrary.Configuration.Bootstrap
{
    using static Logging.SkyhoshiLogger;

    public static class ApplicationConfiguration<T> where T : class
    {
        private static IConfigurationRoot _configuration;

        /// <summary>
        /// Object to which you can retrieve Config file (json and xml) stored options for an application
        /// </summary>
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration != null) return _configuration;
                Type type = typeof(ApplicationConfiguration<T>);
                BootstrapConfiguration(type);
                return _configuration;
            }
        }

        /// <summary>
        /// Loads the Assembly's Configuration and other Configurations based on the type. 
        /// </summary>
        /// <param name="type"></param>
        private static void BootstrapConfiguration(Type type)
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(Assembly.GetAssembly(type),false,true);
            if (File.Exists("./settings.json"))
            {
                builder.AddJsonFile($"./settings.json");
                Log($"Loading : ./settings.json");
            }
            if (File.Exists("./appsettings.json"))
            {
                builder.AddJsonFile($"./appsettings.json");
                Log($"Loading : ./appsettings.json");
            }
            foreach (IConfigurationSource configurationSource in builder.Sources)
            {
                JsonConfigurationSource source = (JsonConfigurationSource)configurationSource;
                if (source != null)
                {
                    //source.ReloadOnChange = true;
                    Log($@"Loading Source: {source.Path} Reloads on Change:{source.ReloadOnChange}");
                }
            }

            foreach (KeyValuePair<string, object> builderProperty in builder.Properties)
            {
                Log($@"{builderProperty.Key}:{builderProperty.Value.GetType()}");
            }

            _configuration = builder.Build();

        }

        /// <summary>
        /// Retrieves the configuration option by the name of the option.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConfigurationByName(string name)
        {
            Log($"Retrieving : {name} from Configuration : {Configuration.ToString()}");
            return Configuration[name];
        }
    }

}
