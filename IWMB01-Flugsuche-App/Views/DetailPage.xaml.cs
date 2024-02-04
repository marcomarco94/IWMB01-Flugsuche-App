namespace IWMB01_Flugsuche_App.Views;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailPageViewModel pageViewModel)
    {
        InitializeComponent();
        BindingContext = pageViewModel;
    }
    
}