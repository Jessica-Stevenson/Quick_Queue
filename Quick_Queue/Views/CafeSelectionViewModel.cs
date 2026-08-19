using System.Collections.ObjectModel;
using System.Windows.Input;
using Quick_Queue.Views;

namespace Quick_Queue.ViewModels;

public class CafeInfo
{
    public string Name { get; set; }
    public double ServiceTimePerPerson { get; set; }
}

public class CafeSelectionViewModel : BindableObject
{
    public ObservableCollection<CafeInfo> Cafes { get; } =
        new ObservableCollection<CafeInfo>
        {
            new CafeInfo { Name = "Main Campus Café", ServiceTimePerPerson = 2 },
            new CafeInfo { Name = "Library Café",      ServiceTimePerPerson = 3 },
            new CafeInfo { Name = "Tech Hub Café",     ServiceTimePerPerson = 1.5 }
        };

    private CafeInfo _selectedCafe;
    public CafeInfo SelectedCafe
    {
        get => _selectedCafe;
        set
        {
            _selectedCafe = value;
            OnPropertyChanged();
        }
    }

    public ICommand ContinueCommand => new Command(() =>
    {
        if (SelectedCafe == null)
            return;

        Application.Current.OpenWindow(new QueuePage());
    });


}
