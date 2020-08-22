using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace ModularInventoryManagement.Infrastructure.Configuration
{
    internal class AppConfiguration
    {
        private const string AppConfigPath = "appconfig.json";
        private readonly IConfiguration configuration;

        private AppConfiguration()
        {
            configuration = new ConfigurationBuilder()
                                .AddJsonFile(AppConfigPath)
                                .Build();
        }

        internal string GetString(string key)
        {
            return configuration[key];
        }

        internal IEnumerable<string> GetArray(string key)
        {
            var regex = $"{key}:\\d+";
            var result = configuration.AsEnumerable()
                                    .Where(pair => Regex.IsMatch(pair.Key, regex))
                                    .Select(pair => pair.Value);
            return result;
        }

        private static readonly AppConfiguration instance;

        static AppConfiguration()
        {
            instance = new AppConfiguration();
        }

        public static AppConfiguration GetInstance() => instance;
    }
}
