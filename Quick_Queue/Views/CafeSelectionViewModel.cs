using System.Collections.ObjectModel;
using System.Windows.Input;
using Quick_Queue.Models;
using Quick_Queue.Services;

namespace Quick_Queue.Views;

public class CafeSelectionViewModel : BindableObject
{
    private readonly DatabaseService _database;

    public ObservableCollection<Cafe> Cafes { get; } = new();

    private Cafe? _selectedCafe;

    public Cafe? SelectedCafe
    {
        get => _selectedCafe;
        set
        {
            _selectedCafe = value;
            OnPropertyChanged();
        }
    }

    public ICommand ContinueCommand { get; }

    public CafeSelectionViewModel(DatabaseService database)
    {
        _database = database;

        ContinueCommand = new Command(async () => await OnContinueClicked());

        LoadCafes();
    }

    private async void LoadCafes()
    {
        var cafes = await _database.GetCafesAsync();

        Cafes.Clear();

        foreach (var cafe in cafes)
        {
            Cafes.Add(cafe);
        }
    }

    private async Task OnContinueClicked()
    {
        if (SelectedCafe == null)
            return;

        SelectedCafeStore.CurrentCafe = SelectedCafe;

        await Shell.Current.GoToAsync(nameof(CafeDetails));
    }
}