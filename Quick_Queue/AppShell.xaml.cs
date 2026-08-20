namespace Quick_Queue;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            nameof(Views.CafeDetails),
            typeof(Views.CafeDetails));
    }
}