using InventoryManagementService.APIs.Dtos;
using InventoryManagementService.Infrastructure.Models;

namespace InventoryManagementService.APIs.Extensions;

public static class WarehousesExtensions
{
    public static Warehouse ToDto(this WarehouseDbModel model)
    {
        return new Warehouse
        {
            Capacity = model.Capacity,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Location = model.Location,
            StockMovements = model.StockMovements?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static WarehouseDbModel ToModel(
        this WarehouseUpdateInput updateDto,
        WarehouseWhereUniqueInput uniqueId
    )
    {
        var warehouse = new WarehouseDbModel
        {
            Id = uniqueId.Id,
            Capacity = updateDto.Capacity,
            Location = updateDto.Location
        };

        if (updateDto.CreatedAt != null)
        {
            warehouse.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            warehouse.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return warehouse;
    }
}
