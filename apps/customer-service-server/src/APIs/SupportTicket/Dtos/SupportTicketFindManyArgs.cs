using CustomerService.APIs.Common;
using CustomerService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class SupportTicketFindManyArgs : FindManyInput<SupportTicket, SupportTicketWhereInput> { }
