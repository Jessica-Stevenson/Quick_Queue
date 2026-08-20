using Quick_Queue.Services;

namespace Quick_Queue.Views;

public partial class CafeSelection : ContentPage
{
    public CafeSelection(DatabaseService database)
    {
        InitializeComponent();

        BindingContext = new CafeSelectionViewModel(database);
    }
}