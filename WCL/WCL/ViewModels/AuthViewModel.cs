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
        private bool _isFailedToLogIn;
        private bool _isFailedToRegister;

        private readonly Func<string, Task> _logInFunc;
        private readonly Action _logOutFunc;
        private bool _isCredEmpty;

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

        public bool IsFailedToLogIn
        {
            get => _isFailedToLogIn;
            set
            {
                if (value == _isFailedToLogIn)
                {
                    return;
                }

                _isFailedToLogIn = value;
                OnPropertyChanged();
            }
        }

        public bool IsFailedToRegister
        {
            get => _isFailedToRegister;
            set
            {
                if (value == _isFailedToRegister)
                {
                    return;
                }

                _isFailedToRegister = value;
                OnPropertyChanged();
            }
        }

        public bool IsCredEmpty
        {
            get => _isCredEmpty;
            set
            {
                if (value == _isCredEmpty)
                {
                    return;
                }

                _isCredEmpty = value;
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

            IsFailedToRegister = false;
            IsFailedToLogIn = false;

            if (result == true)
            {
                IsHttpOperationRunning = true;

                var isSuccess = await _httpClient.TryRegisterAsync(viewModel.UserInfo).ConfigureAwait(false);
                IsAuth = isSuccess;

                IsHttpOperationRunning = false;

                if (isSuccess)
                {
                    await _logInFunc.Invoke(viewModel.UserInfo.Username).ConfigureAwait(false);

                    UserName = viewModel.UserInfo.Username;
                    Password = viewModel.UserInfo.Password;
                }
                else
                {
                    IsFailedToRegister = true;
                }
            }
        }

        public async Task LogInCommandExecute()
        {
            IsFailedToRegister = false;
            IsFailedToLogIn = false;

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                IsCredEmpty = true;
                return;
            }

            IsCredEmpty = false;
            IsHttpOperationRunning = true;

            var isSuccess = await _httpClient.TryLogInAsync(UserName, Password).ConfigureAwait(false);

            IsHttpOperationRunning = false;

            if (isSuccess)
            {
                IsAuth = isSuccess;
                await _logInFunc.Invoke(UserName).ConfigureAwait(false);
                IsFailedToLogIn = false;
            }
            else
            {
                IsFailedToLogIn = true;
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
