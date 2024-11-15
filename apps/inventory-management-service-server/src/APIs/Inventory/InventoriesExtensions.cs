using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.Infrastructure.Models;

namespace InventoryManagementService.APIs.Extensions;

public static class InventoriesExtensions
{
    public static Inventory ToDto(this InventoryDbModel model)
    {
        return new Inventory
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Product = model.ProductId,
            Quantity = model.Quantity,
            StockMovements = model.StockMovements?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InventoryDbModel ToModel(
        this InventoryUpdateInput updateDto,
        InventoryWhereUniqueInput uniqueId
    )
    {
        var inventory = new InventoryDbModel { Id = uniqueId.Id, Quantity = updateDto.Quantity };

        if (updateDto.CreatedAt != null)
        {
            inventory.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Product != null)
        {
            inventory.ProductId = updateDto.Product;
        }
        if (updateDto.UpdatedAt != null)
        {
            inventory.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return inventory;
    }
}
