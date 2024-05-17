using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WCL.Helpers;

namespace WCL.ViewModels
{
    public class AuthViewModel : ObservableObject
    {
        private bool _isAuth;
        private readonly PetStoreHttpClient _httpClient;
        private string _userName;
        private string _password;

        public AuthViewModel(IServiceProvider serviceProvider)
        {
            OpenRegistrationCommand = new AsyncRelayCommand(OpenRegistrationExecute);
            _httpClient = serviceProvider.GetService<PetStoreHttpClient>()!;
        }

        public bool IsAuth
        {
            get => _isAuth;
            set
            {
                if (value == _isAuth)
                {
                    return;
                }

                _isAuth = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (value == _userName)
                {
                    return;
                }

                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password)
                {
                    return;
                }

                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenRegistrationCommand { get; set; }

        public async Task OpenRegistrationExecute()
        {
            var viewModel = new AuthWindowViewModel();
            var result = WindowService.ShowDialog(viewModel);

            if (result == true)
            {
                var isSuccess = await _httpClient.TryRegisterAsync(viewModel.UserInfo).ConfigureAwait(false);
                IsAuth = isSuccess;
            }
        }
    }
}
