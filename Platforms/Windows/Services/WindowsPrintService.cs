#if WINDOWS
using System.Diagnostics;
using System.IO;
using SMS.Services;

namespace SMS.Platforms.Windows.Services;

public class WindowsPrintService : IPrintService
{
    public async Task PrintHtmlAsync(string htmlContent, string jobName = "Document")
    {
        try
        {
            // Inject an auto-print script so the browser opens the Windows Print dialog immediately
            string autoPrintScript = @"
                <script type='text/javascript'>
                    window.addEventListener('load', function() {
                        window.focus();
                        window.print();
                    });
                </script>";

            string fullHtml;
            if (htmlContent.Contains("<body", StringComparison.OrdinalIgnoreCase))
            {
                // Insert script right before closing body
                fullHtml = htmlContent.Replace("</body>", $"{autoPrintScript}</body>", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                fullHtml = $@"<!DOCTYPE html>
<html>
<head>
    <title>{jobName}</title>
    {autoPrintScript}
    <style>
        body {{ font-family: monospace; font-size: 13px; padding: 20px; white-space: pre-wrap; }}
    </style>
</head>
<body>{htmlContent}</body>
</html>";
            }

            // Save to temp HTML file
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{jobName}_{Guid.NewGuid():N}.html");
            await File.WriteAllTextAsync(tempFilePath, fullHtml);

            // Launch the file with Windows Shell - opens Edge/default browser and pops print dialog
            var processStartInfo = new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true
            };

            Process.Start(processStartInfo);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[WindowsPrintService Error]: {ex.Message}");
            throw;
        }
    }
}
#endif