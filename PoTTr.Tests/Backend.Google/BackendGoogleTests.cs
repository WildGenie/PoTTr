using NUnit.Framework;
using System.Text.Json;
using System.Text.Json.Serialization;
using PoTTr.Backend.Google;
using Google.Cloud.Speech.V1;

namespace Tests
{
    public class BackendGoogleTests
    {
        string s;
        [SetUp]
        public void Setup()
        {
            var a = new GoogleProviderConfig()
            {
                GoogleApiKey = "TEST",
                RecognitionConfig = new RecognitionConfig
                {
                    AudioChannelCount = 1,
                    DiarizationConfig = new SpeakerDiarizationConfig
                    {
                        EnableSpeakerDiarization = true,
                        MaxSpeakerCount = 2,
                        MinSpeakerCount = 2,
                        SpeakerTag = 1
                    },
                    EnableAutomaticPunctuation = true,
                    EnableSeparateRecognitionPerChannel = true,
                    EnableWordTimeOffsets = true,
                    LanguageCode = "en-us",
                    MaxAlternatives = 4,
                    Metadata = new RecognitionMetadata(),
                    Model = "",
                    ProfanityFilter = false,
                    SampleRateHertz = 43000,
                    UseEnhanced = true
                }
            };
            var sc = new SpeechContext();
            sc.Phrases.Add("c");
            sc.Phrases.Add("d");
            a.RecognitionConfig.SpeechContexts.Add(sc);
            s = JsonSerializer.Serialize(a);
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual("", s);
        }
        [Test]
        public void Test2()
        {
            var ds = JsonSerializer.Deserialize<GoogleProviderConfig>(s);
            Assert.AreEqual(null, ds);
        }
    }
}