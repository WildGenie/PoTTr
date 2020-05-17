using System;
using System.IO;
using System.Text.Json;

namespace PoTTr.Backend.Interfaces
{
    public interface IBackendProvider
    {
        static BackendMetadata BackendMetadata { get; }

        void Configure(JsonDocument options);

        IBackendResult Process(string fileName);
        IBackendResult Process(Stream audoStream);
    }
}
