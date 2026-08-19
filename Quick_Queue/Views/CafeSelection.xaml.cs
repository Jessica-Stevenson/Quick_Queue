using Quick_Queue.Views;

namespace Quick_Queue.Views;

public partial class CafeSelection : Window
{
    public CafeSelection()
    {
        InitializeComponent();
        Page.BindingContext = new CafeSelectionViewModel();
    }
}
