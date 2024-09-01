using System.CommandLine;
using CommandLineAdvancedDemo.Commands;

Console.WriteLine();

var rootCommand = new RootCommand("Demo app for Command line");

rootCommand.AddCommand(new ReadFileCommand());
rootCommand.AddCommand(new ReadFolderCommand());

rootCommand.Invoke(args);
