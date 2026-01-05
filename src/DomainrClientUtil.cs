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
    private readonly string _host;
    private readonly string _apiKey;

    private const string _clientId = nameof(DomainrClientUtil);

    public DomainrClientUtil(IHttpClientCache httpClientCache, IConfiguration configuration)
    {
        _httpClientCache = httpClientCache;
        _host = configuration.GetValueStrict<string>("Domainr:Host");
        _apiKey = configuration.GetValueStrict<string>("Domainr:ApiKey");
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, CreateHttpClientOptions, cancellationToken);
    }

    private HttpClientOptions? CreateHttpClientOptions()
    {
        return new HttpClientOptions
        {
            BaseAddress = $"https://{_host}/v2/",
            DefaultRequestHeaders = new System.Collections.Generic.Dictionary<string, string>
            {
                { "x-rapidapi-key", _apiKey },
                { "x-rapidapi-host", _host }
            }
        };
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}