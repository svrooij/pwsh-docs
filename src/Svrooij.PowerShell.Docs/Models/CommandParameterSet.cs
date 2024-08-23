using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Models;

public class CommandParameterSet
{
    public string? Name { get; set; }
    public List<CommandParameter>? Parameters { get; set; }
    public string? Description { get; set; }
    public bool IsDefault { get; set; }
}
