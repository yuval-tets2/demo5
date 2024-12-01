using ServiceFromPrivateTemplate.APIs.Common;
using ServiceFromPrivateTemplate.APIs.Dtos;

namespace ServiceFromPrivateTemplate.APIs;

public interface IFffsService
{
    /// <summary>
    /// Create one fff
    /// </summary>
    public Task<Fff> CreateFff(FffCreateInput fff);

    /// <summary>
    /// Delete one fff
    /// </summary>
    public Task DeleteFff(FffWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many fffs
    /// </summary>
    public Task<List<Fff>> Fffs(FffFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about fff records
    /// </summary>
    public Task<MetadataDto> FffsMeta(FffFindManyArgs findManyArgs);

    /// <summary>
    /// Get one fff
    /// </summary>
    public Task<Fff> Fff(FffWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one fff
    /// </summary>
    public Task UpdateFff(FffWhereUniqueInput uniqueId, FffUpdateInput updateDto);
}
