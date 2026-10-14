using System.Net.Http.Json;
using SMSShared;

namespace SMS.Services;

public class ApiClientService
{
    private readonly HttpClient _http;

    public ApiClientService(HttpClient http)
    {
        _http = http;
    }

    // Stocks
    public Task<List<StockItemDto>?> GetStocksAsync() => _http.GetFromJsonAsync<List<StockItemDto>>("api/stocks");
    public Task AddStockAsync(StockItemDto item) => _http.PostAsJsonAsync("api/stocks", item);
    public Task DeleteStockAsync(int id) => _http.DeleteAsync($"api/stocks/{id}");

    // Purchases
    public Task<List<PurchaseDto>?> GetPurchasesAsync() => _http.GetFromJsonAsync<List<PurchaseDto>>("api/purchases");
    public Task<PurchaseDto?> GetPurchaseByIdAsync(int id) => _http.GetFromJsonAsync<PurchaseDto>($"api/purchases/{id}");
    public Task CreatePurchaseAsync(PurchaseDto dto) => _http.PostAsJsonAsync("api/purchases", dto);
    public Task UpdatePurchaseAsync(int id, PurchaseDto dto) => _http.PutAsJsonAsync($"api/purchases/{id}", dto);
    public Task DeletePurchaseAsync(int id) => _http.DeleteAsync($"api/purchases/{id}");

    // Sales
    public Task<List<SaleDto>?> GetSalesAsync() => _http.GetFromJsonAsync<List<SaleDto>>("api/sales");
    public Task<SaleDto?> GetSaleByIdAsync(int id) => _http.GetFromJsonAsync<SaleDto>($"api/sales/{id}");
    public Task CreateSaleAsync(SaleDto dto) => _http.PostAsJsonAsync("api/sales", dto);
    public Task UpdateSaleAsync(int id, SaleDto dto) => _http.PutAsJsonAsync($"api/sales/{id}", dto);
    public Task DeleteSaleAsync(int id) => _http.DeleteAsync($"api/sales/{id}");

    // Reports
    public Task<List<ReportRecordDto>?> GetReportsAsync(DateTime from, DateTime to, string type) =>
        _http.GetFromJsonAsync<List<ReportRecordDto>>($"api/reports?fromDate={from:yyyy-MM-dd}&toDate={to:yyyy-MM-dd}&type={type}");
}