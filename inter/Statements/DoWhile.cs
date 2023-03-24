using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class DoWhile : Stmt
    {
        private Expr? expr;
        private Stmt? stmt;
        public DoWhile()
        {
            expr = null;
            stmt = null;
        }

        public void Init(Expr x, Stmt s)
        {
            expr = x; stmt = s;
            if (expr.Type != LType.Bool)
                expr.Error("boolean required in while");
        }

        public override void Gen(int b, int a)
        {
            after = a; //save a
            int label = NewLabel();
            stmt?.Gen(b, label);
            EmitLabel(label);
            expr?.Jumping(b, 0);
        }
    }

}
