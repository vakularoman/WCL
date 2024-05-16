﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using WCL.Helpers;

namespace WCL.ViewModels
{
    public class AuthWindowViewModel : WindowViewModel
    {
        private string _userName;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _phone;

        public AuthWindowViewModel()
        {
            SubmitCommand = new RelayCommand(SubmitCommandExecute);
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName)
                {
                    return;
                }

                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName)
                {
                    return;
                }

                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email)
                {
                    return;
                }

                _email = value;
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

        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone)
                {
                    return;
                }

                _phone = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; }

        private void SubmitCommandExecute()
        {
            WindowServiceProvider.Close(true);
        }
    }
}
