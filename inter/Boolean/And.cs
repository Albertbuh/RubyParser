using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    public class And : Logical
    {
        public And(Token op, Expr ex1, Expr ex2) : base(op, ex1, ex2) 
        { }

        public override void Jumping(int t, int f)
        {
            int label = f != 0 ? f : NewLabel();
            Expr1.Jumping(0, label);
            Expr2.Jumping(t, f);
            if( f == 0)
                EmitLabel(label);
        }
    }
}
