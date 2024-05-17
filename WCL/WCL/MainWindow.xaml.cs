using System.Windows;

namespace WCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; }

        public MainWindow(IServiceProvider serviceProvider)
        {
            MainViewModel = new MainViewModel(serviceProvider);
            DataContext = this;

            InitializeComponent();
        }
    }
}