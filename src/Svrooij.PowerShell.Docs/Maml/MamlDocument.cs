using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svrooij.PowerShell.Docs.Maml;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://msh")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://msh", IsNullable = false)]
public partial class helpItems
{

    private command[]? commandField;

    private string schemaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("command", Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
    public command[] command
    {
        get
        {
            return this.commandField;
        }
        set
        {
            this.commandField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string schema
    {
        get
        {
            return this.schemaField;
        }
        set
        {
            this.schemaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10", IsNullable = false)]
public partial class command
{
    [System.Xml.Serialization.XmlNamespaceDeclarations]
    public System.Xml.Serialization.XmlSerializerNamespaces xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(
          new[] { 
              new System.Xml.XmlQualifiedName("maml", "http://schemas.microsoft.com/maml/2004/10"),
              new System.Xml.XmlQualifiedName("command", "http://schemas.microsoft.com/maml/dev/command/2004/10"),
              new System.Xml.XmlQualifiedName("dev", "http://schemas.microsoft.com/maml/dev/2004/10"),
              new System.Xml.XmlQualifiedName("MSHelp", "http://msdn.microsoft.com/mshelp"),
          });

    private commandDetails detailsField;

    private description descriptionField;

    private commandSyntaxItem[]? syntaxField;

    private commandParameter[] parametersField;

    private commandInputType[] inputTypesField;

    private commandReturnValues returnValuesField;

    private alertSet alertSetField;

    private commandExample[]? examplesField;

    private commandRelatedLinks relatedLinksField;

    /// <remarks/>
    public commandDetails details
    {
        get
        {
            return this.detailsField;
        }
        set
        {
            this.detailsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public description description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("syntaxItem", IsNullable = false)]
    public commandSyntaxItem[]? syntax
    {
        get
        {
            return this.syntaxField;
        }
        set
        {
            this.syntaxField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("parameter", IsNullable = false)]
    public commandParameter[] parameters
    {
        get
        {
            return this.parametersField;
        }
        set
        {
            this.parametersField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("inputType", IsNullable = false)]
    public commandInputType[] inputTypes
    {
        get
        {
            return this.inputTypesField;
        }
        set
        {
            this.inputTypesField = value;
        }
    }

    /// <remarks/>
    public commandReturnValues returnValues
    {
        get
        {
            return this.returnValuesField;
        }
        set
        {
            this.returnValuesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public alertSet alertSet
    {
        get
        {
            return this.alertSetField;
        }
        set
        {
            this.alertSetField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("example", IsNullable = false)]
    public commandExample[]? examples
    {
        get
        {
            return this.examplesField;
        }
        set
        {
            this.examplesField = value;
        }
    }

    /// <remarks/>
    public commandRelatedLinks relatedLinks
    {
        get
        {
            return this.relatedLinksField;
        }
        set
        {
            this.relatedLinksField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandDetails
{

    private string nameField;

    private string verbField;

    private string nounField;

    private description descriptionField;

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string verb
    {
        get
        {
            return this.verbField;
        }
        set
        {
            this.verbField = value;
        }
    }

    /// <remarks/>
    public string noun
    {
        get
        {
            return this.nounField;
        }
        set
        {
            this.nounField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public description description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10", IsNullable = false)]
public partial class description
{

    private string paraField;

    /// <remarks/>
    public string para
    {
        get
        {
            return this.paraField;
        }
        set
        {
            this.paraField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandSyntaxItem
{

    private string nameField;

    private commandParameter[] parameterField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("parameter")]
    public commandParameter[] parameter
    {
        get
        {
            return this.parameterField;
        }
        set
        {
            this.parameterField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10", IsNullable = false)]
public partial class type
{

    private string nameField;

    private object uriField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public object uri
    {
        get
        {
            return this.uriField;
        }
        set
        {
            this.uriField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandParameter
{

    private string nameField;

    private description descriptionField;

    private commandParameterParameterValue[]? parameterValueGroupField;

    private commandParameterParameterValue parameterValueField;
    
    private type typeField;

    private string defaultValueField;

    private bool requiredField;

    private bool variableLengthField;

    private bool globbingField;

    private string pipelineInputField;

    private string positionField;

    private string aliasesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public description description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("parameterValue", IsNullable = false)]
    public commandParameterParameterValue[]? parameterValueGroup
    {
        get
        {
            return this.parameterValueGroupField;
        }
        set
        {
            this.parameterValueGroupField = value;
        }
    }

    /// <remarks/>
    public commandParameterParameterValue parameterValue
    {
        get
        {
            return this.parameterValueField;
        }
        set
        {
            this.parameterValueField = value;
        }
    }

    

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public type type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public string defaultValue
    {
        get
        {
            return this.defaultValueField;
        }
        set
        {
            this.defaultValueField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool required
    {
        get
        {
            return this.requiredField;
        }
        set
        {
            this.requiredField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool variableLength
    {
        get
        {
            return this.variableLengthField;
        }
        set
        {
            this.variableLengthField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool globbing
    {
        get
        {
            return this.globbingField;
        }
        set
        {
            this.globbingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pipelineInput
    {
        get
        {
            return this.pipelineInputField;
        }
        set
        {
            this.pipelineInputField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string position
    {
        get
        {
            return this.positionField;
        }
        set
        {
            this.positionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string aliases
    {
        get
        {
            return this.aliasesField;
        }
        set
        {
            this.aliasesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandParameterParameterValue
{

    private bool requiredField;

    private bool variableLengthField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool required
    {
        get
        {
            return this.requiredField;
        }
        set
        {
            this.requiredField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool variableLength
    {
        get
        {
            return this.variableLengthField;
        }
        set
        {
            this.variableLengthField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandInputType
{

    private type typeField;

    private description descriptionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public type type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public description description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandReturnValues
{

    private commandReturnValuesReturnValue returnValueField;

    /// <remarks/>
    public commandReturnValuesReturnValue returnValue
    {
        get
        {
            return this.returnValueField;
        }
        set
        {
            this.returnValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandReturnValuesReturnValue
{

    private type typeField;

    private description descriptionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public type type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public description description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10", IsNullable = false)]
public partial class alertSet
{

    private alertSetAlert alertField;

    /// <remarks/>
    public alertSetAlert alert
    {
        get
        {
            return this.alertField;
        }
        set
        {
            this.alertField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/2004/10")]
public partial class alertSetAlert
{

    private object paraField;

    /// <remarks/>
    public object para
    {
        get
        {
            return this.paraField;
        }
        set
        {
            this.paraField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandExample
{

    private string titleField;

    private string codeField;

    private remarks remarksField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
    public remarks remarks
    {
        get
        {
            return this.remarksField;
        }
        set
        {
            this.remarksField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/dev/2004/10", IsNullable = false)]
public partial class remarks
{

    private string paraField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public string para
    {
        get
        {
            return this.paraField;
        }
        set
        {
            this.paraField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/dev/command/2004/10")]
public partial class commandRelatedLinks
{

    private navigationLink navigationLinkField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10")]
    public navigationLink navigationLink
    {
        get
        {
            return this.navigationLinkField;
        }
        set
        {
            this.navigationLinkField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/maml/2004/10")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/maml/2004/10", IsNullable = false)]
public partial class navigationLink
{

    private string linkTextField;

    private string uriField;

    /// <remarks/>
    public string linkText
    {
        get
        {
            return this.linkTextField;
        }
        set
        {
            this.linkTextField = value;
        }
    }

    /// <remarks/>
    public string uri
    {
        get
        {
            return this.uriField;
        }
        set
        {
            this.uriField = value;
        }
    }
}


