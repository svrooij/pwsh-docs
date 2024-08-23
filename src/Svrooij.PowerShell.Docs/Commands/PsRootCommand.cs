using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Commands;

internal class PsRootCommand
{
    private static Option<string> DllOption = new Option<string>(
        "--dll",
        description: "The path to the dll to generate documentation for"
    );

    private static Option<bool> XmlDocs = new Option<bool>(
        "--use-xml-docs",
        description: "Should we try to load the xml docs, (you probably want to enable this)."
    );

    private static Option<string?> MdOutput = new Option<string?>(
        "--md-output",
        description: "The path where the markdown files will be written to."
    );

    public static RootCommand GetRootCommand()
    {
        var rootCommand = new RootCommand("Generate documentation for a binary PowerShell module")
        {
            DllOption,
            XmlDocs,
            MdOutput
        };

        rootCommand.SetHandler(Execute, DllOption, XmlDocs, MdOutput); 

        return rootCommand;
    }

    public static async Task Execute(string dllPath, bool useXmlDocs, string? mdOutput)
    {
        var reflector = new DllReflector(dllPath);
        Console.WriteLine("Assemply loaded and parsed");

        if (useXmlDocs)
        {
            string xmlDocsPath = dllPath.Replace(".dll", ".xml");
            await reflector.EnhanceDocsWithXmlDocs(xmlDocsPath);
            Console.WriteLine("Xml docs loaded and parsed");
        }

        

        if (mdOutput != null)
        {
            Console.WriteLine("Writing markdown output to: {0}", mdOutput);
            await reflector.WriteMarkdownOutput(mdOutput);
        }

        //Console.WriteLine();
        //Console.WriteLine("Commands found:");
        //foreach (var command in reflector.GetCommands())
        //{
        //    Console.WriteLine(" -> {0} {1}", command.Name, command.Synopsis);
        //    foreach (var parameter in command.Parameters)
        //    {
        //        Console.WriteLine("    -{0} ({1}) {2}", parameter.Name, parameter.Type, parameter.Description);
        //    }
        //}
    }
}
