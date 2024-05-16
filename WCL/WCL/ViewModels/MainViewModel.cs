using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WCL.Enums;
using WCL.ViewModels;

namespace WCL
{
    public class MainViewModel : ObservableObject
    {
        private MainViewType _viewType;

        public MainViewModel()
        {
            ChangeViewCommand = new RelayCommand<MainViewType>(ChangeViewCommandExecute, ChangeViewCommandCanExecute);
        }

        public RelayCommand<MainViewType> ChangeViewCommand { get; }

        public DevInfoViewModel DevInfoViewModel { get; } = new DevInfoViewModel();

        public UserInfoViewModel UserInfoViewModel { get; } = new UserInfoViewModel();

        public AuthViewModel AuthViewModel { get; } = new AuthViewModel();

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
