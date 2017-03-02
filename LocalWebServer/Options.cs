using CommandLine;
using CommandLine.Text;

namespace LocalWebServer
{
    class Options
    {
        [Option('r', "root", Required = true,
             HelpText = "Root physical path of the website")]
        public string Root { get; set; }

        [Option('p', "port", Required = true,
             HelpText = "port")]
        public int Port { get; set; }

        [Option('v', "verbose", Required = false,
             HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}