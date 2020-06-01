using Google.Cloud.Speech.V1;
using PoTTr.Backend.Interfaces;

namespace PoTTr.Backend.Google
{
    public class GoogleProviderConfig : IBackendConfig
    {
        public string? JsonCredentialPath { get; set; }
        public RecognitionConfig? RecognitionConfig { get; set; }
        public static GoogleProviderConfig Default
        {
            get => new GoogleProviderConfig
            {
                RecognitionConfig = new RecognitionConfig
                {
                    SpeechContexts = { new SpeechContext { Phrases = { "A", "B" } } }
                }
            };
        }
    }
}