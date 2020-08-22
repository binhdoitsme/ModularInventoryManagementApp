using ModularInventoryManagement.Infrastructure.Configuration;
using ModularInventoryManagement.Infrastructure.Extensibility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    internal class ModuleManager
    {
        private readonly IDictionary<string, IModuleMetadata> metadataDictionary;

        ModuleManager() 
        {
            metadataDictionary = new Dictionary<string, IModuleMetadata>();
        }

        internal IEnumerable<IModuleMetadata> ModuleMetadataCollection
        {
            get => metadataDictionary.Values;
        }

        /// <summary>
        /// Load the module metadata (communication interface). 
        /// The module assembly must be named {moduleName}.dll and put in the executable path.
        /// The module metadata class must be named ModuleMetadata and put in the root namespace of the assembly.
        /// </summary>
        /// <param name="moduleName">The name of the module to load</param>
        /// <returns>The module metadata instance</returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.IO.FileLoadException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal IModuleMetadata LoadModuleMetadata(string moduleName)
        {
            if (metadataDictionary.ContainsKey(moduleName))
            {
                return metadataDictionary[moduleName];
            }
            Assembly assembly = Assembly.LoadFrom($"{moduleName}.dll");
            var metadataClass = assembly.GetType($"{moduleName}.ModuleMetadata");
            var metadata = metadataClass.GetConstructor(new Type[] { })
                                .Invoke(new object[] { }) as IModuleMetadata;
            metadataDictionary.Add(metadata.Name, metadata);
            return metadata;
        }

        /// <summary>
        /// Load all required modules.
        /// </summary>
        internal void LoadModules()
        {
            var moduleNames = AppConfiguration.GetInstance().GetArray("modules");
            foreach (var moduleName in moduleNames)
            {
                LoadModuleMetadata(moduleName);
            }
        }

        /// <summary>
        /// Get a module metadata by its name
        /// </summary>
        /// <param name="moduleName">Name of the module</param>
        /// <returns>The module metadata</returns>
        internal IModuleMetadata GetModule(string moduleName)
        {
            return metadataDictionary[moduleName];
        }

        internal IModuleMetadata GetStartupModule()
        {
            var startupModuleName = AppConfiguration.GetInstance()
                                        .GetString("startupModule");
            return LoadModuleMetadata(startupModuleName);
        }

        private static readonly ModuleManager instance;
        static ModuleManager()
        {
            instance = new ModuleManager();
            // Load all modules on startup
            instance.LoadModules();
        }
        public static ModuleManager GetInstance() => instance;
    }
}
