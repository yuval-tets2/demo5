using CustomerService.APIs.Dtos;
using CustomerService.Infrastructure.Models;

namespace CustomerService.APIs.Extensions;

public static class InteractionsExtensions
{
    public static Interaction ToDto(this InteractionDbModel model)
    {
        return new Interaction
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Details = model.Details,
            Id = model.Id,
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InteractionDbModel ToModel(
        this InteractionUpdateInput updateDto,
        InteractionWhereUniqueInput uniqueId
    )
    {
        var interaction = new InteractionDbModel
        {
            Id = uniqueId.Id,
            Details = updateDto.Details,
            TypeField = updateDto.TypeField
        };

        if (updateDto.CreatedAt != null)
        {
            interaction.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            interaction.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            interaction.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return interaction;
    }
}
