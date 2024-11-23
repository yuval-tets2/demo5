using SupplierManagementService.APIs;

namespace SupplierManagementService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDeliveriesService, DeliveriesService>();
        services.AddScoped<IPurchaseOrdersService, PurchaseOrdersService>();
        services.AddScoped<ISuppliersService, SuppliersService>();
        services.AddScoped<ISupplyContractsService, SupplyContractsService>();
    }
}
