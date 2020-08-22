using ModularInventoryManagement.Infrastructure.Configuration;
using ModularInventoryManagement.Infrastructure.Extensibility;
using ModularInventoryManagement.Infrastructure.ViewFactory;
using System.Collections.Generic;

namespace ModularInventoryManagement.Infrastructure
{
    class AppModuleMetadata : AbstractModuleMetadata
    {
        private readonly IModuleMetadata startupModule;
        public override string Name => "App";
        public override IViewFactory ViewFactory { get; } = new AppViewFactory();
        private readonly IStateObject mState;
        public override IStateObject State => mState;

        public AppModuleMetadata() : base()
        {
            mState = new StateObject();
            startupModule = ModuleManager.GetInstance().GetStartupModule();
            startupModule.StateChanged += HandleStartupModuleStateChange;
            StateChanged += BroadcastStateChange;
            var modules = ModuleManager.GetInstance().ModuleMetadataCollection;
            foreach (var module in modules)
            {
                module.StateChanged += HandleModuleStateChange;
            }
            State.PutState("isLogin", "true");
        }

        // broadcast changes of this to all other modules
        private void BroadcastStateChange(object sender, object e)
        {
            var modules = ModuleManager.GetInstance().ModuleMetadataCollection;
            foreach (var module in modules)
            {
                State.CopyTo(module.State);
            }
        }

        private void HandleModuleStateChange(object sender, object e)
        {
            var state = sender as IStateObject;
            state.CopyTo(State);
            BroadcastStateChange(sender, e);
            var modulePermissions = state.GetState<IEnumerable<string>>("permissions");
            if (modulePermissions is null)
            {
                ViewFactory.AllowedPages = null;
            }
            if (state.GetState<string>("isLogin") == "true")
            {
                ViewFactory.AllowedPages = new string[] { startupModule.Name };
                startupModule.ViewFactory.AllowedPages = new string[]
                {
                    AppConfiguration.GetInstance().GetString("startupPage")
                };
            }
        }

        private void HandleStartupModuleStateChange(object s, object e)
        {
            var state = s as IStateObject;
            var modulePermissions = state.GetState<IEnumerable<string>>("permissions");
            if (modulePermissions is null) return; 
            var allowedPagesInViews = BuildPermissions(modulePermissions);
            var allowedModules = allowedPagesInViews.Keys;
            ViewFactory.AllowedPages = allowedModules;
            SetModuleAllowedPages(allowedPagesInViews);
            state.CopyTo(State);
        }

        private void SetModuleAllowedPages(IDictionary<string, IList<string>> allowedPagesInViews)
        {
            var moduleManager = ModuleManager.GetInstance();
            var allowedModules = allowedPagesInViews.Keys;
            foreach (var moduleName in allowedModules)
            {
                var moduleMetadata = moduleManager.GetModule(moduleName);
                var allowedPages = allowedPagesInViews[moduleName];
                moduleMetadata.ViewFactory.AllowedPages = allowedPages;
            }
        }

        private IDictionary<string, IList<string>> BuildPermissions(IEnumerable<string> modulePermissions)
        {
            IDictionary<string, IList<string>> allowedPagesInViews = new Dictionary<string, IList<string>>();
            foreach (var allowedPage in modulePermissions)
            {
                var splitIndex = allowedPage.Trim().LastIndexOf('.');
                var viewName = allowedPage.Substring(0, splitIndex);
                var pageName = allowedPage.Substring(splitIndex + 1);
                if (!allowedPagesInViews.ContainsKey(viewName))
                {
                    allowedPagesInViews.Add(viewName, new List<string>());
                }
                allowedPagesInViews[viewName].Add(pageName);
            }
            return allowedPagesInViews;
        }
    }
}
