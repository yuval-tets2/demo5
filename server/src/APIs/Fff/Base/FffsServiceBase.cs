using Microsoft.EntityFrameworkCore;
using ServiceFromPrivateTemplate.APIs;
using ServiceFromPrivateTemplate.APIs.Common;
using ServiceFromPrivateTemplate.APIs.Dtos;
using ServiceFromPrivateTemplate.APIs.Errors;
using ServiceFromPrivateTemplate.APIs.Extensions;
using ServiceFromPrivateTemplate.Infrastructure;
using ServiceFromPrivateTemplate.Infrastructure.Models;

namespace ServiceFromPrivateTemplate.APIs;

public abstract class FffsServiceBase : IFffsService
{
    protected readonly ServiceFromPrivateTemplateDbContext _context;

    public FffsServiceBase(ServiceFromPrivateTemplateDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one fff
    /// </summary>
    public async Task<Fff> CreateFff(FffCreateInput createDto)
    {
        var fff = new FffDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            fff.Id = createDto.Id;
        }

        _context.Fffs.Add(fff);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FffDbModel>(fff.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one fff
    /// </summary>
    public async Task DeleteFff(FffWhereUniqueInput uniqueId)
    {
        var fff = await _context.Fffs.FindAsync(uniqueId.Id);
        if (fff == null)
        {
            throw new NotFoundException();
        }

        _context.Fffs.Remove(fff);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many fffs
    /// </summary>
    public async Task<List<Fff>> Fffs(FffFindManyArgs findManyArgs)
    {
        var fffs = await _context
            .Fffs.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return fffs.ConvertAll(fff => fff.ToDto());
    }

    /// <summary>
    /// Meta data about fff records
    /// </summary>
    public async Task<MetadataDto> FffsMeta(FffFindManyArgs findManyArgs)
    {
        var count = await _context.Fffs.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one fff
    /// </summary>
    public async Task<Fff> Fff(FffWhereUniqueInput uniqueId)
    {
        var fffs = await this.Fffs(
            new FffFindManyArgs { Where = new FffWhereInput { Id = uniqueId.Id } }
        );
        var fff = fffs.FirstOrDefault();
        if (fff == null)
        {
            throw new NotFoundException();
        }

        return fff;
    }

    /// <summary>
    /// Update one fff
    /// </summary>
    public async Task UpdateFff(FffWhereUniqueInput uniqueId, FffUpdateInput updateDto)
    {
        var fff = updateDto.ToModel(uniqueId);

        _context.Entry(fff).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Fffs.Any(e => e.Id == fff.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
