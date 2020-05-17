using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoTTr.Backend.Interfaces
{
    public interface IBackendProvider<TConfig>
    {
        BackendMetadata BackendMetadata { get; }

        void Configure(TConfig options);

        IBackendResult Process(string fileName);
        IBackendResult Process(Stream audoStream);
    }

    public static class BackendProviderExtensionMethods
    {
        public static async Task<TConfig> ConfigFromJson<TConfig>(this IBackendProvider<TConfig> provider, Stream jsonConfig)
            => await JsonSerializer.DeserializeAsync<TConfig>(jsonConfig);

        public static TConfig ConfigFromJson<TConfig>(this IBackendProvider<TConfig> provider, string jsonConfig)
            => JsonSerializer.Deserialize<TConfig>(jsonConfig);
    }
}
