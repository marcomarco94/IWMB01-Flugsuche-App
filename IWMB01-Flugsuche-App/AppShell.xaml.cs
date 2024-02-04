namespace IWMB01_Flugsuche_App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ResultPage), typeof(ResultPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}