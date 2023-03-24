using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    /// <summary>
    /// Work with real numbers
    /// </summary>
    public class Real : Token
    {
        public readonly float value;
        public Real(float value) : base(Tag.REAL)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
}
