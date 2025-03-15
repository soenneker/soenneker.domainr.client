using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Domainr.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Domainr.Client.Registrars;

/// <summary>
/// A .NET thread-safe singleton HttpClient for Domainr
/// </summary>
public static class DomainrClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IDomainrClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddDomainrClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton().TryAddSingleton<IDomainrClientUtil, DomainrClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IDomainrClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddDomainrClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton().TryAddScoped<IDomainrClientUtil, DomainrClientUtil>();

        return services;
    }
}