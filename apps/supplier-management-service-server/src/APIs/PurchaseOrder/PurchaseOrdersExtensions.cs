using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs.Extensions;

public static class PurchaseOrdersExtensions
{
    public static PurchaseOrder ToDto(this PurchaseOrderDbModel model)
    {
        return new PurchaseOrder
        {
            CreatedAt = model.CreatedAt,
            Deliveries = model.Deliveries?.Select(x => x.Id).ToList(),
            Id = model.Id,
            OrderDate = model.OrderDate,
            Supplier = model.SupplierId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PurchaseOrderDbModel ToModel(
        this PurchaseOrderUpdateInput updateDto,
        PurchaseOrderWhereUniqueInput uniqueId
    )
    {
        var purchaseOrder = new PurchaseOrderDbModel
        {
            Id = uniqueId.Id,
            OrderDate = updateDto.OrderDate
        };

        if (updateDto.CreatedAt != null)
        {
            purchaseOrder.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Supplier != null)
        {
            purchaseOrder.SupplierId = updateDto.Supplier;
        }
        if (updateDto.UpdatedAt != null)
        {
            purchaseOrder.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return purchaseOrder;
    }
}
