﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.Automata.Templates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Automata;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class AutomataTextTemplate : AutomataTextTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n#if !defined(REGEX_MATCHER_TYPES_AND_CONSTRUCTORS)\r\n#define REGEX_MATCHER_TYPES" +
                    "_AND_CONSTRUCTORS\r\n\r\n");
            
            #line 10 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
  var matchers = new StringBuilder();

    foreach (var entry in automata)
    {
        matchers.Append(new AutomatonTextTemplate(manager, helperPredicates, entry.Key, entry.Value).TransformText());
    }

    if (automata.Count > 0)
    {
        OutputUTF8ToUTF16Decoder();
    }
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 22 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(helperPredicates.Format((name, body) => {
    return string.Format(@"
    static bool {0}(int c)
    {{
{1}
    }}", name, body);
})));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 29 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(matchers));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n#endif\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 33 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
 void OutputUTF8ToUTF16Decoder() 
    {
        
        #line default
        #line hidden
        
        #line 34 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
this.Write("    //*i is the current idex of str, size is the length of str, *i must be in [0." +
        ".size-1], initially 0\r\n    //*r is the the leftover lowsurrogate portion from fi" +
        "rst three bytes in 4byte encoding, intially 0\r\n    //*c is the next UTF16 charac" +
        "ter code if the return value is true and *i is the index to the next character e" +
        "ncoding\r\n    //if the return value is false then the UTF8 encoding was incorrect" +
        "\r\n    static bool UTF8toUTF16(unsigned short* r, UINT* i, unsigned short* c, UIN" +
        "T size, const unsigned char* str)\r\n    {\r\n        if (*r == 0)\r\n        {   //*r" +
        "==0 means that we are not in the middle of 4byte encoding\r\n            unsigned " +
        "short b1 = str[*i];\r\n            if (b1 <= 0x7F)\r\n            {\r\n               " +
        " *c = b1; \r\n                *i += 1;\r\n                return true;\r\n            " +
        "}\r\n            else if (0xC2 <= b1 && b1 <= 0xDF) //two byte encoding\r\n         " +
        "   {\r\n                *i += 1;\r\n                if (*i == size)\r\n               " +
        "     return false; \r\n                else {\r\n                    unsigned short " +
        "b2 = str[*i];\r\n                    if (0x80 <= b2 && b2 <= 0xBF)\r\n              " +
        "      {\r\n                        *c = ((b1 & 0x3F) << 6) | (b2 & 0x3F);\r\n       " +
        "                 *i += 1;\r\n                        return true;\r\n               " +
        "     }\r\n                    return false;\r\n                }\r\n            }\r\n   " +
        "         else if (0xE0 <= b1 && b1 <= 0xEF)  //three byte encoding\r\n            " +
        "{\r\n                *i += 1;\r\n                if (*i + 1 >= size)\r\n              " +
        "      return false; \r\n                else\r\n                {\r\n                 " +
        "   unsigned short b2 = str[*i];\r\n                    if ((b1 == 0xE0 && 0xA0 <= " +
        "b2 && b2 <= 0xBF) ||\r\n                        (b1 == 0xED && 0x80 <= b2 && b2 <=" +
        " 0x9F) ||\r\n                        (0x80 <= b2 && b2 <= 0xBF))\r\n                " +
        "    {\r\n                        *i += 1;\r\n                        unsigned short " +
        "b3 = str[*i];\r\n                        if (0x80 <= b3 && b3 <= 0xBF)\r\n          " +
        "              {\r\n                            *c = ((b1 & 0xF) << 12) | ((b2 & 0x" +
        "3F) << 6) | (b3 & 0x3F); //utf8decode the bytes\r\n                            *i " +
        "+= 1;\r\n                            return true;\r\n                        }\r\n    " +
        "                    return false; //invalid third byte\r\n                    }\r\n " +
        "                   return false; //invalid second byte\r\n                }\r\n     " +
        "       }\r\n            else if (0xF0 <= b1 && b1 <= 0xF4) //4 byte encoding decod" +
        "ed and reencoded into UTF16 surrogate pair (high, low)\r\n            {\r\n         " +
        "       *i += 1;\r\n                if (*i + 2 >= size) //(4 byte check)\r\n         " +
        "           return false;  //second byte, third byte or fourth byte is missing\r\n " +
        "               else\r\n                {\r\n                    unsigned short b2 = " +
        "str[*i];\r\n                    if ((b1 == 0xF0 && (0x90 <= b2 && b2 <= 0xBF)) ||\r" +
        "\n                        (b1 == 0xF4 && (0x80 <= b2 && b2 <= 0x8F)) ||\r\n        " +
        "                (0x80 <= b2 && b2 <= 0xBF))\r\n                    {\r\n            " +
        "            *i += 1;\r\n                        unsigned short b3 = str[*i];\r\n    " +
        "                    if (0x80 <= b3 && b3 <= 0xBF)\r\n                        {\r\n  " +
        "                          //set *c to high surrogate\r\n                          " +
        "  *c = 0xD800 | (((((b1 & 7) << 2) | ((b2 & 0x30) >> 4)) - 1) << 6) | ((b2 & 0x0" +
        "F) << 2) | ((b3 >> 4) & 3);\r\n                            *r = 0xDC00 | ((b3 & 0x" +
        "F) << 6); //set the low surrogate register\r\n                            *i += 1;" +
        "\r\n                            return true;\r\n                        }\r\n         " +
        "               else\r\n                            return false; //incorrect third" +
        " byte\r\n                    }\r\n                    else\r\n                        " +
        "return false; //incorrect second byte\r\n                }\r\n            }\r\n       " +
        "     else\r\n                return false; //incorrect first byte\r\n        }\r\n    " +
        "    else //compute the low surrogate\r\n        {\r\n            unsigned short b4 =" +
        " str[*i]; //we know *i < size due to the above check (4 byte check)\r\n           " +
        " if (0x80 <= b4 && b4 <= 0xBF)\r\n            {\r\n                *i += 1;\r\n       " +
        "         *c = (*r | (b4 & 0x3F)); //set *c to low surrogate\r\n                *r " +
        "= 0;                  //reset the low surrogate register\r\n                return" +
        " true;\r\n            }\r\n            return false; //incorrect fourth byte\r\n      " +
        "  }\r\n    }\r\n\r\n");

        
        #line default
        #line hidden
        
        #line 137 "C:\GitHub\Automata\src\Automata\Templates\AutomataTextTemplate.tt"
 }

    public AutomataTextTemplate(BREXManager manager, Dictionary<string, Automaton<BDD>> automata)
    {
        this.manager = manager;
        this.automata = automata;
        this.helperPredicates = new BDDHelperPredicates(manager.Solver, true /*Optimize for ascii*/);
    }

    BREXManager manager;
    Dictionary<string, Automaton<BDD>> automata; 
    BDDHelperPredicates helperPredicates; 

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class AutomataTextTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
