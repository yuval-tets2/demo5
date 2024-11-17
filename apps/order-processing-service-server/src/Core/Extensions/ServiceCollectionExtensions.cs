using OrderProcessingService.APIs;

namespace OrderProcessingService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IOrderItemsService, OrderItemsService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IShipmentsService, ShipmentsService>();
    }
}
