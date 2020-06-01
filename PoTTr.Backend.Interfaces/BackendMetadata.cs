using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PoTTr.Backend.Interfaces
{
    public class BackendMetadata
    {
        public string BackendName { get; }
        public string BackendVersion { get; }

        public IEnumerable<string> SupportedFormats { get; }

        public string PreferredFormat { get;}

        public BackendMetadata(string backendName, string backendVersion, IEnumerable<string> supportedFormats, string preferredFormat)
        {
            BackendName = backendName;
            BackendVersion = backendVersion;
            SupportedFormats = supportedFormats;
            PreferredFormat = preferredFormat;
        }
    }
}
