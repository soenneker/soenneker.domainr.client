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
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
