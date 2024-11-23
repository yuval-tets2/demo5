using Microsoft.EntityFrameworkCore;
using SupplierManagementService.APIs;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.APIs.Errors;
using SupplierManagementService.APIs.Extensions;
using SupplierManagementService.Infrastructure;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs;

public abstract class SupplyContractsServiceBase : ISupplyContractsService
{
    protected readonly SupplierManagementServiceDbContext _context;

    public SupplyContractsServiceBase(SupplierManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one SupplyContract
    /// </summary>
    public async Task<SupplyContract> CreateSupplyContract(SupplyContractCreateInput createDto)
    {
        var supplyContract = new SupplyContractDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Details = createDto.Details,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            supplyContract.Id = createDto.Id;
        }
        if (createDto.Supplier != null)
        {
            supplyContract.Supplier = await _context
                .Suppliers.Where(supplier => createDto.Supplier.Id == supplier.Id)
                .FirstOrDefaultAsync();
        }

        _context.SupplyContracts.Add(supplyContract);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SupplyContractDbModel>(supplyContract.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one SupplyContract
    /// </summary>
    public async Task DeleteSupplyContract(SupplyContractWhereUniqueInput uniqueId)
    {
        var supplyContract = await _context.SupplyContracts.FindAsync(uniqueId.Id);
        if (supplyContract == null)
        {
            throw new NotFoundException();
        }

        _context.SupplyContracts.Remove(supplyContract);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SupplyContracts
    /// </summary>
    public async Task<List<SupplyContract>> SupplyContracts(SupplyContractFindManyArgs findManyArgs)
    {
        var supplyContracts = await _context
            .SupplyContracts.Include(x => x.Supplier)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return supplyContracts.ConvertAll(supplyContract => supplyContract.ToDto());
    }

    /// <summary>
    /// Meta data about SupplyContract records
    /// </summary>
    public async Task<MetadataDto> SupplyContractsMeta(SupplyContractFindManyArgs findManyArgs)
    {
        var count = await _context.SupplyContracts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one SupplyContract
    /// </summary>
    public async Task<SupplyContract> SupplyContract(SupplyContractWhereUniqueInput uniqueId)
    {
        var supplyContracts = await this.SupplyContracts(
            new SupplyContractFindManyArgs
            {
                Where = new SupplyContractWhereInput { Id = uniqueId.Id }
            }
        );
        var supplyContract = supplyContracts.FirstOrDefault();
        if (supplyContract == null)
        {
            throw new NotFoundException();
        }

        return supplyContract;
    }

    /// <summary>
    /// Update one SupplyContract
    /// </summary>
    public async Task UpdateSupplyContract(
        SupplyContractWhereUniqueInput uniqueId,
        SupplyContractUpdateInput updateDto
    )
    {
        var supplyContract = updateDto.ToModel(uniqueId);

        if (updateDto.Supplier != null)
        {
            supplyContract.Supplier = await _context
                .Suppliers.Where(supplier => updateDto.Supplier == supplier.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(supplyContract).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SupplyContracts.Any(e => e.Id == supplyContract.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Supplier record for SupplyContract
    /// </summary>
    public async Task<Supplier> GetSupplier(SupplyContractWhereUniqueInput uniqueId)
    {
        var supplyContract = await _context
            .SupplyContracts.Where(supplyContract => supplyContract.Id == uniqueId.Id)
            .Include(supplyContract => supplyContract.Supplier)
            .FirstOrDefaultAsync();
        if (supplyContract == null)
        {
            throw new NotFoundException();
        }
        return supplyContract.Supplier.ToDto();
    }
}
