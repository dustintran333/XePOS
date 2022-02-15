using XePOS.Application.Data;
using XePOS.Application.Services;

namespace XePos.Tests;

public class TestBase
{
    protected PointOfSaleTerminal Terminal { get; set; } = new PointOfSaleTerminal();

    public void TestSetup() => Terminal.SetPricing(PricingData.GetData());
}