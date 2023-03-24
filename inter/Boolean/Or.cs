using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    public class Or : Logical
    {
        public Or(Token oper, Expr ex1, Expr ex2) : base(oper, ex1, ex2)
        { }
        public override void Jumping(int t, int f)
        {
            int label = t != 0 ? t : NewLabel();
            Expr1.Jumping(label, 0);
            Expr2.Jumping(t, f);
            if( t == 0 ) 
                EmitLabel(label);
        }
    }
}
