using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using WCL.Helpers;

namespace WCL.ViewModels
{
    public class AuthWindowViewModel : WindowViewModel
    {
        private bool _isAnyFieldEmpty;

        public AuthWindowViewModel()
        {
            SubmitCommand = new RelayCommand(SubmitCommandExecute);
        }

        public bool IsAnyFieldEmpty
        {
            get => _isAnyFieldEmpty;
            set
            {
                if (value == _isAnyFieldEmpty)
                {
                    return;
                }
                
                _isAnyFieldEmpty = value;
                OnPropertyChanged();
            }
        }

        public UserInfoViewModel UserInfo { get; } = new();

        public ICommand SubmitCommand { get; }

        private void SubmitCommandExecute()
        {
            if (string.IsNullOrEmpty(UserInfo.Username) || string.IsNullOrEmpty(UserInfo.Password) || string.IsNullOrEmpty(UserInfo.FirstName) 
                || string.IsNullOrEmpty(UserInfo.LastName) || string.IsNullOrEmpty(UserInfo.Email) || string.IsNullOrEmpty(UserInfo.Phone))
            {
                IsAnyFieldEmpty = true;
                return;
            }

            WindowServiceProvider.Close(true);
        }
    }
}
