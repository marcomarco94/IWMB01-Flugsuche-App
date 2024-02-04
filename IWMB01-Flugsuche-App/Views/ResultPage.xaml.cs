namespace IWMB01_Flugsuche_App.Views;

public partial class ResultPage : ContentPage
{
    public ResultPage(ResultPageViewModel pageViewModel)
    {
        InitializeComponent();
        BindingContext = pageViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        (BindingContext as ResultPageViewModel)?.GetScheduledFlightsCommand.Execute(null);
    }
}