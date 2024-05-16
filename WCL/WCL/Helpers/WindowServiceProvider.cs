using System.Windows;

namespace WCL.Helpers
{
    public class WindowServiceProvider
    {
        private Window _window;

        public WindowServiceProvider(Window window)
        {
            _window = window;
        }

        public void Close(bool isSuccess)
        {
            _window.DialogResult = isSuccess;
            _window.Close();
        }
    }
}
