using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;

namespace SupplierManagementService.APIs;

public interface ISupplyContractsService
{
    /// <summary>
    /// Create one SupplyContract
    /// </summary>
    public Task<SupplyContract> CreateSupplyContract(SupplyContractCreateInput supplycontract);

    /// <summary>
    /// Delete one SupplyContract
    /// </summary>
    public Task DeleteSupplyContract(SupplyContractWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SupplyContracts
    /// </summary>
    public Task<List<SupplyContract>> SupplyContracts(SupplyContractFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about SupplyContract records
    /// </summary>
    public Task<MetadataDto> SupplyContractsMeta(SupplyContractFindManyArgs findManyArgs);

    /// <summary>
    /// Get one SupplyContract
    /// </summary>
    public Task<SupplyContract> SupplyContract(SupplyContractWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one SupplyContract
    /// </summary>
    public Task UpdateSupplyContract(
        SupplyContractWhereUniqueInput uniqueId,
        SupplyContractUpdateInput updateDto
    );

    /// <summary>
    /// Get a Supplier record for SupplyContract
    /// </summary>
    public Task<Supplier> GetSupplier(SupplyContractWhereUniqueInput uniqueId);
}
