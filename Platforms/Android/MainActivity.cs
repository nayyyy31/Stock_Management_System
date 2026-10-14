using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Print;
using Android.Webkit;

namespace SMS
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Enable default printing behavior across Android WebViews
            Android.Webkit.WebView.EnableSlowWholeDocumentDraw();
        }
    }
}
