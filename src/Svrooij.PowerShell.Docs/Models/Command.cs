using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Models
{
    internal class Command
    {
        public Command(Type cmdletType)
        {
            CmdletType = cmdletType;
            var cmdLetAttribute = CmdletType.GetCustomAttribute<CmdletAttribute>();
            Name = cmdLetAttribute!.VerbName + "-" + cmdLetAttribute.NounName;
            LoadCommandInfo();
        }
        public Type CmdletType { get; set; }
        public string Name { get; set; }
        public string? Synopsis { get; set; }
        public string? Description { get; set; }
        public List<Models.CommandParameter> Parameters { get; set; } = new List<Models.CommandParameter>();
        public CommandExample[]? Examples { get; set; }

        public override string ToString()
        {
            return Name;
        }

        private void LoadCommandInfo()
        {
            var parameters = CmdletType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var parameter in parameters)
            {
                var parameterAttributes = parameter.GetCustomAttributes<ParameterAttribute>();
                if (parameterAttributes != null)
                {
                    foreach (var parameterAttribute in parameterAttributes)
                    {
                        var commandParameter = new Models.CommandParameter {
                            Description = parameterAttribute.HelpMessage,
                            Name = parameter.Name,
                            Type = parameter.PropertyType.Name,
                            Mandatory = parameterAttribute.Mandatory,
                            Position = parameterAttribute.Position < 0 ? 0 : parameterAttribute.Position,
                            ParameterSetName = parameterAttribute.ParameterSetName == "__AllParameterSets" ? null : parameterAttribute.ParameterSetName
                        };
                        Parameters.Add(commandParameter);
                    }
                }
            }

        }

    }
}
