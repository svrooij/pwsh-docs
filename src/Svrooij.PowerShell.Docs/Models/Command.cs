using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Models;

public class Command
{
    private const string DEFAULT_PARAMETER_SET_NAME = "__AllParameterSets";
    public Command(Type cmdletType)
    {
        CmdletType = cmdletType;
        var cmdLetAttribute = CmdletType.GetCustomAttribute<CmdletAttribute>();
        Verb = cmdLetAttribute!.VerbName;
        Noun = cmdLetAttribute.NounName;
        Name = $"{Verb}-{Noun}";
        HelpUri = string.IsNullOrEmpty(cmdLetAttribute.HelpUri) ? cmdLetAttribute.HelpUri : null;
        DefaultParameterSetName = cmdLetAttribute.DefaultParameterSetName == DEFAULT_PARAMETER_SET_NAME ? null : cmdLetAttribute.DefaultParameterSetName;
        LoadCommandInfo();
    }
    public Type CmdletType { get; set; }
    public string Name { get; set; }
    public string Verb { get; set; }
    public string Noun { get; set; }
    public string? HelpUri { get; set; }
    public string? Synopsis { get; set; }
    public string? Description { get; set; }
    public string? DefaultParameterSetName { get; set; }
    public string? OutputType { get; set; }
    public int? Order { get; set; }
    public List<Models.CommandParameterSet>? ParameterSets { get; set; }
    public List<Models.CommandParameter> Parameters { get; set; } = new List<Models.CommandParameter>();
    public CommandExample[]? Examples { get; set; }

    public override string ToString()
    {
        return Name;
    }

    private void LoadCommandInfo()
    {
        var parameters = CmdletType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var parameter in parameters)
        {
            var parameterAttributes = parameter.GetCustomAttributes<ParameterAttribute>();
            if (parameterAttributes != null)
            {
                foreach (var parameterAttribute in parameterAttributes)
                {
                    var commandParameter = new Models.CommandParameter
                    {
                        Description = parameterAttribute.HelpMessage,
                        Name = parameter.Name,
                        Type = parameter.PropertyType.IsGenericType ? parameter.PropertyType.GenericTypeArguments.FirstOrDefault()?.Name ?? parameter.PropertyType.Name : parameter.PropertyType.Name,
                        Mandatory = parameterAttribute.Mandatory,
                        Position = parameterAttribute.Position < 0 ? 0 : parameterAttribute.Position,
                        ParameterSetName = parameterAttribute.ParameterSetName == DEFAULT_PARAMETER_SET_NAME ? null : parameterAttribute.ParameterSetName,
                        ValueFromPipeline = parameterAttribute.ValueFromPipeline,
                        ValueFromPipelineByPropertyName = parameterAttribute.ValueFromPipelineByPropertyName,
                        ValueFromRemainingArguments = parameterAttribute.ValueFromRemainingArguments,
                        // if the parameter has an enum, we will add the string values to the enumValues
                        EnumValues = parameter.PropertyType.IsEnum ? Enum.GetNames(parameter.PropertyType) : null


                    };
                    Parameters.Add(commandParameter);
                }
            }
        }

        var outputTypeAttribute = CmdletType.GetCustomAttribute<OutputTypeAttribute>();
        if (outputTypeAttribute is not null)
        {
            OutputType = string.Join(", ", outputTypeAttribute.Type.Select(t => t.Name));
        }

    }

    public IEnumerable<CommandParameterSet> GetParameterSets()
    {
        var parametersWithoutSet = Parameters.Where(p => p.ParameterSetName == null).ToList();
        // If command.ParameterSetOrder is set, we will use that to order the parameter sets.
        if (ParameterSets != null && ParameterSets.Count > 0)
        {
            var orderedParameterSets = new List<CommandParameterSet>();
            foreach (var parameterSet in ParameterSets)
            {
                parameterSet.Parameters = Parameters
                    .Where(p => p.ParameterSetName == parameterSet.Name || string.IsNullOrEmpty(p.ParameterSetName))
                    // Sort order: Mandatory, Position (only positive values), Name 
                    .OrderByDescending(p => p.Mandatory ? 1 : 0)
                    .ThenByDescending(p => p.Position < 0 ? 0 : 1)
                    .ThenBy(p => p.Position)
                    .ThenBy(p => p.Name)
                    .ToList();
                parameterSet.IsDefault = parameterSet.Name == DefaultParameterSetName;
                orderedParameterSets.Add(parameterSet);
            }
            return orderedParameterSets;
        }

        var parameterSets = Parameters
            .Where(p => p.ParameterSetName != null)
            .GroupBy(p => p.ParameterSetName)
            .Select(g => new CommandParameterSet
            {
                Name = g.Key,
                Parameters = g.ToList(),
                IsDefault = g.Key == DefaultParameterSetName
            }).ToList();

        if (parametersWithoutSet.Any())
        {
            parameterSets.ForEach(set => set.Parameters!.AddRange(parametersWithoutSet));
            parameterSets.Add(new CommandParameterSet
            {
                Name = null,
                Parameters = parametersWithoutSet,
                IsDefault = DefaultParameterSetName is null
            });
        }

        return parameterSets.OrderByDescending(p => p.IsDefault).ThenBy(p => p.Name);
    }

}
