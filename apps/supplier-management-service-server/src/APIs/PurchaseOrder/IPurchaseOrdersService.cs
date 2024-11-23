using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;

namespace SupplierManagementService.APIs;

public interface IPurchaseOrdersService
{
    /// <summary>
    /// Create one PurchaseOrder
    /// </summary>
    public Task<PurchaseOrder> CreatePurchaseOrder(PurchaseOrderCreateInput purchaseorder);

    /// <summary>
    /// Delete one PurchaseOrder
    /// </summary>
    public Task DeletePurchaseOrder(PurchaseOrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many PurchaseOrders
    /// </summary>
    public Task<List<PurchaseOrder>> PurchaseOrders(PurchaseOrderFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about PurchaseOrder records
    /// </summary>
    public Task<MetadataDto> PurchaseOrdersMeta(PurchaseOrderFindManyArgs findManyArgs);

    /// <summary>
    /// Get one PurchaseOrder
    /// </summary>
    public Task<PurchaseOrder> PurchaseOrder(PurchaseOrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one PurchaseOrder
    /// </summary>
    public Task UpdatePurchaseOrder(
        PurchaseOrderWhereUniqueInput uniqueId,
        PurchaseOrderUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Deliveries records to PurchaseOrder
    /// </summary>
    public Task ConnectDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] deliveriesId
    );

    /// <summary>
    /// Disconnect multiple Deliveries records from PurchaseOrder
    /// </summary>
    public Task DisconnectDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] deliveriesId
    );

    /// <summary>
    /// Find multiple Deliveries records for PurchaseOrder
    /// </summary>
    public Task<List<Delivery>> FindDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryFindManyArgs DeliveryFindManyArgs
    );

    /// <summary>
    /// Update multiple Deliveries records for PurchaseOrder
    /// </summary>
    public Task UpdateDeliveries(
        PurchaseOrderWhereUniqueInput uniqueId,
        DeliveryWhereUniqueInput[] deliveriesId
    );

    /// <summary>
    /// Get a Supplier record for PurchaseOrder
    /// </summary>
    public Task<Supplier> GetSupplier(PurchaseOrderWhereUniqueInput uniqueId);
}
