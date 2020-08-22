using ModularInventoryManagement.AuthenticationManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModularInventoryManagement.AuthenticationManagement.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var modal = new LoadingWindow
            {
                WindowStyle = WindowStyle.ToolWindow
            };
            modal.Show();
            await ((LoginViewModel)DataContext).OnTryLogin();
            modal.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            { ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
