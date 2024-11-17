using Microsoft.EntityFrameworkCore;
using ServiceFromPrivateTemplate.Infrastructure.Models;

namespace ServiceFromPrivateTemplate.Infrastructure;

public class ServiceFromPrivateTemplateDbContext : DbContext
{
    public ServiceFromPrivateTemplateDbContext(
        DbContextOptions<ServiceFromPrivateTemplateDbContext> options
    )
        : base(options) { }

    public DbSet<FffDbModel> Fffs { get; set; }
}
