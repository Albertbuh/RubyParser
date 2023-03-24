using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    public class Not : Logical
    {
        public Not(Token op, Expr ex) : base(op, ex, ex)
        { }
        public override void Jumping(int t, int f)
        {
            Expr2.Jumping(f, t);
        }
        public override string ToString()
        {
            return Operator.ToString() + " " + Expr2.ToString();
        }
    }
}
