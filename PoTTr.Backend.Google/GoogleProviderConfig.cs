using Google.Cloud.Speech.V1;

namespace PoTTr.Backend.Google
{
    public class GoogleProviderConfig
    {
        public string? GoogleApiKey { get; set; }
        public RecognitionConfig? RecognitionConfig{get;set;}
    }
}