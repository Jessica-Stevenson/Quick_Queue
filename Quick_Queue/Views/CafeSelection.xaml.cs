using Quick_Queue.ViewModels;

namespace Quick_Queue.Views;

public partial class CafeSelection : Window
{
    public CafeSelection()
    {
        InitializeComponent();
        Page.BindingContext = new CafeSelectionViewModel();
    }
}
