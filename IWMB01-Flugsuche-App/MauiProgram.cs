using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IWMB01_Flugsuche_App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Configuration.AddConfiguration(AddConfiguration());
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<FlightApiHelper>();
        builder.Services.AddSingleton<FlightService>();
        builder.Services.AddSingleton<FlightAccessHelper>();
        builder.Services.AddSingleton<GeoService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddTransient<ResultPageViewModel>();
        builder.Services.AddTransient<ResultPage>();

        builder.Services.AddTransient<DetailPageViewModel>();
        builder.Services.AddTransient<DetailPage>();

        return builder.Build();
    }

    private static IConfiguration AddConfiguration()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "IWMB01_Flugsuche_App.appsettings.json";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        return new ConfigurationBuilder()
            .AddJsonStream(memoryStream)
            .Build();
    }
}