using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureService(this IServiceCollection services) =>
        services.AddScoped<IBasketRepository, BasketRepository>()
        .AddTransient<ISerializeService, SerializeService>();
}
