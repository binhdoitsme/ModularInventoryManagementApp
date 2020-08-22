using ModularInventoryManagement.AuthenticationManagement.Business;
using ModularInventoryManagement.Data.Abstractions;
using ModularInventoryManagement.Data.Models;
using ModularInventoryManagement.Data.Repositories;
using ModularInventoryManagement.Infrastructure.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;

namespace ModularInventoryManagement.AuthenticationManagement.ViewModels
{
    class LoginViewModel : AbstractViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAsyncReadonlyRepository<User> UserRepository;

        private string mUsername;
        public string Username
        {
            get => mUsername;
            set
            {
                if (mUsername == value) return;
                mUsername = value;
                NotifyPropertyChanged();
            }
        }
        private SecureString mPassword;
        public SecureString Password
        {
            get => mPassword;
            set
            {
                if (mPassword == value) return;
                mPassword = value;
                NotifyPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            mStateLocator = StateLocator.GetInstance();
            UserRepository = new ReadonlyUserRepository();
        }

        public async Task<bool> OnTryLogin()
        {
            var password = new System.Net.NetworkCredential(string.Empty, mPassword).Password;
            var activity = AuthenticationActivityFactory.Create(AuthenticationActivityType.CheckLogin);
            var result = await activity.PerformAsync(mUsername, password);
            var isAuthed = !(result is null);
            //mStateLocator.Put("authenticatedUser", mUsername);
            Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(result));
            if (isAuthed)
            {
                mStateLocator.Put("isLogin", "truen't");
                mStateLocator.Put("permissions", new string[] 
                { 
                    "ModularInventoryManagement.OrderManagement.TestOrderView",
                    "ModularInventoryManagement.OrderManagement.CreateOrderView"
                });
                mStateLocator.Put("empFullName", "Binh Do");
            }
            return isAuthed;
        }
    }
}
