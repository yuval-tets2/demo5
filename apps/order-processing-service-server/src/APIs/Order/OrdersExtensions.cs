using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs.Extensions;

public static class OrdersExtensions
{
    public static Order ToDto(this OrderDbModel model)
    {
        return new Order
        {
            CreatedAt = model.CreatedAt,
            Customer = model.Customer,
            Id = model.Id,
            OrderDate = model.OrderDate,
            OrderItems = model.OrderItems?.Select(x => x.Id).ToList(),
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            Shipments = model.Shipments?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderDbModel ToModel(
        this OrderUpdateInput updateDto,
        OrderWhereUniqueInput uniqueId
    )
    {
        var order = new OrderDbModel
        {
            Id = uniqueId.Id,
            Customer = updateDto.Customer,
            OrderDate = updateDto.OrderDate
        };

        if (updateDto.CreatedAt != null)
        {
            order.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            order.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return order;
    }
}
