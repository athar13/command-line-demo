using System.CommandLine;

namespace CommandLineAdvancedDemo.Options;
public class InputFileOption : Option<IEnumerable<FileInfo>>
{
    private const string name = "--input";
    private const string alias = "-i";
    private const string description = "Input file(s) to process";

    public InputFileOption()
        : base(name, description)
    {
        AddAlias(alias);
        IsRequired = true;
        AddValidator(result =>
        {
            var files = result.GetValueForOption(this);
            if (files?.Count() == 0)
            {
                result.ErrorMessage = "At least one file is required!";
            }
        });
    }
}
