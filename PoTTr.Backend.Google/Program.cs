using System.Threading.Tasks;
using FFmpeg.NET;
using System;
using Google.Cloud.Speech.V1P1Beta1;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace PoTTr.Backend.Google
{
    /// <summary>
    ///  Main class that runs the command line interface
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audioPath"></param>
        static async Task<int> Main(string[] args)
        {
            var google = new GoogleProvider();
            var rootCommand = new RootCommand
            {
                new Option<FileInfo>("--audio-file"){
                    Required = true
                },
                CreateCommand(google)
             };

            rootCommand.Description = "My sample app";

            // Note that the parameters of the handler method are matched according to the names of the options
            // rootCommand.Handler = CommandHandler.Create<FileInfo, FileInfo>(RunProvider);

            // Parse the incoming args and invoke the handler
            return await rootCommand.InvokeAsync(args);
        }

        private static Command CreateCommand<T>(IProvider<T> provider) where T : IProviderConfig
        {
            Command c = provider.ConfigurationCommand;
            c.Handler = CommandHandler.Create<T>((c) => HandleExceptions(() =>
            {
                provider.Handle(c);
            }));
            return c;
        }
#if !DEBUG
        private static int HandleExceptions(Action a)
        {
            try
            {
                a();
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Error.WriteLine($"ERROR -- {ex?.Message}");
                return -1;
            }
        }
#elif DEBUG
        private static int HandleExceptions(Action a)
        {
            a();
            return 0;
           
        }
#endif
    }

    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the integer version of the sample rate
        /// </summary>
        /// <param name="md">Audio Metadata</param>
        /// <returns>integer version of the sample rate</returns>
        public static int SampleRateInt(this MetaData.Audio md)
        {
            return int.Parse(md.SampleRate.Substring(0,
             md.SampleRate.ToCharArray().TakeWhile(c => char.IsDigit(c)).Count()));
        }
    }
}