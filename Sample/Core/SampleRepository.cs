namespace Sample.Core;

public class SampleRepository: ISampleRepository
{
    
}

public interface ISampleRepository
{
    public async Task<SampleEntity> GetSampleEntityAsync(Guid uuid)
    {
        return await Task.FromResult(new SampleEntity());
    }
}