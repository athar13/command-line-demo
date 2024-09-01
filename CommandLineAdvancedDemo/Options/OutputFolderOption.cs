using System.CommandLine;

namespace CommandLineAdvancedDemo.Options;
public class OutputFolderOption : Option<DirectoryInfo>
{
    private const string name = "--directory";
    private const string alias = "-d";
    private const string description = "Save output file in this directory";

    public OutputFolderOption() 
        : base(name, description)
    {
        AddAlias(alias);
        SetDefaultValueFactory(() => new DirectoryInfo(Directory.GetCurrentDirectory()));
    }
}
