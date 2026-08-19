using System.Collections.ObjectModel;
using System.Windows.Input;
using Quick_Queue.Models;

namespace Quick_Queue.Views;

public class CafeSelectionViewModel : BindableObject
{
    public ObservableCollection<CafeInfo> Cafes { get; } =
        new ObservableCollection<CafeInfo>
        {
            new CafeInfo { Name = "Main Campus Café", ImagePath = "cafeone.png" },
            new CafeInfo { Name = "Library Café", ImagePath = "cafetwo.png" },
            new CafeInfo { Name = "Tech Hub Café", ImagePath = "cafethree.png" }
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

        SelectedCafeStore.CurrentCafe = SelectedCafe;

        Application.Current.OpenWindow(new CafeDetails());
    });
}
