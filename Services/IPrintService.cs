namespace SMS.Services;

public interface IPrintService
{
    Task PrintHtmlAsync(string htmlContent, string jobName = "Document");
}