using CustomerService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure;

public class CustomerServiceDbContext : DbContext
{
    public CustomerServiceDbContext(DbContextOptions<CustomerServiceDbContext> options)
        : base(options) { }

    public DbSet<InteractionDbModel> Interactions { get; set; }

    public DbSet<SupportTicketDbModel> SupportTickets { get; set; }

    public DbSet<FeedbackDbModel> Feedbacks { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }
}
