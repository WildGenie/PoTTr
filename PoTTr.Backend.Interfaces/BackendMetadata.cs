using System;
using System.Collections.Generic;
using System.Text;

namespace PoTTr.Backend.Interfaces
{
    public struct BackendMetadata
    {
        public readonly string BackendName { get; }
        public readonly string BackendVersion { get; }

        public BackendMetadata(string backendName, string backendVersion)
        {
            BackendName = backendName;
            BackendVersion = backendVersion;
        }
    }
}
