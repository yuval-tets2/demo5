using OrderProcessingService.APIs.Common;
using OrderProcessingService.APIs.Dtos;

namespace OrderProcessingService.APIs;

public interface IShipmentsService
{
    /// <summary>
    /// Create one Shipment
    /// </summary>
    public Task<Shipment> CreateShipment(ShipmentCreateInput shipment);

    /// <summary>
    /// Delete one Shipment
    /// </summary>
    public Task DeleteShipment(ShipmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Shipments
    /// </summary>
    public Task<List<Shipment>> Shipments(ShipmentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Shipment records
    /// </summary>
    public Task<MetadataDto> ShipmentsMeta(ShipmentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Shipment
    /// </summary>
    public Task<Shipment> Shipment(ShipmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Shipment
    /// </summary>
    public Task UpdateShipment(ShipmentWhereUniqueInput uniqueId, ShipmentUpdateInput updateDto);

    /// <summary>
    /// Get a Order record for Shipment
    /// </summary>
    public Task<Order> GetOrder(ShipmentWhereUniqueInput uniqueId);
}
