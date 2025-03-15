using Soenneker.Domainr.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Domainr.Client.Tests;

[Collection("Collection")]
public class DomainrClientUtilTests : FixturedUnitTest
{
    private readonly IDomainrClientUtil _util;

    public DomainrClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IDomainrClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
