using Microsoft.AspNetCore.Mvc;
using SupplierManagementService.APIs.Common;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class DeliveryFindManyArgs : FindManyInput<Delivery, DeliveryWhereInput> { }
