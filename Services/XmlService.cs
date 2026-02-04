using System.Xml.Linq;
using Manifest2ScaleConfig_BlazorWA.Models;

namespace Manifest2ScaleConfig_BlazorWA.Services;

public class XmlService
{
    private static readonly XNamespace ns = "http://www.manh.com/ILSNET/Interface";

    public List<Shipment> LoadShipments(string xmlContent)
    {
        var shipments = new List<Shipment>();
        var doc = XDocument.Parse(xmlContent);

        foreach (var shipmentElement in doc.Descendants(ns + "Shipment"))
        {
            var shipment = new Shipment
            {
                Action = GetElementValue(shipmentElement, "Action"),
                CreationDateTimeStamp = ParseDateTime(GetElementValue(shipmentElement, "CreationDateTimeStamp")),
                UserDef1 = GetElementValue(shipmentElement, "UserDef1"),
                UserDef2 = GetElementValue(shipmentElement, "UserDef2"),
                UserDef7 = GetElementValue(shipmentElement, "UserDef7"),
                UserDef8 = GetElementValue(shipmentElement, "UserDef8"),
                UserStamp = GetElementValue(shipmentElement, "UserStamp"),
                Carrier = GetElementValue(shipmentElement, "Carrier/Carrier"),
                Company = GetElementValue(shipmentElement, "Customer/Company"),
                Customer = GetElementValue(shipmentElement, "Customer/Customer"),
                FreightBillTo = GetElementValue(shipmentElement, "Customer/FreightBillTo"),
                CustomerPO = GetElementValue(shipmentElement, "CustomerPO"),
                ErpOrder = GetElementValue(shipmentElement, "ErpOrder"),
                OrderDate = ParseDateTime(GetElementValue(shipmentElement, "OrderDate")),
                OrderType = GetElementValue(shipmentElement, "OrderType"),
                PlannedShipDate = ParseDateTime(GetElementValue(shipmentElement, "PlannedShipDate")),
                ScheduledShipDate = ParseDateTime(GetElementValue(shipmentElement, "ScheduledShipDate")),
                ShipmentId = GetElementValue(shipmentElement, "ShipmentId"),
                Warehouse = GetElementValue(shipmentElement, "Warehouse"),
                Item = GetElementValue(shipmentElement, "Details/ShipmentDetail/SKU/Item"),
                Category1 = GetElementValue(shipmentElement, "Details/ShipmentDetail/SKU/ItemCategories/Category1"),
                Quantity = int.TryParse(GetElementValue(shipmentElement, "Details/ShipmentDetail/SKU/Quantity"), out var qty) ? qty : 0,
                QuantityUm = GetElementValue(shipmentElement, "Details/ShipmentDetail/SKU/QuantityUm"),
                ErpOrderLineNum = GetElementValue(shipmentElement, "Details/ShipmentDetail/ErpOrderLineNum")
            };

            shipments.Add(shipment);
        }

        return shipments;
    }

    public string SaveShipments(List<Shipment> shipments)
    {
        var root = new XElement(ns + "Shipments");

        foreach (var shipment in shipments)
        {
            var shipmentElement = new XElement(ns + "Shipment",
                new XElement(ns + "Action", shipment.Action),
                new XElement(ns + "CreationDateTimeStamp", FormatDateTime(shipment.CreationDateTimeStamp)),
                new XElement(ns + "UserDef1", shipment.UserDef1),
                new XElement(ns + "UserDef2", shipment.UserDef2),
                new XElement(ns + "UserDef7", shipment.UserDef7),
                new XElement(ns + "UserDef8", shipment.UserDef8),
                new XElement(ns + "UserStamp", shipment.UserStamp),
                new XElement(ns + "Carrier",
                    new XElement(ns + "Action", "Save"),
                    new XElement(ns + "Carrier", shipment.Carrier)
                ),
                new XElement(ns + "Customer",
                    new XElement(ns + "Company", shipment.Company),
                    new XElement(ns + "Customer", shipment.Customer),
                    new XElement(ns + "FreightBillTo", shipment.FreightBillTo)
                ),
                new XElement(ns + "CustomerPO", shipment.CustomerPO),
                new XElement(ns + "ErpOrder", shipment.ErpOrder),
                new XElement(ns + "OrderDate", FormatDateTime(shipment.OrderDate)),
                new XElement(ns + "OrderType", shipment.OrderType),
                new XElement(ns + "PlannedShipDate", FormatDateTime(shipment.PlannedShipDate)),
                new XElement(ns + "ScheduledShipDate", FormatDateTime(shipment.ScheduledShipDate)),
                new XElement(ns + "ShipmentId", shipment.ShipmentId),
                new XElement(ns + "Warehouse", shipment.Warehouse),
                new XElement(ns + "Details",
                    new XElement(ns + "ShipmentDetail",
                        new XElement(ns + "Action", shipment.Action),
                        new XElement(ns + "CreationDateTimeStamp", FormatDateTime(shipment.CreationDateTimeStamp)),
                        new XElement(ns + "ErpOrder", shipment.ErpOrder),
                        new XElement(ns + "ErpOrderLineNum", shipment.ErpOrderLineNum),
                        new XElement(ns + "SKU",
                            new XElement(ns + "Company", shipment.Company),
                            new XElement(ns + "Item", shipment.Item),
                            new XElement(ns + "ItemCategories",
                                new XElement(ns + "Category1", shipment.Category1)
                            ),
                            new XElement(ns + "Quantity", shipment.Quantity),
                            new XElement(ns + "QuantityUm", shipment.QuantityUm)
                        )
                    )
                )
            );

            root.Add(shipmentElement);
        }

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            root
        );

        return doc.ToString();
    }

    public Receipt LoadReceipt(string xmlContent)
    {
        var doc = XDocument.Parse(xmlContent);
        var receiptElement = doc.Descendants(ns + "Receipt").FirstOrDefault();
        var poElement = doc.Descendants(ns + "PurchaseOrder").FirstOrDefault();

        if (receiptElement == null)
            throw new InvalidOperationException("No Receipt element found in file");

        var receipt = new Receipt
        {
            Action = GetElementValue(receiptElement, "Action"),
            CreationDateTimeStamp = ParseDateTime(GetElementValue(receiptElement, "CreationDateTimeStamp")),
            UserDef6 = GetElementValue(receiptElement, "UserDef6"),
            UserDef7 = GetElementValue(receiptElement, "UserDef7"),
            UserDef8 = GetElementValue(receiptElement, "UserDef8"),
            UserStamp = GetElementValue(receiptElement, "UserStamp"),
            Company = GetElementValue(receiptElement, "Company"),
            ReceiptDate = ParseDateTime(GetElementValue(receiptElement, "ReceiptDate")),
            ReceiptId = GetElementValue(receiptElement, "ReceiptId"),
            ReceiptIdType = GetElementValue(receiptElement, "ReceiptIdType"),
            TrailerId = GetElementValue(receiptElement, "TrailerId"),
            ShipFrom = GetElementValue(receiptElement, "Vendor/ShipFrom"),
            ShipFromName = GetElementValue(receiptElement, "Vendor/ShipFromAddress/Name"),
            SourceAddress = GetElementValue(receiptElement, "Vendor/SourceAddress"),
            Warehouse = GetElementValue(receiptElement, "Warehouse"),
            Item = GetElementValue(receiptElement, "Details/ReceiptDetail/SKU/Item"),
            Quantity = int.TryParse(GetElementValue(receiptElement, "Details/ReceiptDetail/SKU/Quantity"), out var qty) ? qty : 0,
            QuantityUm = GetElementValue(receiptElement, "Details/ReceiptDetail/SKU/QuantityUm"),
            ErpOrderLineNum = GetElementValue(receiptElement, "Details/ReceiptDetail/ErpOrderLineNum"),
            PurchaseOrderLineNumber = GetElementValue(receiptElement, "Details/ReceiptDetail/PurchaseOrderLineNumber"),
            PurchaseOrderId = GetElementValue(receiptElement, "Details/ReceiptDetail/PurchaseOrderId"),
            HarmCode = GetElementValue(receiptElement, "Details/ReceiptDetail/SKU/HarmCode")
        };

        if (poElement != null)
        {
            receipt.PurchaseOrder = new PurchaseOrder
            {
                Action = GetElementValue(poElement, "Action"),
                CreationDateTimeStamp = ParseDateTime(GetElementValue(poElement, "CreationDateTimeStamp")),
                UserDef6 = GetElementValue(poElement, "UserDef6"),
                UserDef7 = GetElementValue(poElement, "UserDef7"),
                UserDef8 = GetElementValue(poElement, "UserDef8"),
                UserStamp = GetElementValue(poElement, "UserStamp"),
                Company = GetElementValue(poElement, "Company"),
                PurchaseOrderId = GetElementValue(poElement, "PurchaseOrderId"),
                ShipFrom = GetElementValue(poElement, "Vendor/ShipFrom"),
                ShipFromName = GetElementValue(poElement, "Vendor/ShipFromAddress/Name"),
                SourceAddress = GetElementValue(poElement, "Vendor/SourceAddress"),
                Warehouse = GetElementValue(poElement, "Warehouse"),
                Item = GetElementValue(poElement, "Details/PurchaseOrderDetail/SKU/Item"),
                Quantity = int.TryParse(GetElementValue(poElement, "Details/PurchaseOrderDetail/SKU/Quantity"), out var poQty) ? poQty : 0,
                QuantityUm = GetElementValue(poElement, "Details/PurchaseOrderDetail/SKU/QuantityUm"),
                LineNumber = int.TryParse(GetElementValue(poElement, "Details/PurchaseOrderDetail/LineNumber"), out var lineNum) ? lineNum : 1
            };
        }

        return receipt;
    }

    public string SaveReceipt(Receipt receipt)
    {
        var root = new XElement(ns + "Receipts");

        if (receipt.PurchaseOrder != null)
        {
            var po = receipt.PurchaseOrder;
            var poElement = new XElement(ns + "PurchaseOrder",
                new XElement(ns + "Action", po.Action),
                new XElement(ns + "CreationDateTimeStamp", FormatDateTime(po.CreationDateTimeStamp)),
                new XElement(ns + "UserDef6", po.UserDef6),
                new XElement(ns + "UserDef7", po.UserDef7),
                new XElement(ns + "UserDef8", po.UserDef8),
                new XElement(ns + "UserStamp", po.UserStamp),
                new XElement(ns + "Company", po.Company),
                new XElement(ns + "PurchaseOrderId", po.PurchaseOrderId),
                new XElement(ns + "Vendor",
                    new XElement(ns + "Action", po.Action),
                    new XElement(ns + "Company", po.Company),
                    new XElement(ns + "ShipFrom", po.ShipFrom),
                    new XElement(ns + "ShipFromAddress",
                        new XElement(ns + "Name", po.ShipFromName)
                    ),
                    new XElement(ns + "SourceAddress", po.SourceAddress)
                ),
                new XElement(ns + "Warehouse", po.Warehouse),
                new XElement(ns + "Details",
                    new XElement(ns + "PurchaseOrderDetail",
                        new XElement(ns + "Action", po.Action),
                        new XElement(ns + "UserDef1", po.ShipFrom),
                        new XElement(ns + "LineNumber", po.LineNumber),
                        new XElement(ns + "SKU",
                            new XElement(ns + "Company", po.Company),
                            new XElement(ns + "Item", po.Item),
                            new XElement(ns + "Quantity", po.Quantity),
                            new XElement(ns + "QuantityUm", po.QuantityUm)
                        )
                    )
                )
            );
            root.Add(poElement);
        }

        var receiptElement = new XElement(ns + "Receipt",
            new XElement(ns + "Action", receipt.Action),
            new XElement(ns + "CreationDateTimeStamp", FormatDateTime(receipt.CreationDateTimeStamp)),
            new XElement(ns + "UserDef6", receipt.UserDef6),
            new XElement(ns + "UserDef7", receipt.UserDef7),
            new XElement(ns + "UserDef8", receipt.UserDef8),
            new XElement(ns + "UserStamp", receipt.UserStamp),
            new XElement(ns + "Company", receipt.Company),
            new XElement(ns + "ReceiptDate", FormatDateTime(receipt.ReceiptDate)),
            new XElement(ns + "ReceiptId", receipt.ReceiptId),
            new XElement(ns + "ReceiptIdType", receipt.ReceiptIdType),
            new XElement(ns + "TrailerId", receipt.TrailerId),
            new XElement(ns + "Vendor",
                new XElement(ns + "Company", receipt.Company),
                new XElement(ns + "ShipFrom", receipt.ShipFrom),
                new XElement(ns + "ShipFromAddress",
                    new XElement(ns + "Name", receipt.ShipFromName)
                ),
                new XElement(ns + "SourceAddress", receipt.SourceAddress)
            ),
            new XElement(ns + "Warehouse", receipt.Warehouse),
            new XElement(ns + "Details",
                new XElement(ns + "ReceiptDetail",
                    new XElement(ns + "Action", receipt.Action),
                    new XElement(ns + "UserDef1", receipt.ShipFrom),
                    new XElement(ns + "UserDef6", receipt.UserDef6),
                    new XElement(ns + "ErpOrderLineNum", receipt.ErpOrderLineNum),
                    new XElement(ns + "PurchaseOrderLineNumber", receipt.PurchaseOrderLineNumber),
                    new XElement(ns + "PurchaseOrderId", receipt.PurchaseOrderId),
                    new XElement(ns + "SKU",
                        new XElement(ns + "Company", receipt.Company),
                        new XElement(ns + "HarmCode", receipt.HarmCode),
                        new XElement(ns + "Item", receipt.Item),
                        new XElement(ns + "Quantity", receipt.Quantity),
                        new XElement(ns + "QuantityUm", receipt.QuantityUm)
                    )
                )
            )
        );
        root.Add(receiptElement);

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            root
        );

        return doc.ToString();
    }

    private string GetElementValue(XElement parent, string path)
    {
        var parts = path.Split('/');
        var current = parent;

        foreach (var part in parts)
        {
            current = current?.Element(ns + part);
            if (current == null)
                return string.Empty;
        }

        return current?.Value ?? string.Empty;
    }

    private DateTime ParseDateTime(string value)
    {
        if (DateTime.TryParse(value, out var result))
            return result;
        return DateTime.Now;
    }

    private string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
    }

    public Supplier DetectSupplier(List<Shipment> shipments)
    {
        if (shipments.Count == 0)
            return Supplier.AzuraFresh;

        var firstShipment = shipments.First();
        
        if (firstShipment.Item == "2222" || firstShipment.ShipmentId.StartsWith("CTG-"))
            return Supplier.ThemeGroup;

        return Supplier.AzuraFresh;
    }
}