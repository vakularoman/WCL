using CommunityToolkit.Mvvm.ComponentModel;

namespace WCL.Helpers
{
    public abstract class WindowViewModel : ObservableObject
    {
        public WindowServiceProvider WindowServiceProvider { get; set; }
    }
}
