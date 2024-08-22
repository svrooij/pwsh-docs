// See https://aka.ms/new-console-template for more information
using Svrooij.PowerShell.Docs;

Console.WriteLine("Let's generate some docs");

Console.WriteLine("Arguments: {0}", string.Join(", ", args));

string dllPath = args[0];
var reflector = new DllReflector(dllPath);

string xmlDocsPath = dllPath.Replace(".dll", ".xml");
await reflector.EnhanceDocsWithXmlDocs(xmlDocsPath);

Console.WriteLine();
Console.WriteLine("Commands found:");
foreach (var command in reflector.GetCommands())
{
    Console.WriteLine(" -> {0} {1}", command.Name, command.Synopsis);
    foreach (var parameter in command.Parameters)
    {
        Console.WriteLine("    -{0} ({1}) {2}", parameter.Name, parameter.Type, parameter.Description);
    }
}