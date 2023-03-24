using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    /// <summary>
    /// Class which represents whole numbers
    /// </summary>
    public class Num : Token
    {
        public readonly int value;
        public Num(int value) : base(Tag.NUM)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return value.ToString();
        }
    }
}
