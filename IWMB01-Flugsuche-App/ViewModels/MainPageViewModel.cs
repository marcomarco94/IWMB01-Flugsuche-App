using System.ComponentModel.DataAnnotations;


namespace IWMB01_Flugsuche_App.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly FlightService _flightService;
    private readonly IConnectivity _connectivity;

    public MainPageViewModel(IConnectivity connectivity, FlightService flightService)
    {
        _flightService = flightService;
        _connectivity = connectivity;
    }
    
    [ObservableProperty] 
    [Required]
    string _startAirportCode;
    [ObservableProperty] 
    [Required]
    string _startAirportCity;
    [ObservableProperty]
    string _startAirportSearchText;
    [ObservableProperty] 
    [Required]
    string _targetAirportCode;
    [ObservableProperty] 
    [Required]
    string _targetAirportCity;
    [ObservableProperty]
    string _targetAirportSearchText;
    [ObservableProperty]
    [Required]
    DateTime _selectedDate = DateTime.Now;
    
    
    
    [RelayCommand]
    private async Task GetAirportByCurrentLocationAsync()
    {
        IsBusy = true;
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Keine Verbindung!",
                    $"Es besteht keine Verbindung zum Internet.", "OK");
                return;
            }

            var airports = await _flightService.GetAirportByCurrentLocationAsync();
            StartAirportCode = airports.NearestAirportResource.Airports.Airport[0].AirportCode;
            StartAirportCity = airports.NearestAirportResource.Airports.Airport[0].Names.Name.City;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error! {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", "Naheliegenster Flughafen konnte nicht gefunden werden", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GetAirportByCityAsync(bool startAirport)
    {
        try
        {
            IsBusy = true;
            if (startAirport is true )
            {
                var airport = await _flightService.GetAirportByCityAsync(StartAirportSearchText);
                StartAirportCode = airport.NearestAirportResource.Airports.Airport[0].AirportCode;
                StartAirportCity = airport.NearestAirportResource.Airports.Airport[0].Names.Name.City;
            }
            else
            {
                var airport = await _flightService.GetAirportByCityAsync(TargetAirportSearchText);
                TargetAirportCode = airport.NearestAirportResource.Airports.Airport[0].AirportCode;
                TargetAirportCity = airport.NearestAirportResource.Airports.Airport[0].Names.Name.City;
            }
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error! {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", "Flughafen konnte nicht ermittelt werden.\nBitte einen anderen Stadtnamen verwenden", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    [RelayCommand]
    private async Task GetAirportByCodeAsync(bool startAirport)
    {
        try
        {
            IsBusy = true;
            if (startAirport is true)
            {
                var airports = await _flightService.GetAirportByCodeAsync(StartAirportSearchText);
                StartAirportCode = airports.AirportResource.Airports.Airport[0].AirportCode;
                StartAirportCity = airports.AirportResource.Airports.Airport[0].Names.Name.City;
            }
            else
            {
                var airports = await _flightService.GetAirportByCodeAsync(TargetAirportSearchText);
                TargetAirportCode = airports.AirportResource.Airports.Airport[0].AirportCode;
                TargetAirportCity = airports.AirportResource.Airports.Airport[0].Names.Name.City;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error! {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", "Flughafen konnte nicht ermittelt werden.\nBitte einen anderen IATA-Code verwenden", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    
    [RelayCommand]
    private async Task SearchStartAirportsAsync()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Keine Verbindung!",
                $"Es besteht keine Verbindung zum Internet.", "OK");
            return;
        }
        
        if (StartAirportSearchText.Length == 3)
        {
            await GetAirportByCodeCommand.ExecuteAsync(true);
        }
        if (StartAirportSearchText.Length > 3)
        {
            await GetAirportByCityCommand.ExecuteAsync(true);
        }
    }
    
    [RelayCommand]
    private async Task SearchTargetAirportAsync()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Keine Verbindung!",
                $"Es besteht keine Verbindung zum Internet.", "OK");
            return;
        }
        
        if (TargetAirportSearchText.Length == 3)
        {
            await GetAirportByCodeCommand.ExecuteAsync(false);
        }
        if (TargetAirportSearchText.Length > 3)
        {
            await GetAirportByCityCommand.ExecuteAsync(false);
        }
    }

    [RelayCommand]
    async Task Navigate()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await Shell.Current.DisplayAlert("Error", "Bitte geben Sie den Start- und Zielflughafen an", "OK");
            return;
        }
        await Shell.Current.GoToAsync(nameof(ResultPage), true,
            new Dictionary<string, object>
            {
                { "StartAirportCode", StartAirportCode },
                { "TargetAirportCode", TargetAirportCode },
                { "SelectedDate", SelectedDate }
            });
    }
    
}
