using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs.Extensions;

public static class DeliveriesExtensions
{
    public static Delivery ToDto(this DeliveryDbModel model)
    {
        return new Delivery
        {
            CreatedAt = model.CreatedAt,
            DeliveryDate = model.DeliveryDate,
            Id = model.Id,
            PurchaseOrder = model.PurchaseOrderId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DeliveryDbModel ToModel(
        this DeliveryUpdateInput updateDto,
        DeliveryWhereUniqueInput uniqueId
    )
    {
        var delivery = new DeliveryDbModel
        {
            Id = uniqueId.Id,
            DeliveryDate = updateDto.DeliveryDate
        };

        if (updateDto.CreatedAt != null)
        {
            delivery.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.PurchaseOrder != null)
        {
            delivery.PurchaseOrderId = updateDto.PurchaseOrder;
        }
        if (updateDto.UpdatedAt != null)
        {
            delivery.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return delivery;
    }
}
