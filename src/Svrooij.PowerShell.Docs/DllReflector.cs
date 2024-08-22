using Svrooij.PowerShell.Docs.Models;
using System.Xml;

namespace Svrooij.PowerShell.Docs
{
    internal class DllReflector
    {
        private readonly List<Command> commands = new List<Command>();
        public DllReflector(string dllPath)
        {
            LoadCommandsFromDllPath(dllPath);

        }

        private void LoadCommandsFromDllPath(string dllPath)
        {
            // Load the assembly
            var assembly = System.Reflection.Assembly.LoadFrom(dllPath);

            // Get all types that are a subclass of Cmdlet
            var cmdletTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(System.Management.Automation.Cmdlet)) && t.IsPublic && !t.IsAbstract).ToList();

            // Loop over all cmdlets
            foreach (var cmdletType in cmdletTypes)
            {
                // Create a new command
                var command = new Models.Command(cmdletType!);

                // Add the command to the list
                commands.Add(command);
            }
        }

        public Task EnhanceDocsWithXmlDocs(string xmlDocsPath)
        {
            // Load the xml documentation
            var xmlDocs = new XmlDocument();
            xmlDocs.Load(xmlDocsPath);

            // Loop over all commands
            foreach (var command in commands)
            {
                // Get the xml documentation for the command
                var cmdletType = command.CmdletType;
                var cmdletTypeDocumentation = xmlDocs.SelectSingleNode($"/doc/members/member[@name='T:{cmdletType.FullName}']");

                // Set the synopsis
                var summary = cmdletTypeDocumentation?.SelectSingleNode("summary");
                /// <summary>
                /// <para type="synopsis">Connect to Intune</para>
                /// <para type="description">A separate command to select the correct authentication provider, you no longer have to provide the auth parameters with each command.</para>
                /// <para type="link" uri="https://wintuner.app/docs/wintuner-powershell/Connect-WtWinTuner">Documentation</para> 
                /// </summary>
                // Try if the summary contains a synopsis and description
                if (summary != null)
                {
                    var synopsis = summary.SelectSingleNode("para[@type='synopsis']");
                    var description = summary.SelectSingleNode("para[@type='description']");
                    if (synopsis != null)
                    {
                        command.Synopsis = synopsis.InnerText;
                    }
                    if (description != null)
                    {
                        command.Description = description.InnerText;
                    }

                    if (string.IsNullOrEmpty(command.Synopsis))
                    {
                        command.Synopsis = summary.InnerText;
                    }
                }

                if (string.IsNullOrEmpty(command.Description))
                {
                    command.Description = cmdletTypeDocumentation?.SelectSingleNode("remarks")?.InnerText;
                }

                /// <example>
                /// <para type="description">Connect using interactive authentication</para>
                /// <code>Connect-WtWinTuner -Username "youruser@contoso.com"</code>
                /// </example>
                /// <example>
                /// <para type="description">Connect using managed identity</para>
                /// <code>Connect-WtWinTuner -UseManagedIdentity</code>
                /// </example>
                /// <example>
                /// <para type="description">Connect using default credentials</para>
                /// <code>az login &amp; Connect-WtWinTuner -UseDefaultCredentials</code>
                /// </example>
                // cmdletTypeDocumentation may contain zero or more examples
                var examples = cmdletTypeDocumentation?.SelectNodes("example");
                if (examples != null)
                {
                    command.Examples = new CommandExample[examples.Count];
                    for (int i = 0; i < examples.Count; i++)
                    {
                        var example = examples[i];
                        var description = example?.SelectSingleNode("para[@type='description']");
                        var code = example?.SelectSingleNode("code");
                        command.Examples[i] = new CommandExample
                        {
                            Name = description?.InnerText,
                            Code = code?.InnerText
                        };
                    }
                }

                // Loop over all parameters
                foreach (var parameter in command.Parameters)
                {
                    // Get the xml documentation for the parameter
                    var parameterDocumentation = xmlDocs.SelectSingleNode($"/doc/members/member[@name='P:{cmdletType.FullName}.{parameter.Name}']");

                    // Set the description
                    parameter.Description ??= parameterDocumentation?.SelectSingleNode("summary")?.InnerText;
                }
            }

            return Task.CompletedTask;
        }

        public List<Command> GetCommands()
        {
            return commands;
        }
    }
}
