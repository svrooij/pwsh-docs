using Svrooij.PowerShell.Docs.Models;
using System.Xml;
using System.Xml.Serialization;

namespace Svrooij.PowerShell.Docs
{
    internal class DllReflector
    {
        private readonly List<Command> commands = new List<Command>();
        public DllReflector(string dllPath)
        {
            LoadCommandsFromDllPath(dllPath);

        }

        public List<Command> GetCommands() => commands;

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

                var order = cmdletTypeDocumentation?.SelectSingleNode("psOrder");
                if (order != null)
                {
                    command.Order = int.TryParse(order.InnerText, out int result) ? result : null;
                }

                /// <parameterSet>
                /// <para type="name">Interactive</para>
                /// <para type="description">If you're running WinTuner on your local machine, you can use the interactive browser login. This will integrate with the native browser based login screen on Windows and with the default browser on other platforms.</para>
                /// </parameterSet>
                /// <parameterSet>
                /// <para type="name">UseManagedIdentity</para>
                /// <para type="description">WinTuner supports Managed Identity authentication, this is the recommended way if you run WinTuner in the Azure Environment.</para>
                /// </parameterSet>
                var parameterSets = cmdletTypeDocumentation?.SelectNodes("parameterSet");
                if (parameterSets != null)
                {
                    command.ParameterSets = new List<CommandParameterSet>();
                    for (int i = 0; i < parameterSets.Count; i++)
                    {
                        var parameterSet = parameterSets[i];
                        var name = parameterSet?.SelectSingleNode("para[@type='name']");
                        var description = parameterSet?.SelectSingleNode("para[@type='description']");
                        command.ParameterSets.Add(new CommandParameterSet
                        {
                            Name = name?.InnerText,
                            Description = description?.InnerText.Replace("\\r", "\r").Replace("\\n", "\n")
                        });
                    }
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
                /// <para type="name">Name of the sample</para>
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
                        var name = example?.SelectSingleNode("para[@type='name']");
                        var description = example?.SelectSingleNode("para[@type='description']");
                        var code = example?.SelectSingleNode("code");
                        command.Examples[i] = new CommandExample
                        {
                            Name = name?.InnerText,
                            Description = description?.InnerText.Replace("\\r", "\r").Replace("\\n", "\n"),
                            Code = code?.InnerText.Replace("\\r", "\r").Replace("\\n", "\n")
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

        public async Task WriteMarkdownOutput(string outputFolder)
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            foreach (var command in commands)
            {
                var outputFilePath = Path.Combine(outputFolder, $"{command.Name}.md");
                var template = new Templates.CommandMarkdown(command);
                await File.WriteAllTextAsync(outputFilePath, template.TransformText());
            }
        }

        public async Task WriteMamlFile(string outputFile)
        {
            var directory = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(directory!))
            {
                Directory.CreateDirectory(directory!);
            }

            var document = new Maml.helpItems();
            document.command = new Maml.command[commands.Count];
            var index = 0;
            foreach (var command in commands)
            {
                var helpItem = new Maml.command();
                helpItem.details = new Maml.commandDetails
                {
                    name = command.Name,
                    verb = command.Verb,
                    noun = command.Noun,
                    description = new Maml.description
                    {
                        para = command.Synopsis ?? command.Description ?? ""
                    }
                };
                helpItem.description = new Maml.description()
                {
                    para = command.Description ?? ""
                };

                var parameterSets = command.GetParameterSets();
                if (parameterSets?.Any() == true)
                {
                    helpItem.syntax = new Maml.commandSyntaxItem[parameterSets.Count()];
                    for(int setIndex = 0; setIndex < parameterSets.Count(); setIndex++)
                    
                    {
                        var parameterSet = parameterSets.ElementAt(setIndex);
                        var syntaxItem = new Maml.commandSyntaxItem();
                        syntaxItem.name = parameterSet.Name ?? command.Name;
                        syntaxItem.parameter = new Maml.commandSyntaxItemParameter[parameterSet.Parameters!.Count()];
                        var parameterSetParameters = parameterSet.Parameters!.ToList();
                        for (int i = 0; i < parameterSet.Parameters!.Count(); i++)
                        {
                            var parameter = parameterSetParameters!.ElementAt(i);
                            syntaxItem.parameter[i] = new Maml.commandSyntaxItemParameter
                            {
                                required = parameter.Mandatory,
                                variableLength = true,
                                name = parameter.Name,
                                position = parameter.Position?.ToString() ?? "0",
                                aliases = "none",
                                pipelineInput = "false",
                                description = new Maml.description
                                {
                                    para = parameter.Description ?? ""
                                },
                                type = new Maml.type
                                {
                                    name = parameter.Type
                                }
                            };
                        }
                        helpItem.syntax[setIndex] = syntaxItem;
                    }
                }

                var parameters = command.Parameters.DistinctBy(p => p.Name);
                if (parameters.Any())
                {
                    helpItem.parameters = new Maml.commandParameter[parameters.Count()];
                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        var parameter = parameters.ElementAt(i);
                        helpItem.parameters[i] = new Maml.commandParameter
                        {
                            required = parameter.Mandatory,
                            variableLength = true,
                            name = parameter.Name,
                            description = new Maml.description
                            {
                                para = parameter.Description ?? ""
                            },
                            type = new Maml.type
                            {
                                name = parameter.Type
                            }
                        };
                    }
                }

                // Add input/output types
                if (command.OutputType is not null)
                {
                    helpItem.returnValues = new Maml.commandReturnValues
                    {
                        returnValue = new Maml.commandReturnValuesReturnValue
                        {
                            description = new Maml.description
                            {
                                para = command.OutputType
                            },
                            type = new Maml.type
                            {
                                name = command.OutputType
                            }
                        }
                    };
                }

                // Add examples to helpItem
                if (command.Examples?.Any() == true)
                {
                    helpItem.examples = new Maml.commandExample[command.Examples.Count()];
                    for (int i = 0; i < command.Examples.Count(); i++)
                    {
                        var example = command.Examples.ElementAt(i);
                        helpItem.examples[i] = new Maml.commandExample
                        {
                            title = example.Name ?? $"-------- Example {i + 1} ---------",
                            remarks = new Maml.remarks
                            {
                                para = example.Description ?? ""
                            },
                            code = $"PS C:\\&gt; {example.Code}"
                        };
                    }
                }

                if (command.HelpUri != null)
                {
                    helpItem.relatedLinks = new Maml.commandRelatedLinks
                    {
                        navigationLink = new Maml.navigationLink
                        {
                            linkText = "Online Version",
                            uri = command.HelpUri
                        }
                    };
                }

                document.command[index] = helpItem;
                index++;
            }

            var serializer = new XmlSerializer(typeof(Maml.helpItems));
            var writer = new StreamWriter(outputFile);
            serializer.Serialize(writer, document);
            await writer.FlushAsync();
            await writer.DisposeAsync();

        }
    }
}
