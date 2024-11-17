using ServiceFromPrivateTemplate.APIs.Dtos;
using ServiceFromPrivateTemplate.Infrastructure.Models;

namespace ServiceFromPrivateTemplate.APIs.Extensions;

public static class FffsExtensions
{
    public static Fff ToDto(this FffDbModel model)
    {
        return new Fff
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FffDbModel ToModel(this FffUpdateInput updateDto, FffWhereUniqueInput uniqueId)
    {
        var fff = new FffDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            fff.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            fff.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return fff;
    }
}
