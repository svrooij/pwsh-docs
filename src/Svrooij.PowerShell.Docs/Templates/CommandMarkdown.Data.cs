using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Templates;

partial class CommandMarkdown
{
    public Models.Command Command { get; }
    public CommandMarkdown(Models.Command command)
    {
        Command = command;
    }
}
