using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Domainr.Client.Abstract;

/// <summary>
/// A .NET thread-safe singleton HttpClient for Domainr
/// </summary>
public interface IDomainrClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured http Client used by the domainr client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested http Client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
