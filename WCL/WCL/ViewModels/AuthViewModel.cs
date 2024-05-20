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
        private bool _isHttpOperationRunning;

        private readonly Func<string, Task> _logInFunc;
        private readonly Action _logOutFunc;

        public AuthViewModel(IServiceProvider serviceProvider, Func<string, Task> logInFunc, Action logOutFunc)
        {
            OpenRegistrationCommand = new AsyncRelayCommand(OpenRegistrationCommandExecute);
            LogInCommand = new AsyncRelayCommand(LogInCommandExecute);
            LogOutCommand = new AsyncRelayCommand(LogOutCommandExecute);

            _httpClient = serviceProvider.GetService<PetStoreHttpClient>()!;
            _logInFunc = logInFunc;
            _logOutFunc = logOutFunc;
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

        public bool IsHttpOperationRunning
        {
            get => _isHttpOperationRunning;
            set
            {
                if (value == _isHttpOperationRunning)
                {
                    return;
                }

                _isHttpOperationRunning = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenRegistrationCommand { get; }

        public ICommand LogInCommand { get; }

        public ICommand LogOutCommand { get; }

        public async Task OpenRegistrationCommandExecute()
        {
            var viewModel = new AuthWindowViewModel();
            var result = WindowService.ShowDialog(viewModel);

            if (result == true)
            {
                IsHttpOperationRunning = true;

                var isSuccess = await _httpClient.TryRegisterAsync(viewModel.UserInfo).ConfigureAwait(false);
                IsAuth = isSuccess;

                IsHttpOperationRunning = false;
                await _logInFunc.Invoke(viewModel.UserInfo.Username).ConfigureAwait(false);

                UserName = viewModel.UserInfo.Username;
                Password = viewModel.UserInfo.Password;
            }
        }

        public async Task LogInCommandExecute()
        {
            IsHttpOperationRunning = true;

            var isSuccess = await _httpClient.TryLogInAsync(UserName, Password).ConfigureAwait(false);

            IsHttpOperationRunning = false;

            if (isSuccess)
            {
                IsAuth = isSuccess;
                await _logInFunc.Invoke(UserName).ConfigureAwait(false);
            }
        }

        public async Task LogOutCommandExecute()
        {
            var isSuccess = await _httpClient.TryLogOutAsync().ConfigureAwait(false);

            if (isSuccess)
            {
                IsAuth = false;
                _logOutFunc?.Invoke();
            }
        }
    }
}
