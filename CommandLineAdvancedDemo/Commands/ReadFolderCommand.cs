using System.CommandLine;
using CommandLineAdvancedDemo.Options;

namespace CommandLineAdvancedDemo.Commands;
public class ReadFolderCommand : Command
{
    private readonly InputFolderOption inputFolderOption = new();
    private readonly OutputFileOption outputFileOption = new();
    private readonly OutputFolderOption outputFolderOption = new();

    public ReadFolderCommand()
        : base("folder", "Read and process all files in a directory")
    {
        AddOption(inputFolderOption);
        AddOption(outputFileOption);
        AddOption(outputFolderOption);

        var helper = new FileProcessorHelper();

        this.SetHandler(helper.ProcessFolder, inputFolderOption, outputFileOption, outputFolderOption);
    }

    
}
