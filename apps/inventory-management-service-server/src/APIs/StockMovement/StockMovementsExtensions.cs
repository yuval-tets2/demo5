using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.Infrastructure.Models;

namespace InventoryManagementService.APIs.Extensions;

public static class StockMovementsExtensions
{
    public static StockMovement ToDto(this StockMovementDbModel model)
    {
        return new StockMovement
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Inventory = model.InventoryId,
            Quantity = model.Quantity,
            UpdatedAt = model.UpdatedAt,
            Warehouse = model.WarehouseId,
        };
    }

    public static StockMovementDbModel ToModel(
        this StockMovementUpdateInput updateDto,
        StockMovementWhereUniqueInput uniqueId
    )
    {
        var stockMovement = new StockMovementDbModel
        {
            Id = uniqueId.Id,
            Quantity = updateDto.Quantity
        };

        if (updateDto.CreatedAt != null)
        {
            stockMovement.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Inventory != null)
        {
            stockMovement.InventoryId = updateDto.Inventory;
        }
        if (updateDto.UpdatedAt != null)
        {
            stockMovement.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.Warehouse != null)
        {
            stockMovement.WarehouseId = updateDto.Warehouse;
        }

        return stockMovement;
    }
}
