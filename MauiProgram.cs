using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMS.Data;

namespace SMS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        var apiBaseUrl = "http://localhost:5123/";
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
        builder.Services.AddScoped<SMS.Services.ApiClientService>();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped<SMS.Services.SaveActionService>();

#if ANDROID
        // Registers native Android Print Service
        builder.Services.AddSingleton<SMS.Services.IPrintService, SMS.Platforms.Android.Services.AndroidPrintService>();
#elif WINDOWS
        builder.Services.AddSingleton<SMS.Services.IPrintService, SMS.Platforms.Windows.Services.WindowsPrintService>();
#endif

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var connectionString = "Host=localhost;Port=5432;Database=SMS_db;Username=postgres;Password=2931";

        builder.Services.AddDbContextFactory<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqloptions =>
            {
                npgsqloptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            }));

        return builder.Build();
    }
}