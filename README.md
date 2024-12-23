# PowerShell.Docs by Stephan van Rooij

You can use this library to document your binary PowerShell modules. As a C# developer you can just document the library with XML comments in your code and this tool will generate the [maml file](#maml-file) and or [markdown files](#markdown-files) for you.

## Installation

This tool is distributed as a [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools). You can install it by running the following command:

```powershell
dotnet tool install -g SvRooij.PowerShell.Docs
```

## Usage

You can use this tool by running the following command:

```Shell
pwsh-docs --dll "path\to\your\module.dll" --use-xml-docs --md-output "path\to\output" --maml-file "path\to\YourModule.xml"
```

### Arguments

| Argument | Description |
| --- | --- |
| `--dll "{path-here}"` | The path to the dll file of your module. |
| `--use-xml-docs` | Use this flag to read the xml file (did you enable this?) |
| `--md-output "{path-here}"` | The path to the output directory for the markdown files. |
| `--maml-file "{path-here}"` | The path to the maml file. |

### Automate generation

You can automate the generation of the documentation by adding the command to your build pipeline. For example, you can add the following to your `csproj` file:

```xml
<PropertyGroup>
  <!-- Add  PowerShellDocsFile to first property group, change the filename to the file you want-->
  <PowerShellDocsFile>YourModuleName.dll-Help.xml</PowerShellDocsFile>
</PropertyGroup>
```

And then add the following build steps to your `csproj` file:

```xml
<!-- Generate the documentation after the build -->
 <Target Name="GenerateDocumentation" AfterTargets="AfterBuild" Outputs="$(PowerShellDocsFile)" Condition="!Exists($(PowerShellDocsFile))">
  <Message Text="Generating $(PowerShellDocsFile)" Importance="high" />
  <Message Text="Project path $(ProjectDir)" Importance="high" />
  <Message Text="Output path $(OutputPath)" Importance="high" />
  <Exec Command="pwsh-docs --dll $(ProjectDir)$(OutputPath)\Svrooij.WinTuner.CmdLets.dll --use-xml-docs --maml-file $(ProjectDir)\$(PowerShellDocsFile)" />
  <OnError ExecuteTargets="DocsGenerationError" />
 </Target>
<!-- Error target, to get a decent error message -->
 <Target Name="DocsGenerationError">
  <Error Text="Documentation could not be generated" />
 </Target>
<!-- Clean target, to remove the documentation file -->
 <Target Name="RemoveDocumentation" AfterTargets="CoreClean">
  <Delete Files="$(PowerShellDocsFile)" />
 </Target>
```

## Outputs

The tool will generate markdown files and or a maml file, depending on the arguments you provide.

### Markdown files

The markdown files are generated in the following structure, using [this template](./src/SvRooij.PowerShell.Docs/Templates/CommandMarkdown.tt):

```md
# {CommandName}

{CommandSynopsis}
{CommandDescription}

## Syntax

{Each parameter set..}

  {Complete syntax}

  {Markdown table with parameters}

{Each parameter set..}

{Each example..}

  {Example description}

  {Example code block}

{Each example..}

```

### MAML file

If the `--maml-file` argument is provided, the tool will generate a [maml file](https://en.wikipedia.org/wiki/Microsoft_Assistance_Markup_Language), which is a file used by the PowerShell help system.

## XML comments

To generate the documentation, you may  provide XML comments in your C# code. The better you document here, the better the output will be. The tool should also work without XML comments, but the output will be less detailed, and you'll be missing out on some features.

Here is an example of which xml comments you can use, from [Connect-WtWinTuner command](https://github.com/svrooij/WingetIntune/blob/9105090f376278cf5a47fa952d76b4a3c52c1002/src/Svrooij.WinTuner.CmdLets/Commands/ConnectWtWinTuner.cs#L14-L55):

```csharp
/// <summary>
/// <para type="synopsis">Connect to Intune</para>
/// <para type="description">A separate command to select the correct authentication provider, you no longer have to provide the auth parameters with each command.</para>
/// </summary>
/// <psOrder>3</psOrder>
/// <parameterSet>
/// <para type="name">Interactive</para>
/// <para type="description">If you're running WinTuner on your local machine, you can use the interactive browser login. This will integrate with the native browser based login screen on Windows and with the default browser on other platforms.</para>
/// </parameterSet>
/// <parameterSet>
/// <para type="name">UseManagedIdentity</para>
/// <para type="description">WinTuner supports Managed Identity authentication, this is the recommended way if you run WinTuner in the Azure Environment.</para>
/// </parameterSet>
/// <example>
/// <para type="name">Connect using interactive authentication</para>
/// <para type="description">This will trigger a login broker popup (Windows Hello) on Windows and the default browser on other platforms</para>
/// <code>Connect-WtWinTuner -Username "youruser@contoso.com"</code>
/// </example>
[Cmdlet(VerbsCommunications.Connect, "WtWinTuner", DefaultParameterSetName = ParamSetInteractive, HelpUri = "https://wintuner.app/docs/wintuner-powershell/Connect-WtWinTuner")]
[Alias("Connect-WinTuner")]
public class ConnectWtWinTuner : PSCmdlet
{
  ...
}
```

### Summary

Each command should have a `<summary>` tag with a `<para type="synopsis">` and `<para type="description">` tag. The synopsis is a short description of what the command does, the description is a more detailed explanation.

### Order

The `<psOrder>` tag is used to order the commands in the markdown files. This is also outputted in the markdown in the header as `sidebar_position`. Completely optional.

### Parameter sets

You either specify a `<parameterSet>` tag for each parameter set, with a `<para type="name">` and `<para type="description">` tag. The name is the name of the parameter set, the description is a short explanation of what the parameter set does.

If you don't specify any parameter sets, the tool will generate them for you based on the parameters of the command, but you cannot control the order in the markdown files or the description.

### Examples

You may provide one or more samples in the `<example>` tag. Each example may have a `<para type="name">` and `<para type="description">` tag. The name is the name of the example (will generate example {number} otherwise), the description is a short explanation of what the example does. Each example should have a `<code>` tag with the example code.
