using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    /// <summary>
    /// Class represents identificators
    /// </summary>
    public class Identificator : Expr
    {
        public int Offset; //address offset
        public Identificator(Word id, LType type, int offset) : base(id, type)
        {
            Offset = offset;
        }
    }
}
