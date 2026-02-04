namespace Manifest2ScaleConfig_BlazorWA.Models;

public class Store
{
    public string StoreNumber { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;

    public override string ToString() => $"{StoreNumber} - {StoreName}";
}