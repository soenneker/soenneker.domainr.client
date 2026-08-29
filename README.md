[![](https://img.shields.io/nuget/v/soenneker.domainr.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.domainr.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.domainr.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.domainr.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.domainr.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.domainr.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.domainr.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.domainr.client/actions/workflows/codeql.yml)

# Soenneker.Domainr.Client

A .NET thread-safe singleton HttpClient for Domainr.

## Install

```bash
dotnet add package Soenneker.Domainr.Client
```

## Quick start

```csharp
using Soenneker.Domainr.Client.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddDomainrClientUtilAsSingleton();
```

Adds `IDomainrClientUtil` as a singleton service.

## What you get

- `IDomainrClientUtil` — A .NET thread-safe singleton HttpClient for Domainr.
- `DomainrClientUtilRegistrar` — A .NET thread-safe singleton HttpClient for Domainr.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `DomainrClientUtilRegistrar.AddDomainrClientUtilAsSingleton(services)` | Adds `IDomainrClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `DomainrClientUtilRegistrar.AddDomainrClientUtilAsScoped(services)` | Adds `IDomainrClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
