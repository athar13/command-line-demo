<a id="readme-top"></a>
# CommandLine Demo App

[![.NET 8.0][.NET 8.0]][dotnet-url]

## Overview

I wasnted to create a command line application and wanted to implement command line argument. I had options which were third party, but I wanted to use something reliable. In my search I stubled upon Microsoft's `System.Commandline`. Unfortunately there are two drawbacks using this library
1. It is in preview and no stable release is available.
2. Because it is in preview, there is a limited documentation available.

So I tried it out and create this simple command line application. Hope it helps you getting started with it.

## Getting Started
This sample application implements the `System.Commandline` library in two ways:
- Basic straight forward implementation
- A bit complex implementation

Both implementations are in the same solution and does the same thing. The only difference is the way the options and commands are implemented.

### Installation
```dotnetcli
dotnet restore
```

### Projects

#### CommandlineCore
This is a sample application that acts as the main application. It read a give csv file or files, transforms it into an object, serializes it as json and writes it out to a specified file.

#### CommandLineDemo
This is a simple implementation of `System.CommandLine`. All Options and Commands are implemented in the `Program.cs` file itself. This is useful for a small application.

#### CommandLineAdvancedDemo
This project has a bit more complex implementation of `System.CommandLine`. It has separate classes for Options and Commands. This is useful for an application that has a lot of options and commands.

### Usage
- Running the application without any arguments will show the help message.
```dotnetcli
commandlinedemo
```

- For help use the '-?', `-h` or `--help` option.
```dotnetcli
commandlinedemo -?
```

- For help on subcommands use the help option with the subcommand.
```dotnetcli
comamandlinedemo file -?
```

You can read more about the `System.CommandLine` library [here](https://learn.microsoft.com/en-us/dotnet/standard/commandline/)

[.NET 8.0]: https://img.shields.io/badge/.NET-8.0-blue
[dotnet-url]: https://dotnet.microsoft.com/download/dotnet/8.0

## License
Distributed under the MIT License. See `LICENSE` for more information.
