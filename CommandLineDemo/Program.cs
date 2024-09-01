using System.CommandLine;
using CommandLineDemoCore;

Console.WriteLine();

var inputFileOption = new Option<IEnumerable<FileInfo>>(
    name: "--input",
    description: "Input file(s) to process");
inputFileOption.AddAlias("-i");
inputFileOption.AddValidator(result =>
{
    var files = result.GetValueForOption(inputFileOption);
    if (files?.Count() == 0)
    {
        result.ErrorMessage = "At least one file is required!";
    }
});
inputFileOption.IsRequired = true;

var inputFolderOption = new Option<DirectoryInfo>(
    name: "--input",
    description: "Directory with csv files");
inputFolderOption.AddAlias("-i");
inputFolderOption.IsRequired = true;

var outputFolderOption = new Option<DirectoryInfo>(
    name: "--directory",
    description: "Save output file in this directory",
    getDefaultValue: () => new DirectoryInfo(Directory.GetCurrentDirectory()));
outputFolderOption.AddAlias("-d");

var outputFileOption = new Option<string>(
    name: "--file",
    description: "File to write out to in JSON format",
    getDefaultValue: () => "people.json");
outputFileOption.AddAlias("-f");

var rootCommand = new RootCommand("Demo app for Command line");

var readFileCommand = new Command("file", "Read and process files")
{
    inputFileOption,
    outputFileOption,
    outputFolderOption
};

var readFolderCommand = new Command("folder", "Read and process all files in a directory")
{
    inputFolderOption,
    outputFileOption,
    outputFolderOption
};

rootCommand.AddCommand(readFolderCommand);
rootCommand.AddCommand(readFileCommand);

readFileCommand.SetHandler((inputFiles, outputFile, outputFolder) =>
{
    ProcessFiles(inputFiles.ToList().AsReadOnly(), outputFile, outputFolder);
},
inputFileOption, outputFileOption, outputFolderOption);

readFolderCommand.SetHandler(ProcessFolder, inputFolderOption, outputFileOption, outputFolderOption);

return rootCommand.Invoke(args);

void ProcessFiles(IReadOnlyList<FileInfo> inputFiles, string outputFile, DirectoryInfo outputFolder)
{
    Console.WriteLine($"Reading {inputFiles.Count} {(inputFiles.Count == 1 ? "file" : "files")}");
    Console.WriteLine($"Output Directory {outputFolder.Name}");
    Console.WriteLine($"Output file {outputFile}");

    if (!outputFolder.Exists)
    {
        outputFolder.Create();
    }

    var targetFile = Path.Combine(outputFolder.FullName, outputFile);

    var processor = new FileProcessor();

    foreach(var file in inputFiles)
    {
        processor.ProcessCsvFileToJsonFile(file.FullName, targetFile);
    }

    Console.WriteLine("Processing all files complete!");
};

void ProcessFolder(DirectoryInfo inputFolder, string outputFile, DirectoryInfo outputFolder)
{
    Console.WriteLine($"Reading Directory {inputFolder.Name}");
    Console.WriteLine($"Output Directory {outputFolder.Name}");
    Console.WriteLine($"Output file {outputFile}");

    if (!outputFolder.Exists)
    {
        outputFolder.Create();
    }

    var targetFile = Path.Combine(outputFolder.FullName, outputFile);

    var processor = new FileProcessor();

    foreach (var file in inputFolder.GetFiles("*.csv", SearchOption.TopDirectoryOnly))
    {
        processor.ProcessCsvFileToJsonFile(file.FullName, targetFile);
    }

    Console.WriteLine("Processing folder complete!");
}

