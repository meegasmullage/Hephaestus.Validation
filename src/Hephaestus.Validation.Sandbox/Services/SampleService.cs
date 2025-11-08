using System;

namespace Hephaestus.Validation.Sandbox.Services
{
    public class SampleService : ISampleService
    {
        public long GetTimestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}
