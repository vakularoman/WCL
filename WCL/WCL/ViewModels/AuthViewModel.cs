using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WCL.Helpers;

namespace WCL.ViewModels
{
    public class AuthViewModel : ObservableObject
    {
        private bool _isAuth;

        public AuthViewModel()
        {
            OpenRegistrationCommand = new RelayCommand(OpenRegistrationExecute);
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

        public ICommand OpenRegistrationCommand { get; set; }

        public void OpenRegistrationExecute()
        {
            var viewModel = new AuthWindowViewModel();
            var result = WindowService.ShowDialog(viewModel);
        }
    }
}
