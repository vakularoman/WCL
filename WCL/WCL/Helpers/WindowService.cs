using System.Windows;

namespace WCL.Helpers
{
    public static class WindowService
    {
        private const int DefaultHeight = 600;
        private const int DefaultWidth = 400;
        private const int DefaultMinHeight = 300;
        private const int DefaultMinWidth = 200;

        public static bool? ShowDialog(WindowViewModel viewModel)
        {
            var window = new Window
            {
                Content = viewModel,
                Height = DefaultHeight,
                Width = DefaultWidth,
                MinHeight = DefaultMinHeight,
                MinWidth = DefaultMinWidth
            };

            viewModel.WindowServiceProvider = new WindowServiceProvider(window);

            return window.ShowDialog();
        }
    }
}
