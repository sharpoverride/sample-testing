using AutoFixture.Xunit2;
using Moq;
using Sample.Core;

namespace Sample;

public class SampleAggregatorTests
{
    [Theory, AutoCreateFixtures]
    public async Task GivenSomeThing_ThenSomeResult_WhenSomeCondition(
        [Frozen] Mock<ISampleTransformationService> sampleTransformationService,
        [Frozen] Mock<ISampleRepository> sampleRepository,
        [Frozen] Mock<ISampleClient> sampleClient,
        [Frozen] SampleEntity remoteSampleEntity,
        [Frozen] SampleEntity sampleEntity,
        SampleAggregator sut,
        Guid uuid
    )
    {
        remoteSampleEntity.Id = uuid;
        sampleEntity.Id = uuid;
        sampleTransformationService.Setup(
                                       x => x.TransformAsync(
                                           sampleEntity, remoteSampleEntity))
                                   .ReturnsAsync(sampleEntity);

        sampleRepository.Setup(x => x.GetSampleEntityAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(sampleEntity);
        sampleClient.Setup(x => x.GetSampleEntityAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(remoteSampleEntity);

        var result = await sut.AggregateAsync(uuid);
        
        Assert.Equal(sampleEntity, result);
    }
}