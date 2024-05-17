using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WCL.Enums;
using WCL.ViewModels;

namespace WCL
{
    public class MainViewModel : ObservableObject
    {
        private MainViewType _viewType;
        private readonly IServiceProvider _serviceProvider;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            ChangeViewCommand = new RelayCommand<MainViewType>(ChangeViewCommandExecute, ChangeViewCommandCanExecute);
            _serviceProvider = serviceProvider;
            AuthViewModel = new AuthViewModel(serviceProvider);
        }

        public RelayCommand<MainViewType> ChangeViewCommand { get; }

        public DevInfoViewModel DevInfoViewModel { get; } = new DevInfoViewModel();

        public UserInfoViewModel UserInfoViewModel { get; } = new UserInfoViewModel();

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
