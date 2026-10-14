#if ANDROID
using Android.Content;
using Android.Print;
using Android.Webkit;
using SMS.Services;

namespace SMS.Platforms.Android.Services;

public class AndroidPrintService : IPrintService
{
    public Task PrintHtmlAsync(string htmlContent, string jobName = "Document")
    {
        var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        if (activity == null) return Task.CompletedTask;

        activity.RunOnUiThread(() =>
        {
            var webView = new global::Android.Webkit.WebView(activity);
            webView.SetWebViewClient(new PrintWebViewClient(activity, jobName));
            webView.LoadDataWithBaseURL(null, htmlContent, "text/html", "UTF-8", null);
        });

        return Task.CompletedTask;
    }

    private class PrintWebViewClient : WebViewClient
    {
        private readonly Context _context;
        private readonly string _jobName;

        public PrintWebViewClient(Context context, string jobName)
        {
            _context = context;
            _jobName = jobName;
        }

        public override void OnPageFinished(global::Android.Webkit.WebView? view, string? url)
        {
            if (view == null) return;

            var printManager = (PrintManager?)_context.GetSystemService(Context.PrintService);
            if (printManager != null)
            {
                var printAdapter = view.CreatePrintDocumentAdapter(_jobName);
                printManager.Print(_jobName, printAdapter, new PrintAttributes.Builder().Build());
            }
        }
    }
}
#endif