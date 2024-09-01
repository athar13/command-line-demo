using CommandLineDemoCore;

namespace CommandLineAdvancedDemo;
public class FileProcessorHelper
{
    public void ProcessFiles(IReadOnlyList<FileInfo> inputFiles, string outputFile, DirectoryInfo outputFolder)
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

        foreach (var file in inputFiles)
        {
            processor.ProcessCsvFileToJsonFile(file.FullName, targetFile);
        }

        Console.WriteLine("Processing all files complete!");
    }

    public void ProcessFolder(DirectoryInfo inputFolder, string outputFile, DirectoryInfo outputFolder)
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
}
