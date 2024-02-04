namespace IWMB01_Flugsuche_App.ViewModels;

[QueryProperty(nameof(SelectedSchedule), nameof(SelectedSchedule))]
public partial class DetailPageViewModel : BaseViewModel
{
    [ObservableProperty] private Schedule _selectedSchedule;
    
    [RelayCommand]
    private Task GoBack()
    {
        return Shell.Current.GoToAsync("..");
    }
}