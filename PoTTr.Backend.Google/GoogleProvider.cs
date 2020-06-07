using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using Google.Cloud.Speech.V1P1Beta1;
using System.Text.Json;

namespace PoTTr.Backend.Google
{
    public class GoogleProvider : IProvider<GoogleProviderConfig>
    {
        public Command ConfigurationCommand => GoogleProviderConfig.Command;

        public void Handle(GoogleProviderConfig config)
        {
            throw new NotImplementedException();
        }
    }
}