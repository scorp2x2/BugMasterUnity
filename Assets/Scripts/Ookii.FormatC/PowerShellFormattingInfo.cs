// Copyright © Sven Groot (Ookii.org) 2009
// BSD license; see license.txt for details.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ookii.FormatC
{
    /// <summary>
    /// Provides formatting information for Microsoft PowerShell scripts.
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    public class PowerShellFormattingInfo : IFormattingInfo
    {
        private static readonly CodeElement[] _patterns = new CodeElement[] {
                    new CodeElement("comment", @"#.*?$"),
                    new CodeElement("string", @""".*?""|'.*?'"),
                    new CodeElement("keyword", new string[] { "function", "filter", "global", "script", "local", "private", "if", 
                        "else", "elseif", "for", "foreach", "in", "while", "switch", "continue", "break", "return", "default", 
                        "param", "begin", "process", "end", "throw", "trap"}),
                    new CodeElement("psOperator", new string[] { "-band", "-bor", "-match", "-notmatch", "-like", "-notlike", "-eq", 
                        "-ne", "-gt", "-ge", "-lt", "-le", "-is", "-imatch", "-inotmatch", "-ilike", "-inotlike", "-ieq", "-ine", 
                        "-igt", "-ige", "-ilt", "-ile" })
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellFormattingInfo"/> class.
        /// </summary>
        public PowerShellFormattingInfo()
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
        /// Returns <see langword="true" />.
        /// </value>
        public bool CaseSensitive
        {
            get { return true; }
        }

        #endregion
    }
}
