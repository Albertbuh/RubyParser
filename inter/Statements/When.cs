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
            //label of instruction code
            int label = NewLabel();
            //if 'false' go to 'a', else goto next command
            Emit("WHEN NOT " + expr.ToString() + " goto L" + a);
            EmitLabel(label);
            stmt.Gen(label, a);
        }
    }
}
