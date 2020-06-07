using System.CommandLine;
using System.IO;

namespace PoTTr.Backend.Google
{
    public interface IProviderConfig
    {

        FileInfo? AudioPath { get; set; }
    }
}