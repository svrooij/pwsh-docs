using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Models;

public class CommandParameter
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public string? ParameterSetName { get; set; }
    public int? Position { get; set; }
    public bool Mandatory { get; set; }

    public override string ToString()
    {
        return $"{Name} [{Type}]";
    }
}
