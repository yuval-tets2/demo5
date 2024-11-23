using SupplierManagementService.APIs.Common;
using SupplierManagementService.APIs.Dtos;

namespace SupplierManagementService.APIs;

public interface ISuppliersService
{
    /// <summary>
    /// Create one Supplier
    /// </summary>
    public Task<Supplier> CreateSupplier(SupplierCreateInput supplier);

    /// <summary>
    /// Delete one Supplier
    /// </summary>
    public Task DeleteSupplier(SupplierWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Suppliers
    /// </summary>
    public Task<List<Supplier>> Suppliers(SupplierFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Supplier records
    /// </summary>
    public Task<MetadataDto> SuppliersMeta(SupplierFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Supplier
    /// </summary>
    public Task<Supplier> Supplier(SupplierWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Supplier
    /// </summary>
    public Task UpdateSupplier(SupplierWhereUniqueInput uniqueId, SupplierUpdateInput updateDto);

    /// <summary>
    /// Connect multiple PurchaseOrders records to Supplier
    /// </summary>
    public Task ConnectPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    );

    /// <summary>
    /// Disconnect multiple PurchaseOrders records from Supplier
    /// </summary>
    public Task DisconnectPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    );

    /// <summary>
    /// Find multiple PurchaseOrders records for Supplier
    /// </summary>
    public Task<List<PurchaseOrder>> FindPurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderFindManyArgs PurchaseOrderFindManyArgs
    );

    /// <summary>
    /// Update multiple PurchaseOrders records for Supplier
    /// </summary>
    public Task UpdatePurchaseOrders(
        SupplierWhereUniqueInput uniqueId,
        PurchaseOrderWhereUniqueInput[] purchaseOrdersId
    );

    /// <summary>
    /// Connect multiple SupplyContracts records to Supplier
    /// </summary>
    public Task ConnectSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] supplyContractsId
    );

    /// <summary>
    /// Disconnect multiple SupplyContracts records from Supplier
    /// </summary>
    public Task DisconnectSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] supplyContractsId
    );

    /// <summary>
    /// Find multiple SupplyContracts records for Supplier
    /// </summary>
    public Task<List<SupplyContract>> FindSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractFindManyArgs SupplyContractFindManyArgs
    );

    /// <summary>
    /// Update multiple SupplyContracts records for Supplier
    /// </summary>
    public Task UpdateSupplyContracts(
        SupplierWhereUniqueInput uniqueId,
        SupplyContractWhereUniqueInput[] supplyContractsId
    );
}
