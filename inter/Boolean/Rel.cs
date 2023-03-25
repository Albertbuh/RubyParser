using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    /// <summary>
    /// Class to work with <, <=, ==, !=, >=, >
    /// </summary>
    public class Rel : Logical
    {
        public Rel(Token op, Expr ex1, Expr ex2) : base(op , ex1, ex2) { }

        public override LType? Check(LType? t1, LType? t2)
        {
            if (t1 is symbols_types.Array || t2 is symbols_types.Array)
                return null;
            else if (t1 == t2)
                return LType.Bool;
            else 
                return null;
        }

        public override void Jumping(int t, int f)
        {
            Expr a = Expr1.Reduce();
            Expr b = Expr2.Reduce();
            String test = a.ToString() + " " + Operator.ToString() +" "+ b.ToString();
            EmitJumps(test, t, f);
        }
    }
}
