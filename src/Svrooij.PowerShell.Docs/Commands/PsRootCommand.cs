using System.CommandLine;

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

    private static Option<string?> MamlOutput = new Option<string?>(
    "--maml-file",
    description: "Output location for the maml file (powershell docs format)"
);

    public static RootCommand GetRootCommand()
    {
        var rootCommand = new RootCommand("Generate documentation for a binary PowerShell module")
        {
            DllOption,
            XmlDocs,
            MdOutput,
            MamlOutput
        };

        rootCommand.SetHandler(Execute, DllOption, XmlDocs, MdOutput, MamlOutput);

        return rootCommand;
    }

    public static async Task Execute(string dllPath, bool useXmlDocs, string? mdOutput, string? mamlOutput)
    {
        try
        {


            var reflector = new DllReflector(dllPath);
            Console.WriteLine("Assemply loaded and parsed");

            if (useXmlDocs)
            {
                string xmlDocsPath = dllPath.Replace(".dll", ".xml");
                await reflector.EnhanceCommandsWithXmlDocs(xmlDocsPath);
                Console.WriteLine("Xml docs loaded and parsed");
            }

            if (mdOutput != null)
            {
                Console.WriteLine("Writing markdown output to: {0}", mdOutput);
                await reflector.WriteMarkdownOutput(mdOutput);
            }

            if (mamlOutput != null)
            {
                Console.WriteLine("Writing maml file to: {0}", mamlOutput);
                await reflector.WriteMamlFile(mamlOutput);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred: {0}", ex.Message);
            Environment.Exit(1);
        }
        finally
        {
            Console.WriteLine("Done.");
        }
    }
}
