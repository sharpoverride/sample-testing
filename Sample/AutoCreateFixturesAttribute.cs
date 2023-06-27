using System.Text.Json;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Sample.Core;

namespace Sample;


public class AutoCreateFixturesAttribute : AutoDataAttribute
{
    public AutoCreateFixturesAttribute()
        : base(() => new Fixture()
                     .Customize(new AutoMoqCustomization())
                     .Customize(new SampleEntityCustomization())
        )
    {
    }
}

public class SampleEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<SampleEntity>(
            c =>
                c.With(
                    p => p.Details,
                    () => JsonSerializer.SerializeToDocument("{\"anything\": \"goes\"}\"")));
    }
}