using System.CommandLine;
using CommandLineAdvancedDemo.Options;

namespace CommandLineAdvancedDemo.Commands;
public class ReadFileCommand : Command
{
    private readonly InputFileOption inputFileOption = new();
    private readonly OutputFileOption outputFileOption = new();
    private readonly OutputFolderOption outputFolderOption = new();

    public ReadFileCommand()
        : base("file", "Read and process files")
    {
        AddOption(inputFileOption);
        AddOption(outputFolderOption);
        AddOption(outputFileOption);

        var helper = new FileProcessorHelper();

        this.SetHandler((inputFiles, outputFile, outputDirectory) => {
            helper.ProcessFiles(inputFiles.ToList().AsReadOnly(), outputFile, outputDirectory);
        }, inputFileOption, outputFileOption, outputFolderOption);
    }

    
}
