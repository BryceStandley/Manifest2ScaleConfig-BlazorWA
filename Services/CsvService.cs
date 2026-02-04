using Manifest2ScaleConfig_BlazorWA.Models;

namespace Manifest2ScaleConfig_BlazorWA.Services;

public class CsvService
{
    private readonly HttpClient _httpClient;

    public CsvService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Store>> LoadStoresAsync()
    {
        var stores = new List<Store>();

        try
        {
            var csvContent = await _httpClient.GetStringAsync("stores.csv");
            var lines = csvContent.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    var storeNumber = parts[0].Trim();
                    var storeName = parts[1].Trim();

                    if (!string.IsNullOrWhiteSpace(storeNumber))
                    {
                        stores.Add(new Store
                        {
                            StoreNumber = storeNumber,
                            StoreName = storeName
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading stores.csv: {ex.Message}");
        }

        return stores;
    }

    public bool IsValidStore(string storeNumber, List<Store> stores)
    {
        return stores.Any(s => s.StoreNumber.Equals(storeNumber, StringComparison.OrdinalIgnoreCase));
    }
}