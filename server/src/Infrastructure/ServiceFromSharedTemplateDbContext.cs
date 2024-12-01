using Microsoft.EntityFrameworkCore;

namespace ServiceFromSharedTemplate.Infrastructure;

public class ServiceFromSharedTemplateDbContext : DbContext
{
    public ServiceFromSharedTemplateDbContext(
        DbContextOptions<ServiceFromSharedTemplateDbContext> options
    )
        : base(options) { }
}
