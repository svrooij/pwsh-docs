using System.Xml.Serialization;

namespace Svrooij.PowerShell.Docs.Maml;

internal static class MamlGenerator
{
    internal static async Task GenerateMamlFile(string outputFile, List<Models.Command> commands)
    {
        var document = MamlGenerator.FromCommands(commands);

        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");
        var serializer = new XmlSerializer(typeof(helpItems));
        var writer = new StreamWriter(outputFile);
        serializer.Serialize(writer, document, ns);
        await writer.FlushAsync();
        await writer.DisposeAsync();
    }

    private static helpItems FromCommands(List<Models.Command> commands)
    {
        var document = new helpItems();
        document.schema = "maml";
        document.command = new command[commands.Count];
        var index = 0;
        foreach (var command in commands)
        {
            var helpItem = new command();
            helpItem.details = new commandDetails
            {
                name = command.Name,
                verb = command.Verb,
                noun = command.Noun,
                description = new description
                {
                    para = command.Synopsis ?? command.Description ?? ""
                }
            };
            helpItem.description = new description()
            {
                para = command.Description ?? ""
            };

            helpItem.syntax = command.GetParameterSets()?.Select(ps => MamlGenerator.FromParameterSet(ps, command.Name)).ToArray();

            helpItem.parameters = command.Parameters.DistinctBy(p => p.Name).Select(MamlGenerator.FromParameter).ToArray();

            // Add input/output types
            if (command.OutputType is not null)
            {
                helpItem.returnValues = new commandReturnValues
                {
                    returnValue = new commandReturnValuesReturnValue
                    {
                        description = new description
                        {
                            para = command.OutputType
                        },
                        type = new type
                        {
                            name = command.OutputType
                        }
                    }
                };
            }

            helpItem.examples = command.Examples?.Select(e => MamlGenerator.FromExample(e)).ToArray();

            if (command.HelpUri != null)
            {
                helpItem.relatedLinks = new commandRelatedLinks
                {
                    navigationLink = new navigationLink
                    {
                        linkText = "Online Version",
                        uri = command.HelpUri
                    }
                };
            }

            document.command[index] = helpItem;
            index++;
        }
        return document;
    }

    private static commandSyntaxItem FromParameterSet(Models.CommandParameterSet parameterSet, string commandName)
    {
        var mamlSyntaxItem = new commandSyntaxItem
        {
            name = commandName,
            parameter = parameterSet.Parameters!.Select(FromParameter).ToArray()
        };

        return mamlSyntaxItem;
    }

    private static commandParameter FromParameter(Models.CommandParameter parameter)
    {
        var mamlParameter = new commandParameter
        {
            required = parameter.Mandatory,
            variableLength = true,
            name = parameter.Name,
            position = parameter.Position?.ToString() ?? "0",
            aliases = "none",
            pipelineInput = parameter.PipelineInput,
            globbing = false,
            parameterValue = new commandParameterParameterValue
            {
                required = parameter.Mandatory,
                variableLength = parameter.Type.EndsWith("[]"),
                Value = parameter.Type
            },
            description = new description
            {
                para = parameter.Description ?? ""
            },
            type = new type
            {
                name = parameter.Type
            },
            defaultValue = parameter.Type == "SwitchParameter" ? "False" : "None",
        };

        mamlParameter.parameterValueGroup = parameter.EnumValues?.Select(v => new commandParameterParameterValue { required = false, variableLength = false, Value = v }).ToArray();


        return mamlParameter;
    }

    internal static commandExample FromExample(Models.CommandExample example)
    {
        // make the title the name (or example if no name is set), with dashes in front and after making a total lenght of 80 characters
        var title = $"  {example.Name ?? "Example"}  ";
        if (title.Length < 60)
        {
            title = title.PadLeft((80 - title.Length) / 2 + title.Length, '-').PadRight(80, '-');
        }

        var mamlExample = new commandExample
        {
            title = title,
            remarks = new remarks
            {
                para = $"{example.Description}\r\n"
            },
            code = $"PS C:\\> {example.Code}"
        };
        return mamlExample;
    }
}