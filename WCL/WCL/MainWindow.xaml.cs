using System.Windows;

namespace WCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; }

        public MainWindow()
        {
            MainViewModel = new MainViewModel();
            DataContext = this;

            InitializeComponent();
        }
    }
}