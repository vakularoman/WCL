using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace WCL.ViewModels
{
    public class UserInfoPageViewModel : ObservableObject
    {
        private UserInfoViewModel _userInfo;
        private bool _isAuth;
        private readonly PetStoreHttpClient _httpClient;
        private bool _isHttpRequestInProgress;
        private bool _isFailedToLoad;

        public UserInfoPageViewModel(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetService<PetStoreHttpClient>()!;
        }

        public UserInfoViewModel UserInfo
        {
            get => _userInfo;
            set
            {
                if (Equals(value, _userInfo))
                {
                    return;
                }

                _userInfo = value;
                OnPropertyChanged();
            }
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

        public bool IsFailedToLoad
        {
            get => _isFailedToLoad;
            set
            {
                if (value == _isFailedToLoad)
                {
                    return;
                }

                _isFailedToLoad = value;
                OnPropertyChanged();
            }
        }

        public bool IsHttpRequestInProgress
        {
            get => _isHttpRequestInProgress;
            set
            {
                if (value == _isHttpRequestInProgress)
                {
                    return;
                }

                _isHttpRequestInProgress = value;
                OnPropertyChanged();
            }
        }

        public async Task FetchUserInfoAsync(string userName)
        {
            IsHttpRequestInProgress = true;

            var (isSuccess, userInfo) = await _httpClient.TryGetUserInfoAsync(userName).ConfigureAwait(false);

            if (isSuccess && userInfo is not null)
            {
                IsAuth = true;
                UserInfo = userInfo;
                IsFailedToLoad = false;
            }
            else
            {
                IsFailedToLoad = true;
            }

            IsHttpRequestInProgress = false;
        }

        public void LogOut()
        {
            IsAuth = false;
        }
    }
}
