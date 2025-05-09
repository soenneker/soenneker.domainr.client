using Microsoft.Extensions.Configuration;
using Soenneker.Domainr.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;

namespace Soenneker.Domainr.Client;

/// <inheritdoc cref="IDomainrClientUtil"/>
public sealed class DomainrClientUtil : IDomainrClientUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _configuration;

    private const string _clientId = nameof(DomainrClientUtil);

    public DomainrClientUtil(IHttpClientCache httpClientCache, IConfiguration configuration)
    {
        _httpClientCache = httpClientCache;
        _configuration = configuration;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, () =>
        {
            var host = _configuration.GetValueStrict<string>("Domainr:Host");
            var apiKey = _configuration.GetValueStrict<string>("Domainr:ApiKey");

            return new HttpClientOptions
            {
                BaseAddress = $"https://{host}/v2/",
                DefaultRequestHeaders = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", host }
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return _httpClientCache.Remove(_clientId);
    }
}