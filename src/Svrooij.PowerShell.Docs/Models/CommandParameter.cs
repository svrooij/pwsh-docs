using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Models;

public class CommandParameter
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Type { get; set; }
    public string? ParameterSetName { get; set; }
    public int? Position { get; set; }
    public bool Mandatory { get; set; }

    // ByValue
    public bool ValueFromPipeline { get; set; }
    // ByPropertyName
    public bool ValueFromPipelineByPropertyName { get; set; }
    // FromRemainingArguments
    public bool ValueFromRemainingArguments { get; set; }

    public string[]? EnumValues { get; set; }

    public string PipelineInput
    {
        get
        {
            if (!ValueFromPipeline && !ValueFromPipelineByPropertyName && !ValueFromRemainingArguments)
            {
                return "False";
            }
            var names = new List<string>();
            if (ValueFromPipelineByPropertyName)
            {
                names.Add("ByPropertyName");
            }
            if (ValueFromPipeline)
            {
                names.Add("ByValue");
            }
            if (ValueFromRemainingArguments)
            {
                names.Add("FromRemainingArguments");
            }
            return $"True ({string.Join(", ", names)})";
        }
    }

    public override string ToString()
    {
        return $"{Name} [{Type}]";
    }
}
