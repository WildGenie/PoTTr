using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoTTr.Backend.Interfaces
{
    public interface IBackendProvider<TConfig> where TConfig : IBackendConfig
    {
        BackendMetadata Metadata { get; }

        Task<IBackendResult> ProcessAsync(string fileName, TConfig options);
        Task<IBackendResult> ProcessAsync(Stream audoStream, TConfig options);
        IBackendResult Process(string fileName, TConfig options);
        IBackendResult Process(Stream audoStream, TConfig options);
    }

    public static class BackendProviderExtensionMethods
    {
        public static async Task<TConfig> ConfigFromJson<TConfig>(this IBackendProvider<TConfig> provider, Stream jsonConfig)
            where TConfig : IBackendConfig
            => await JsonSerializer.DeserializeAsync<TConfig>(jsonConfig);

        public static TConfig ConfigFromJson<TConfig>(this IBackendProvider<TConfig> provider, string jsonConfig)
            where TConfig : IBackendConfig
            => JsonSerializer.Deserialize<TConfig>(jsonConfig);
    }
}
