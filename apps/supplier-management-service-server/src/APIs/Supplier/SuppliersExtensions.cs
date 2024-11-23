using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs.Extensions;

public static class SuppliersExtensions
{
    public static Supplier ToDto(this SupplierDbModel model)
    {
        return new Supplier
        {
            ContactInfo = model.ContactInfo,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            PurchaseOrders = model.PurchaseOrders?.Select(x => x.Id).ToList(),
            SupplyContracts = model.SupplyContracts?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SupplierDbModel ToModel(
        this SupplierUpdateInput updateDto,
        SupplierWhereUniqueInput uniqueId
    )
    {
        var supplier = new SupplierDbModel
        {
            Id = uniqueId.Id,
            ContactInfo = updateDto.ContactInfo,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            supplier.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            supplier.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return supplier;
    }
}
