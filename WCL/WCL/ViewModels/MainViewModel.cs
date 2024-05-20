using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WCL.Enums;
using WCL.ViewModels;

namespace WCL
{
    public class MainViewModel : ObservableObject
    {
        private MainViewType _viewType;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            ChangeViewCommand = new RelayCommand<MainViewType>(ChangeViewCommandExecute, ChangeViewCommandCanExecute);

            UserInfoPageViewModel = new UserInfoPageViewModel(serviceProvider);
            AuthViewModel = new AuthViewModel(serviceProvider, UserInfoPageViewModel.FetchUserInfoAsync, UserInfoPageViewModel.LogOut);
        }

        public RelayCommand<MainViewType> ChangeViewCommand { get; }

        public DevInfoViewModel DevInfoViewModel { get; } = new();

        public UserInfoPageViewModel UserInfoPageViewModel { get; }

        public AuthViewModel AuthViewModel { get; }

        public MainViewType ViewType
        {
            get => _viewType;
            set
            {
                if (value == _viewType)
                {
                    return;
                }

                _viewType = value;
                OnPropertyChanged();
                ChangeViewCommand.NotifyCanExecuteChanged();
            }
        }

        private void ChangeViewCommandExecute(MainViewType viewType)
        {
            ViewType = viewType;
        }

        private bool ChangeViewCommandCanExecute(MainViewType viewType)
        {
            return ViewType != viewType;
        }
    }
}
