using CommunityToolkit.Mvvm.ComponentModel;

namespace IWMB01_Flugsuche_App.ViewModels;

public partial class BaseViewModel : ObservableValidator
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;


    public bool IsNotBusy => !IsBusy;
}