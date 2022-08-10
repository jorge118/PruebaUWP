using App1.Controls;
using App1.Services;
using App1.ViewModels.Base;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace App1.ViewModels.Common
{
    public sealed class LoginViewModel : BaseViewModel
    {
        public string Version => $"{Constants.PoweredBy}; Build {Constants.AppVersion}";

        public string ImageSource => Constants.ImageSource;

        public string UserName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string Password
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public ICommand LoginCommand => new RelayCommand(async () => await ExecutingBusy(Login));

        private async Task Login()
        {
            try
            {
                await AuthService.Instance.LoginAsync();

                //await RefreshingToken.Start(AuthService.Instance.AuthenticationInfo.AccessTokenExpiresIn - Constants.RefreshBeforeTokenExpires); // We want to refresh before token expires, that way the token will always be valid
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Algo salió mal al iniciar sesión.", ex);
            }

            Window.Current.Content = new ShellPage();

            //Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}
