namespace IWMB01_Flugsuche_App.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        GetNearestAirpot();
    }
    
    void GetNearestAirpot()
    {
        (BindingContext as MainPageViewModel)?.GetAirportByCurrentLocationCommand.ExecuteAsync(null);
    }

    void OnGridTapped(object sender, EventArgs e)
    {
        StartSearchBar.Unfocus();
        TargetSearchBar.Unfocus();
    }
}