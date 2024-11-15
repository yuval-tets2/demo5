using InventoryManagementService.APIs.Common;
using InventoryManagementService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class StockMovementFindManyArgs : FindManyInput<StockMovement, StockMovementWhereInput> { }
