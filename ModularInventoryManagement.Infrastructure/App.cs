using ModularInventoryManagement.Infrastructure.Configuration;
using ModularInventoryManagement.Infrastructure.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace ModularInventoryManagement.Infrastructure
{
    public class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var startupModule = new AppModuleMetadata();
            var window = new Window
            {
                Content = startupModule.ViewFactory.MasterPageFactory(),
                Title = startupModule.ViewFactory.Title,
                WindowState = WindowState.Maximized
            };
            startupModule.StateChanged += (s, e) => {
                window.Content = startupModule.ViewFactory.MasterPageFactory();
            };
            var app = new App()
            {
                MainWindow = window
            };
            window.Show();
            app.Run();
            //MessageBox.Show(auth.FullName);
        }
    }
}
