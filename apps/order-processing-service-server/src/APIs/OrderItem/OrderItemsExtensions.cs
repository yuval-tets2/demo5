using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs.Extensions;

public static class OrderItemsExtensions
{
    public static OrderItem ToDto(this OrderItemDbModel model)
    {
        return new OrderItem
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Order = model.OrderId,
            Product = model.Product,
            Quantity = model.Quantity,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderItemDbModel ToModel(
        this OrderItemUpdateInput updateDto,
        OrderItemWhereUniqueInput uniqueId
    )
    {
        var orderItem = new OrderItemDbModel
        {
            Id = uniqueId.Id,
            Product = updateDto.Product,
            Quantity = updateDto.Quantity
        };

        if (updateDto.CreatedAt != null)
        {
            orderItem.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Order != null)
        {
            orderItem.OrderId = updateDto.Order;
        }
        if (updateDto.UpdatedAt != null)
        {
            orderItem.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return orderItem;
    }
}
