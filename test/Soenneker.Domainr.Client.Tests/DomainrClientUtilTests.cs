using Soenneker.Domainr.Client.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Domainr.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class DomainrClientUtilTests : HostedUnitTest
{
    private readonly IDomainrClientUtil _util;

    public DomainrClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IDomainrClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
