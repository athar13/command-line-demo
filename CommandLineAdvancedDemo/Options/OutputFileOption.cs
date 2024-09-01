using System.CommandLine;

namespace CommandLineAdvancedDemo.Options;
public class OutputFileOption : Option<string>
{
    private const string name = "--file";
    private const string alias = "-f";
    private const string description = "File to write out to in JSON format";

    public OutputFileOption()
        : base(name, description)
    {
        AddAlias(alias);
        SetDefaultValueFactory(() => "people.json");
    }
}
