using CustomerService.APIs;

namespace CustomerService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IFeedbacksService, FeedbacksService>();
        services.AddScoped<IInteractionsService, InteractionsService>();
        services.AddScoped<ISupportTicketsService, SupportTicketsService>();
    }
}
