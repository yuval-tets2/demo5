using Microsoft.AspNetCore.Mvc;
using OrderProcessingService.APIs.Common;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PaymentFindManyArgs : FindManyInput<Payment, PaymentWhereInput> { }
