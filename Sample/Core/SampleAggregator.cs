namespace Sample.Core;

public class SampleAggregator
{
    private readonly ISampleClient _sampleClient;
    private readonly ISampleTransformationService _sampleTransformationService;
    private readonly ISampleRepository _sampleRepository;

    public SampleAggregator(
        ISampleClient sampleClient,
        ISampleTransformationService sampleTransformationService,
        ISampleRepository sampleRepository
        )
    {
        _sampleClient = sampleClient;
        _sampleTransformationService = sampleTransformationService;
        _sampleRepository = sampleRepository;
    }
    
    public async Task<SampleEntity> AggregateAsync(Guid uuid)
    {
        var remoteSampleEntity = await _sampleClient.GetSampleEntityAsync(uuid);
        var sampleEntity = await _sampleRepository.GetSampleEntityAsync(uuid);
        return await _sampleTransformationService.TransformAsync(sampleEntity, remoteSampleEntity);
    }
}