using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs.Extensions;

public static class ShipmentsExtensions
{
    public static Shipment ToDto(this ShipmentDbModel model)
    {
        return new Shipment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Order = model.OrderId,
            ShipmentDate = model.ShipmentDate,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ShipmentDbModel ToModel(
        this ShipmentUpdateInput updateDto,
        ShipmentWhereUniqueInput uniqueId
    )
    {
        var shipment = new ShipmentDbModel
        {
            Id = uniqueId.Id,
            ShipmentDate = updateDto.ShipmentDate
        };

        if (updateDto.CreatedAt != null)
        {
            shipment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Order != null)
        {
            shipment.OrderId = updateDto.Order;
        }
        if (updateDto.UpdatedAt != null)
        {
            shipment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return shipment;
    }
}
