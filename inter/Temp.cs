using RubyParser.symbols_types;
using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    /// <summary>
    /// Class for temporary variables
    /// </summary>
    public class Temp : Expr
    {
        private static int count = 0;
        private int number = 0;
        public Temp(LType? type) : base(Word.temp, type)
        {
            number = ++count;
        }
        public override string ToString()
        {
            return "t" + number;
        }

    }
}
