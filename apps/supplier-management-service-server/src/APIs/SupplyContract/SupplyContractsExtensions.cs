using SupplierManagementService.APIs.Dtos;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs.Extensions;

public static class SupplyContractsExtensions
{
    public static SupplyContract ToDto(this SupplyContractDbModel model)
    {
        return new SupplyContract
        {
            CreatedAt = model.CreatedAt,
            Details = model.Details,
            Id = model.Id,
            Supplier = model.SupplierId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SupplyContractDbModel ToModel(
        this SupplyContractUpdateInput updateDto,
        SupplyContractWhereUniqueInput uniqueId
    )
    {
        var supplyContract = new SupplyContractDbModel
        {
            Id = uniqueId.Id,
            Details = updateDto.Details
        };

        if (updateDto.CreatedAt != null)
        {
            supplyContract.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Supplier != null)
        {
            supplyContract.SupplierId = updateDto.Supplier;
        }
        if (updateDto.UpdatedAt != null)
        {
            supplyContract.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return supplyContract;
    }
}
