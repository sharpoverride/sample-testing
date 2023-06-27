namespace Sample.Core;

public class SampleTransformationService: ISampleTransformationService
{
    public async Task<SampleEntity> TransformAsync(SampleEntity entity, SampleEntity sampleEntity)
    {
        return await Task.FromResult(new SampleEntity());
    }
    
}

public interface ISampleTransformationService
{
    Task<SampleEntity> TransformAsync(SampleEntity entity, SampleEntity sampleEntity);
}