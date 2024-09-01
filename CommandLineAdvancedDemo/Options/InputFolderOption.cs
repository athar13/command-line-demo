using System.CommandLine;

namespace CommandLineAdvancedDemo.Options;
public class InputFolderOption : Option<DirectoryInfo>
{
    private const string name = "--input";
    private const string alias = "-i";
    private const string description = "Directory with csv files";

    public InputFolderOption()
        : base(name, description)
    {
        AddAlias(alias);
        IsRequired = true;
    }
}
