using Quick_Queue.Views;

namespace Quick_Queue;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new CafeSelection();
    }
}
