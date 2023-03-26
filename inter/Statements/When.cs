using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class When : Stmt
    {
        
        public Expr expr;
        private Stmt stmt;
        public When(Expr expr, Stmt stmt)
        {
            this.expr = expr;
            this.stmt = stmt;
        }

        public override void Gen(int b, int a)
        {
            //b=0 => next command
            //a => next 'when' label
            EmitLabel(a++);
            Emit("WHEN NOT " + expr.ToString() + " goto L" + a); //go to next 'when' label
            int label = NewLabel();
            stmt.Gen(0, label);
        }
    }
}
