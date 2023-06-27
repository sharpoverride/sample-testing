using System.Text.Json;

namespace Sample.Core;

public class SampleEntity
{
    public JsonDocument Details { get; set; } = default!;
    public Guid Id { get; set; }
}