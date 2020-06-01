using System.Collections.Generic;
using System.IO;
using Google.Cloud.Speech.V1;
using PoTTr.Backend.Interfaces;

namespace PoTTr.Backend.Google
{
    public class GoogleProvider : IBackendProvider<GoogleProviderConfig>
    {
        public BackendMetadata Metadata { get; }
            = new BackendMetadata("Google Cloud Speech-to-Text",
                                  "0.1.0",
                                  new List<string> { "flac", "mulaw", "s16le", "amrnb", "amrwb", "opus" },
                                  "flac");

        public async IBackendResult Process(string fileName, GoogleProviderConfig options)
        {
            throw new System.NotImplementedException();
        }

        public async IBackendResult Process(Stream audioStream, GoogleProviderConfig options)
        {
            RecognitionAudio.FromStreamAsync(audioStream)
            throw new System.NotImplementedException();
        }
    }
}