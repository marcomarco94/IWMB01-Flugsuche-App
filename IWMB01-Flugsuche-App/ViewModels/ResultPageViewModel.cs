namespace IWMB01_Flugsuche_App.ViewModels;

[QueryProperty(nameof(StartAirportCode), nameof(StartAirportCode))]
[QueryProperty(nameof(TargetAirportCode), nameof(TargetAirportCode))]
[QueryProperty(nameof(SelectedDate), nameof(SelectedDate))]
public partial class ResultPageViewModel : BaseViewModel
{
    private readonly FlightService _flightService;
    private readonly IConnectivity _connectivity;

    public ResultPageViewModel(IConnectivity connectivity, FlightService flightService)
    {
        _flightService = flightService;
        _connectivity = connectivity;
    }

    [ObservableProperty] 
    string _startAirportCode;
    [ObservableProperty]  
    string _targetAirportCode;
    [ObservableProperty]  
    DateTime _selectedDate;
    [ObservableProperty]  
    bool _directFlightsOnly = false;

    [ObservableProperty]  
    Schedule _selectedSchedule;

    [ObservableProperty]  
    ObservableCollection<Schedule> _scheduledFlights;

    [ObservableProperty]  
    ObservableCollection<Schedule> _allScheduledFlights;

    partial void OnDirectFlightsOnlyChanged(bool value)
    {
        if (value)
            ScheduledFlights = new ObservableCollection<Schedule>(AllScheduledFlights.Where(f => f.Flight.Count == 1));
        else
            ScheduledFlights = AllScheduledFlights;
    }

    [RelayCommand]
    private async Task GetScheduledFlightsAsync()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Keine Verbindung!",
                $"Es besteht keine Verbindung zum Internet.", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var flights =
                await _flightService.GetScheduledFlightsAsync(StartAirportCode, TargetAirportCode, SelectedDate);
            AllScheduledFlights = new ObservableCollection<Schedule>(flights.ScheduleResource.Schedule);
            if (DirectFlightsOnly is not true)
            {
                ScheduledFlights = AllScheduledFlights;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error! {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", "Flüge konnten nicht gefunden werden", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToDetails()
    {
        if (SelectedSchedule != null)
        {
            await Shell.Current.GoToAsync(nameof(DetailPage), true,
                new Dictionary<string, object>
                {
                    { "SelectedSchedule", SelectedSchedule }
                });
        }
        else
        {
            await Shell.Current.DisplayAlert("Kein Flug ausgewählt!", "Bitte wählen Sie einen Flug aus.", "OK");
        }
    }

    [RelayCommand]
    private Task GoBack()
    {
        return Shell.Current.GoToAsync("..");
    }
}