using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;

namespace SupplierManagementService.APIs;

public interface IDeliveriesService
{
    /// <summary>
    /// Create one Delivery
    /// </summary>
    public Task<Delivery> CreateDelivery(DeliveryCreateInput delivery);

    /// <summary>
    /// Delete one Delivery
    /// </summary>
    public Task DeleteDelivery(DeliveryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Deliveries
    /// </summary>
    public Task<List<Delivery>> Deliveries(DeliveryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Delivery records
    /// </summary>
    public Task<MetadataDto> DeliveriesMeta(DeliveryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Delivery
    /// </summary>
    public Task<Delivery> Delivery(DeliveryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Delivery
    /// </summary>
    public Task UpdateDelivery(DeliveryWhereUniqueInput uniqueId, DeliveryUpdateInput updateDto);

    /// <summary>
    /// Get a PurchaseOrder record for Delivery
    /// </summary>
    public Task<PurchaseOrder> GetPurchaseOrder(DeliveryWhereUniqueInput uniqueId);
}
