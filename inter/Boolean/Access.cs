using RubyParser.symbols_types;
using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    /// <summary>
    /// Class which represents boolean assignment to identificators and arrays
    /// </summary>
    public class Access : Operation
    {
        public Identificator Array;
        public Expr Index;
        public Access(Identificator array, Expr index, LType? type) : base(new Word("[]", Tag.INDEX), type)
        {
            Array = array;
            Index = index;
        }

        public override Expr Gen()
        {
            return new Access(Array, Index.Reduce(), Type);
        }
        public override void Jumping(int t, int f)
        {
            EmitJumps(Reduce().ToString(), t, f);
        }

        public override string ToString()
        {
            return Array.ToString() + " [ " + Index.ToString() + " ] ";
        }
    }
}
