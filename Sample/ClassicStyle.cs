using AutoFixture;
using Moq;
using Sample.Core;

namespace Sample;

public class ClassicStyle
{
    [Fact]
    public async Task SomeFacts()
    {
        var sampleRepository = new Mock<ISampleRepository>();
        var sampleClient = new Mock<ISampleClient>();
        var sampleTransformationService = new Mock<ISampleTransformationService>();

        var sut = new SampleAggregator(
            sampleClient.Object,
            sampleTransformationService.Object,
            sampleRepository.Object);
        
        
        var uuid = Guid.NewGuid();
        var remoteSampleEntity = new SampleEntity {Id = uuid};
        var sampleEntity = new SampleEntity {Id = uuid};

        sampleTransformationService.Setup(
                                       x => x.TransformAsync(
                                           sampleEntity, remoteSampleEntity))
                                   .ReturnsAsync(sampleEntity);

        sampleRepository.Setup(x => x.GetSampleEntityAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(remoteSampleEntity);

        sampleClient.Setup(x => x.GetSampleEntityAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(sampleEntity);

        await sut.AggregateAsync(
            uuid);

    }
}