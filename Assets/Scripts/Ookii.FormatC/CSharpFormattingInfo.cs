// Copyright © Sven Groot (Ookii.org) 2009
// BSD license; see license.txt for details.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ookii.FormatC
{
    /// <summary>
    /// Provides formatting info for C# code.
    /// </summary>
    /// <remarks>
    /// <para>
    ///   This formatter provides support for C# 3.0 features (e.g. Linq), however contextual keywords are not
    ///   treated as contextual by this formatter and will always be colored as keywords.
    /// </para>
    /// <para>
    ///   Because this formatter does not use a full parser, it is unable to identify the context of keywords
    ///   and thus cannot determine when a keyword should be formatted as a type name. However, you can
    ///   specify identifiers that should be colored as type names using the <see cref="Types"/> property.
    ///   These identifiers will then always be formatted as type names (even in contexts where they are not).
    /// </para>
    /// </remarks>
    /// <threadsafety static="true" instance="false" />
    public class CSharpFormattingInfo : IFormattingInfo
    {
        List<CodeElement> _patterns;
        private IEnumerable<string> _types;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpFormattingInfo"/> class.
        /// </summary>
        public CSharpFormattingInfo()
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
            get
            {
                if (_patterns == null)
                {
                    _patterns = new List<CodeElement>(new CodeElement[] {
                    new CodeElement("comment", @"(/\*(.|\n)*?\*/)|(//.*?(?=$))"),
                    new CodeElement("string", @"(@("".*?"")*"".*?"")|("".*?(?<![^\\](\\\\)*\\)"")|('.*?(?<![^\\](\\\\)*\\)')"),
                    new CodeElement("escapedIdentifier", @"@[a-zA-Z0-9_]+"),
                    new CodeElement("keyword", new string[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
                        "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
                        "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "get", "goto", "if", "implicit",
                        "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator",
                        "out", "override", "partial", "params", "private", "protected", "public", "readonly", "ref", "return",
                        "sbyte", "sealed", "set", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
                        "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "value",
                        "virtual", "void", "volatile", "where", "while", "yield", "from", "where", "select", "group", "into",
                        "orderby", "join", "let", "var", "by" }),
                    ///TODO: если нужен класс добавьте
                    new CodeElement("class",new string[]{"List","MonoBehaviour","Dictionary","Array","DateTime","Object","String","Int32", "CodeFormatter","Transform","Mathf","SceneManager","SerializeField",
                    "Animator", "Debug", "Quaternion", "SkeletonGraphic", "Text", "InputField", "StringBuilder", "ScriptableObject", "TextArea", "Vector2","Vector3","Action", "Texture2D", "Type","Rect","Event",
                    "GUI", "GUIContent", "Exception","Convert","TimeSpan","PropertyInfo","Int64", "IMember","EventArgs", "TimeCode","MakeBitmapParameter", "IFormatProvider", "AnimatorControllerParameter", "Thread", "Math",
                    "TvdbEpisode", "TvdbSeries","Image", "MethodDefinition", "MethodReference", "Interlocked", "MemberCore"}),
                    new CodeElement("preprocessor", new string[] { "#if", "#else", "#elif", "#endif", "#define", "#undef", "#warning", "#error",
                    "#line", "#region", "#endregion", "#pragma" }) });
                    if (_types != null)
                        _patterns.Add(new CodeElement("type", _types));
                }

                return _patterns;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the language to be formatted is case sensitive.
        /// </summary>
        /// <value>
        /// Returns <see langword="true" />.
        /// </value>
        public bool CaseSensitive
        {
            get
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets a list of identifiers that should be treated as type names.
        /// </summary>
        /// <value>
        /// A list of identifiers that should be treated as type names.
        /// </value>
        /// <remarks>
        /// <para>
        ///   The context in which these names occur is not considered, so they will be formatted as type names
        ///   in whatever context they occur.
        /// </para>
        /// </remarks>
        public IEnumerable<string> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                _patterns = null;
            }
        }

    }
}
