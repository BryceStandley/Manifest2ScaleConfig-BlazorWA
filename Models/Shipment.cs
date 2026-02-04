namespace Manifest2ScaleConfig_BlazorWA.Models;

public class Shipment
{
    public string Action { get; set; } = "SAVE";
    public DateTime CreationDateTimeStamp { get; set; }
    public string UserDef1 { get; set; } = string.Empty;
    public string UserDef2 { get; set; } = string.Empty;
    public string UserDef7 { get; set; } = "0";
    public string UserDef8 { get; set; } = "0";
    public string UserStamp { get; set; } = "INTERFACE";
    public string Carrier { get; set; } = "TOLL";
    public string Company { get; set; } = "PER-CO-CAF";
    public string Customer { get; set; } = string.Empty;
    public string FreightBillTo { get; set; } = string.Empty;
    public string CustomerPO { get; set; } = string.Empty;
    public string ErpOrder { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string OrderType { get; set; } = "CAF";
    public DateTime PlannedShipDate { get; set; }
    public DateTime ScheduledShipDate { get; set; }
    public string ShipmentId { get; set; } = string.Empty;
    public string Warehouse { get; set; } = "PER";
    public string Item { get; set; } = "1111";
    public string Category1 { get; set; } = "1111";
    public int Quantity { get; set; }
    public string QuantityUm { get; set; } = "UN";
    public string ErpOrderLineNum { get; set; } = "00001";

    public string DisplayName => $"{ShipmentId} - Store {Customer} (Qty: {Quantity})";
}