using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs.Extensions;

public static class PaymentsExtensions
{
    public static Payment ToDto(this PaymentDbModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Order = model.OrderId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PaymentDbModel ToModel(
        this PaymentUpdateInput updateDto,
        PaymentWhereUniqueInput uniqueId
    )
    {
        var payment = new PaymentDbModel { Id = uniqueId.Id, Amount = updateDto.Amount };

        if (updateDto.CreatedAt != null)
        {
            payment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Order != null)
        {
            payment.OrderId = updateDto.Order;
        }
        if (updateDto.UpdatedAt != null)
        {
            payment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return payment;
    }
}
