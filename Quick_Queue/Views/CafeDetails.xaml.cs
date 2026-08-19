using Quick_Queue.Models;

namespace Quick_Queue.Views;

public partial class CafeDetails : Window
{
    public CafeDetails()
    {
        InitializeComponent();

        // Bind the specically chosen cafe to this window
        BindingContext = new
        {
            CurrentCafe = SelectedCafeStore.CurrentCafe
        };
    }
}
