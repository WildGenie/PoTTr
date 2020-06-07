using System.CommandLine;
using System.IO;
using Google.Cloud.Speech.V1P1Beta1;

namespace PoTTr.Backend.Google
{
    public class GoogleProviderConfig : IProviderConfig
    {
        public FileInfo? JsonCredentialPath { get; set; } 
        public FileInfo? RecognitionConfigPath { get; set; }
        public FileInfo? FFmpegExePath {get;set;}
        
        public static Command Command => new Command("google")
            {
                new Option<FileInfo>(
                    "--json-credential-path",
                    "Path to the Google API JSON Credential File"){
                        Required = true
                    },
                new Option<FileInfo>(
                    "--recongnition-config-path",
                    "Path to a JSON file with the Recognition Configuration"),
                new Option<FileInfo>(
                    "--ffmpeg-exe-path",
                    "Path to the ffmpeg executable. (Required if ffmpeg is not one the path)"),
                };

        public FileInfo? AudioPath { get ; set; }
    }
}