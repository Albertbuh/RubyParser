using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    /// <summary>
    /// Class which set field "tag" to make decision 
    /// in the process of analyze
    /// </summary>
    public class Token
    {
        public readonly int tag;
        public Token(int tag)
        {
            this.tag = tag;
        }
        public override string ToString()
        {
            return $"{(char)tag}";
        }

    }
}
