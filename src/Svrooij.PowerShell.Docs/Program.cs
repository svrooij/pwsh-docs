// See https://aka.ms/new-console-template for more information

using Svrooij.PowerShell.Docs.Commands;
using System.CommandLine;

var rootCommand = PsRootCommand.GetRootCommand();
await rootCommand.InvokeAsync(args);