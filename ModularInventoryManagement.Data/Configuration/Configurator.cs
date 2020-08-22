using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Data.Configuration
{
    internal static class Configurator
    {
        private static readonly IConfiguration configuration;

        static Configurator()
        {
            configuration = new ConfigurationBuilder()
                                    .AddJsonFile("dataconfig.json")
                                    .Build();
        }

        internal static IConfiguration GetConfiguration()
        {
            return configuration;
        }
    }
}
