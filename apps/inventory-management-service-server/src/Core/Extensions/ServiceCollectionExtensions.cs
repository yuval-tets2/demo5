using InventoryManagementService.APIs;

namespace InventoryManagementService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IInventoriesService, InventoriesService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IStockMovementsService, StockMovementsService>();
        services.AddScoped<IWarehousesService, WarehousesService>();
    }
}
