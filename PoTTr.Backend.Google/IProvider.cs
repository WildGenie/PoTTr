using System.CommandLine;
using System.IO;

namespace PoTTr.Backend.Google
{
    public interface IProvider<Config> where Config : IProviderConfig
    {
        void Handle(Config config);

        Command ConfigurationCommand { get; }

    }
}