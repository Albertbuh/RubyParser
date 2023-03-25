using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Until : Stmt
    {
        private Expr? expr;
        private Stmt? stmt;

        public Until()
        {
            expr = null;
            stmt = null;
        }

        public void Init(Expr ex, Stmt st)
        {
            expr = ex;
            stmt = st;
        }

        public override void Gen(int b, int a)
        {
            after = a;
            int label = NewLabel();
            expr?.Jumping(a, 0);
            EmitLabel(label);
            stmt?.Gen(b, label);
            Emit("goto L" + b);
        }
    }
}
