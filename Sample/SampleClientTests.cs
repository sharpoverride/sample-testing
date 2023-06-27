using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Moq;
using Moq.Protected;
using Sample.Core;

namespace Sample;

public class SampleClientTests
{
    [AutoCreateFixtures]
    [Theory]
    public async Task WeCanEasilyTest(
        [Frozen] SampleEntity sampleEntity,
        [Frozen] Mock<HttpMessageHandler> mockHttpMessageHandler,
        Guid uuid
    )
    {
        SampleClient sut = new SampleClient(
            new HttpClient(
                mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost")
            });

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(sampleEntity)
            });

        await sut.GetSampleEntityAsync(uuid);
        
    }
}