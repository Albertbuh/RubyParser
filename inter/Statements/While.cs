using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class While : Stmt
    {
        private Expr? expr;
        private Stmt? stmt;
        public While()
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
            expr?.Jumping(0, a);
            int label = NewLabel();
            EmitLabel(label);
            stmt?.Gen(label, b);
            Emit("goto L" + b);
        }
    }
}
