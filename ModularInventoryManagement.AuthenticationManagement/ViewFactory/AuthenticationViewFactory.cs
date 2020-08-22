using ModularInventoryManagement.AuthenticationManagement.Views;
using ModularInventoryManagement.Infrastructure.ViewFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ModularInventoryManagement.AuthenticationManagement.ViewFactory
{
    internal class AuthenticationViewFactory : AbstractViewFactory
    {
        public override string Title { get; } = "Log in";

        public AuthenticationViewFactory()
        {
            PageCreators = new Dictionary<string, Func<Page>>()
            {
                { nameof(LoginView), () => new LoginView() }
            };
            AllowedPages = new List<string>();
        }
    }
}
