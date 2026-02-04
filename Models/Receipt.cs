namespace Manifest2ScaleConfig_BlazorWA.Models;

public class PurchaseOrder
{
    public string Action { get; set; } = "NEW";
    public DateTime CreationDateTimeStamp { get; set; }
    public string UserDef6 { get; set; } = "Y";
    public string UserDef7 { get; set; } = string.Empty;
    public string UserDef8 { get; set; } = string.Empty;
    public string UserStamp { get; set; } = "ILSSRV";
    public string Company { get; set; } = "PER-CO-CAF";
    public string PurchaseOrderId { get; set; } = string.Empty;
    public string ShipFrom { get; set; } = string.Empty;
    public string ShipFromName { get; set; } = string.Empty;
    public string SourceAddress { get; set; } = string.Empty;
    public string Warehouse { get; set; } = "PER";
    public string Item { get; set; } = "1111";
    public int Quantity { get; set; }
    public string QuantityUm { get; set; } = "UN";
    public int LineNumber { get; set; } = 1;
}

public class Receipt
{
    public string Action { get; set; } = "NEW";
    public DateTime CreationDateTimeStamp { get; set; }
    public string UserDef6 { get; set; } = string.Empty;
    public string UserDef7 { get; set; } = string.Empty;
    public string UserDef8 { get; set; } = string.Empty;
    public string UserStamp { get; set; } = "ILSSRV";
    public string Company { get; set; } = "PER-CO-CAF";
    public DateTime ReceiptDate { get; set; }
    public string ReceiptId { get; set; } = string.Empty;
    public string ReceiptIdType { get; set; } = "PO";
    public string TrailerId { get; set; } = "1";
    public string ShipFrom { get; set; } = string.Empty;
    public string ShipFromName { get; set; } = string.Empty;
    public string SourceAddress { get; set; } = string.Empty;
    public string Warehouse { get; set; } = "PER";
    public string Item { get; set; } = "1111";
    public int Quantity { get; set; }
    public string QuantityUm { get; set; } = "UN";
    public string ErpOrderLineNum { get; set; } = "1";
    public string PurchaseOrderLineNumber { get; set; } = "1";
    public string PurchaseOrderId { get; set; } = string.Empty;
    public string HarmCode { get; set; } = string.Empty;

    public PurchaseOrder? PurchaseOrder { get; set; }
}

public class Supplier
{
    public string ShipFrom { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Item { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    
    public string FileName { get; set; } = string.Empty;

    public static readonly Supplier AzuraFresh = new()
    {
        ShipFrom = "856946",
        Name = "AZURA FRESH WA PTY LTD",
        Item = "1111",
        Prefix = "CAF",
        FileName = "Azura_Fresh"
    };

    public static readonly Supplier ThemeGroup = new()
    {
        ShipFrom = "222222",
        Name = "THEME GROUP PTY LTD",
        Item = "2222",
        Prefix = "CTG",
        FileName = "Theme_Group"
    };

    public static List<Supplier> All => new() { AzuraFresh, ThemeGroup };

    public override string ToString() => Name;
}