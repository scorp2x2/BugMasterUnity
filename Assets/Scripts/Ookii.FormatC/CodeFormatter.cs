// Copyright © Sven Groot (Ookii.org) 2009
// BSD license; see license.txt for details.
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Ookii.FormatC
{
    /// <summary>
    /// Provides source code syntax highlighting functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    ///   The <see cref="CodeFormatter"/> class will format source code based on the information provided
    ///   by an implementation of the <see cref="IFormattingInfo"/> interface.
    /// </para>
    /// <para>
    ///   The result will be HTML source code that will display the formatted source code when combined
    ///   with the appropriate style sheet.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// The following code sample shows how to use the <see cref="CodeFormatter"/> class to format
    /// C# source code.
    /// </para>
    /// <code>
    /// CodeFormatter formatter = new CodeFormatter();
    /// formatter.FormattingInfo = new CSharpFormattingInfo();
    /// string formattedHtml = formatter.FormatCode(System.IO.File.ReadAllText("MySourceFile.cs"));</code>
    /// </example>
    /// <threadsafety static="true" instance="false" />
    public class CodeFormatter
    {
        private IFormattingInfo _formattingInfo;
        private int _tabSpaces = 4;
        private string _cssClass = "code";
        private bool _outputLineNumbers = false;
        private string _lineNumberFormat = "{0,3}. ";

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFormatter"/> class.
        /// </summary>
        public CodeFormatter()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="IFormattingInfo" /> that provides information hot to format the source code.
        /// </summary>
        /// <value>
        /// The <see cref="IFormattingInfo" /> that provides information hot to format the source code.
        /// </value>
        public IFormattingInfo FormattingInfo
        {
            get { return _formattingInfo ?? (_formattingInfo = new CSharpFormattingInfo()); }
            set { _formattingInfo = value; }
        }

        /// <summary>
        /// Gets or sets the number of spaces that a tab character should be replaced with.
        /// </summary>
        /// <value>
        /// The number of spaces that a tab character should be replaced with. The default value is 4.
        /// </value>
        public int TabSpaces
        {
            get { return _tabSpaces; }
            set { _tabSpaces = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class name to use on the &lt;pre&gt; element in the output HTML.
        /// </summary>
        /// <value>
        /// The CSS class name to use on the &lt;pre&gt; element. The default value is "code".
        /// </value>
        /// <remarks>
        /// If you change this value, you must also modify your CSS stylesheet accordingly.
        /// </remarks>
        public string CssClass
        {
            get { return _cssClass; }
            set { _cssClass = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the output will contain line numbers.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the output should contain line numbers. Otherwise, <see langword="false" />.
        /// The default value is <see langword="false" />.
        /// </value>
        public bool OutputLineNumbers
        {
            get { return _outputLineNumbers; }
            set { _outputLineNumbers = value; }
        }

        /// <summary>
        /// Gets or sets the format string used to format the line numbers.
        /// </summary>
        /// <value>
        /// The <see href="http://msdn.microsoft.com/en-us/library/txafckwd.aspx">composite format string</see> used for format the line numbes. The default value is "{0,3}. ".
        /// </value>
        /// <remarks>
        /// The format string should contain the "{0}" placeholder in the position where the number itself should be.
        /// </remarks>
        public string LineNumberFormat
        {
            get { return _lineNumberFormat; }
            set { _lineNumberFormat = value; }
        }

        /// <summary>
        /// Formats the specifies source code as HTML.
        /// </summary>
        /// <param name="code">The code to format.</param>
        /// <returns>The formatted HTML.</returns>
        /// <example>For an example see <see cref="CodeFormatter"/>.</example>
        /// <exception cref="InvalidOperationException"><see cref="FormattingInfo"/> is <see langword="null" />.</exception>
        public string FormatCode(string code)
        {
            if (_formattingInfo == null)
                throw new InvalidOperationException(Properties.Resources.Error_NoFormattingInfo);

            StringBuilder result = new StringBuilder(code.Length * 2);

            // Normalize newlines
            code = code.Replace("\r\n", "\n").Replace("\r", "\n");
            // Convert tabs
            code = code.Replace("\t", new String(' ', _tabSpaces));


            FormatCodeInternal(code, result);
            result.Replace("\n", "\r\n");

            if (OutputLineNumbers)
            {
                string temp = result.ToString();
                result.Length = 0;
                using (StringReader reader = new StringReader(temp))
                {
                    int lineNumber = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ++lineNumber;
                        if (OutputLineNumbers)
                        {
                            result.Append("<span class=\"lineNumber\">");
                            result.AppendFormat(System.Globalization.CultureInfo.CurrentCulture, _lineNumberFormat, lineNumber);
                            result.Append("</span>");
                        }

                        result.AppendLine(line);
                    }
                }
            }

            // Convert leading spaces
            string temp2 = result.ToString();
            result.Length = 0;
            using (StringReader reader = new StringReader(temp2))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var trim = line.TrimStart();
                    var count = line.Length - trim.Length;
                    var spaces = "";

                    for (int i = 0; i < count; i++)
                    {
                        spaces += " ";
                    }

                    result.AppendLine(spaces + trim);
                }
            }

            return result.ToString().TrimEnd('\r', '\n');
        }

        private static string HtmlEncode(string input)
        {
            return input/*.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")*/;
        }

        private void FormatCodeInternal(string code, StringBuilder result)
        {
            Dictionary<string, Type> formatters;
            Regex regex = CreateRegex(out formatters);
            Match match = regex.Match(code);

            int previousMatchEnd = 0;
            while (match.Success)
            {
                if (previousMatchEnd < match.Index)
                    result.Append(HtmlEncode(code.Substring(previousMatchEnd, match.Index - previousMatchEnd)));

                foreach (CodeElement p in FormattingInfo.Patterns)
                {
                    ProcessGroup(result, match, p.Name, formatters);
                }
                previousMatchEnd = match.Index + match.Length;

                match = match.NextMatch();
            }

            if (previousMatchEnd < code.Length)
                result.Append(HtmlEncode(code.Substring(previousMatchEnd)));
        }

        private void ProcessGroup(StringBuilder result, Match match, string groupName, Dictionary<string, Type> formatters)
        {
            Group group = match.Groups[groupName];
            if (group.Success && group.Length != 0)
            {
                result.Append("<");
                switch (groupName)
                {
                    case "operator":
                        result.Append("#C63");
                        break;
                    case "comment":
                        result.Append("#57A64A");
                        break;
                    case "string":
                        result.Append("#D69D85");
                        break;
                    case "keyword":
                        result.Append("#569CD6");
                        break;
                    case "preprocessor":
                        result.Append("#00F");
                        break;
                    case "type":
                        result.Append("#4EC9B0");
                        break;
                    case "class":
                        result.Append("#4EC9B0");
                        break;
                }
                result.Append(">");

                Type infoType;
                if (formatters != null && formatters.TryGetValue(groupName, out infoType))
                {
                    IFormattingInfo info = (IFormattingInfo)Activator.CreateInstance(infoType);
                    CodeFormatter formatter = new CodeFormatter();
                    formatter.TabSpaces = TabSpaces;
                    formatter.FormattingInfo = info;
                    Group contentGroup = match.Groups["content"];
                    string content;
                    if (contentGroup != null && contentGroup.Success)
                        content = contentGroup.Value;
                    else
                        content = group.Value;
                    formatter.FormatCodeInternal(content, result);
                }
                else
                    result.Append(HtmlEncode(group.Value));
                result.Append("</color>");
            }
        }

        private Regex CreateRegex(out Dictionary<string, Type> formatters)
        {
            formatters = null;
            StringBuilder pattern = new StringBuilder();

            bool first = true;
            foreach (CodeElement p in FormattingInfo.Patterns)
            {
                if (first)
                    first = false;
                else
                    pattern.Append("|");

                pattern.Append(p.Regex);
                if (p.FormatterType != null)
                {
                    if (formatters == null)
                        formatters = new Dictionary<string, Type>();
                    formatters.Add(p.Name, p.FormatterType);
                }
            }

            RegexOptions options = RegexOptions.Multiline;
            if (!FormattingInfo.CaseSensitive)
                options |= RegexOptions.IgnoreCase;
            return new Regex(pattern.ToString(), options);
        }
    }
}
