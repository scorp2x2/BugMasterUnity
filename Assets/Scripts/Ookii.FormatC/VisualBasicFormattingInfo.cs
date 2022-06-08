// Copyright © Sven Groot (Ookii.org) 2009
// BSD license; see license.txt for details.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ookii.FormatC
{
    /// <summary>
    /// Provides formatting info for Visual Basic code.
    /// </summary>
    /// <remarks>
    /// <para>
    ///   This formatter can format Visual Basic code, including the features introduced in Visual Basic 9.0.
    /// </para>
    /// <para>
    ///   Note that keywords that are contextual keywords in Visual Basic are not treated as contextual by this
    ///   formatter but will always be colored as keywords.
    /// </para>
    /// <para>
    ///   XML literals are supported, however the XML literals must be marked explicitly with with [xml][/xml].
    ///   For example, this would look like this with a simple XML literal: <c>Dim xmlLiteral = [xml]&lt;Foo /&gt;[/xml]</c>.
    /// </para>
    /// <para>
    ///   The [xml][/xml] tags will not be included in the output, and the contents of those tags will be formatted
    ///   as XML literals. Embedded expressions in XML literals (which are delimited by &lt;%= %&gt; blocks) are also
    ///   supported, and the contents of embedded expressions will be formatted as Visual Basic code. However, due to the
    ///   limitations of regular expressions, having an XML literal inside an embedded expression in another XML
    ///   literal is not supported.
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="true" />
    public class VisualBasicFormattingInfo : IFormattingInfo
    {
        private static readonly CodeElement[] _patterns = new CodeElement[] {
                    new CodeElement("xmlLiteral", @"\[xml\](?<content>(.|\n)*?)\[/xml\]", typeof(XmlFormattingInfo)),
                    new CodeElement("comment", @"('|REM).*?$"),
                    new CodeElement("xmlImportAttributeName", @"(?<=Imports <).*?(?==)"),
                    new CodeElement("xmlImportAttributeDelimiter", @"(?<=Imports <.*?)="),
                    new CodeElement("xmlImportAttributeQuotes", @"(?<=Imports <.*?=.*)(""|')"),
                    new CodeElement("xmlImportAttributeValue", @"(?<=Imports <.*?=.*(""|')).*?(?=(""|'))"),
                    new CodeElement("string", @""".*?""c?"),
                    new CodeElement("escapedIdentifier", @"\[[a-zA-Z0-9_]+\]"),
                    new CodeElement("keyword", new string[] { "AddHandler", "AddressOf", "Alias", "And", "AndAlso", "As", "Boolean", 
                        "ByRef", "Byte", "ByVal", "Call", "Case", "Catch", "CBool", "CByte", 
                        "CChar", "CDate", "CDec", "CDbl", "Char", "CInt", "Class", 
                        "CLng", "CObj", "Const", "Continue", "CSByte", "CShort", "CSng", 
                        "CStr", "CType", "CUInt", "CULng", "CUShort", "Date", "Decimal", 
                        "Declare", "Default", "Delegate", "Dim", "DirectCast", "Do", "Double", 
                        "Each", "Else", "ElseIf", "End", "EndIf", "Enum", "Erase", "Error",
                        "Event", "Exit", "False", "Finally", "For", "Friend", "Function", 
                        "Get", "GetType", "Global", "GoSub", "GoTo", "Handles", "If", 
                        "Implements", "Imports", "In", "Inherits", "Integer", "Interface", 
                        "Is", "IsNot", "Let", "Lib", "Like", "Long", "Loop", "Me", 
                        "Mod", "Module", "MustInherit", "MustOverride", "MyBase", "MyClass", 
                        "Namespace", "Narrowing", "New", "Next", "Not", "Nothing", "NotInheritable", 
                        "NotOverridable", "Object", "Of", "On", "Operator", "Option", "Optional",
                        "Or", "OrElse", "Overloads", "Overridable", "Overrides", "ParamArray", 
                        "Partial", "Private", "Property", "Protected", "Public", "RaiseEvent", 
                        "ReadOnly", "ReDim", "RemoveHandler", "Resume", "Return", "SByte", 
                        "Select", "Set", "Shadows", "Shared", "Short", "Single", "Static", 
                        "Step", "Stop", "String", "Structure", "Sub", "SyncLock", "Then", 
                        "Throw", "To", "True", "Try", "TryCast", "TypeOf", "Variant", "Wend", 
                        "UInteger", "ULong", "UShort", "Using", "When", "While", "Widening", 
                        "With", "WithEvents", "WriteOnly", "Xor", "From", "Aggregate", "Distinct", "Group", "By", "Join",
                        "Let", "Order", "Select", "Skip", "Take", "Where", "Strict", "Explicit", "Infer"}),
                    new CodeElement("xmlDelimiter", @"((?<=\.)@)|((?<=\.)<)|((?<=\.<\S*?)>)|((?<=Imports )<)|((?<=Imports <.*?)>)"),
                    new CodeElement("preprocessor", new string[] { "#Const", "#Else", "#ElseIf", "#End", "#If", "#Region" })
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualBasicFormattingInfo"/> class.
        /// </summary>
        public VisualBasicFormattingInfo()
        {
        }


        #region IFormattingInfo Members


        /// <summary>
        /// Gets a list of regular expression patterns used to identify elements of the code.
        /// </summary>
        /// <value>
        /// A list of <see cref="CodeElement"/> classes that provide regular expressions for identifying elements of the code.
        /// </value>
        public IEnumerable<CodeElement> Patterns
        {
            get { return _patterns; }
        }

        /// <summary>
        /// Gets a value that indicates whether the language to be formatted is case sensitive.
        /// </summary>
        /// <value>
        /// Returns <see langword="false" />.
        /// </value>
        public bool CaseSensitive
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}
