[![](https://img.shields.io/nuget/v/soenneker.domainr.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.domainr.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.domainr.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.domainr.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.domainr.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.domainr.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.domainr.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.domainr.client/actions/workflows/codeql.yml)

# Soenneker.Domainr.Client

Provides a cached `HttpClient` configured for Domainr through RapidAPI.

## Installation

```bash
dotnet add package Soenneker.Domainr.Client
```

## Configuration

```json
{
  "Domainr": {
    "Host": "domainr.p.rapidapi.com",
    "ApiKey": "your-rapidapi-key"
  }
}
```

The client builds its base address as `https://{Host}/v2/` and sends the configured values as `x-rapidapi-host` and `x-rapidapi-key`. Keep the API key in a secret provider. Treat `Host` as trusted configuration because changing it changes the destination that receives the key.

## Registration and use

```csharp
using Soenneker.Domainr.Client.Abstract;
using Soenneker.Domainr.Client.Registrars;

services.AddDomainrClientUtilAsSingleton();

public sealed class DomainSearch(IDomainrClientUtil clientProvider)
{
    public async Task<string> Search(string query, CancellationToken cancellationToken)
    {
        HttpClient client = await clientProvider.Get(cancellationToken);
        string escaped = Uri.EscapeDataString(query);

        using HttpResponseMessage response =
            await client.GetAsync($"search?query={escaped}", cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}
```

`Get` returns the cached client. Do not dispose the returned `HttpClient`; dispose response messages and response streams you create. The provider owns the cache entry.

Automatic redirects are disabled so the custom RapidAPI headers cannot be forwarded to another host. Treat a redirect response as an explicit trust decision instead of following its `Location` with the authenticated client.

Singleton registration is the normal choice for direct transport use. `AddDomainrClientUtilAsScoped()` scopes the provider but still uses the shared singleton HTTP-client cache; disposing that provider removes its named cache entry.

This package configures transport only. It does not encode query values, deserialize Domainr responses, check status codes, retry rate limits, or translate errors. Use `Soenneker.Domainr.Util` when you want the higher-level search and status operations.
