using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using WCL.Helpers;

namespace WCL.ViewModels
{
    public class AuthWindowViewModel : WindowViewModel
    {
        public AuthWindowViewModel()
        {
            SubmitCommand = new RelayCommand(SubmitCommandExecute);
        }

        public UserInfoViewModel UserInfo { get; } = new();

        public ICommand SubmitCommand { get; }

        private void SubmitCommandExecute()
        {
            WindowServiceProvider.Close(true);
        }
    }
}
