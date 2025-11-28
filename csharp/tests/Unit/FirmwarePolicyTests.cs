using VendorConnector.Services;
using Xunit;

public class FirmwarePolicyTests
{
    [Fact]
    public void WindowAcrossMidnight()
    {
        Assert.True(FirmwarePolicy.CanUpdate("03:30", "22:00-04:00"));
        Assert.False(FirmwarePolicy.CanUpdate("21:59", "22:00-04:00"));
    }

    [Fact]
    public void SemverCompare()
    {
        Assert.True(FirmwarePolicy.SemverCmp("1.2.10","1.2.9") > 0);
        Assert.True(FirmwarePolicy.SemverCmp("1.2.0","1.2.0") == 0);
        Assert.True(FirmwarePolicy.SemverCmp("1.2.0","1.3.0") < 0);
    }
}
