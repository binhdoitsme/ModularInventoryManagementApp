using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.AuthenticationManagement.Business
{
    internal static class AuthenticationActivityFactory
    {
        internal static AuthenticationActivity Create(AuthenticationActivityType type)
        {
            switch (type)
            {
                case AuthenticationActivityType.CheckLogin:
                    return new CheckLoginActivity();
                default:
                    throw new ArgumentException();
            }
        }
    }

    internal enum AuthenticationActivityType
    {
        CheckLogin,
        Logout
    }
}
