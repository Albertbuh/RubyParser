using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Else : Stmt
    {
        private Expr expr;
        private Stmt stmt1;
        private Stmt stmt2;
        public Else(Expr expr, Stmt stmt1, Stmt stmt2)
        {
            this.expr = expr;
            this.stmt1 = stmt1;
            this.stmt2 = stmt2;
            if (expr.Type != LType.Bool)
                expr.Error("boolean required in if");
        }

        public override void Gen(int b, int a)
        {
            //stmt1 label
            int label1 = NewLabel();
            //stmt2 label
            int label2 = NewLabel();
            // if 'true' go to stmt1 'false' to stmt2
            expr.Jumping(0, label2);
            EmitLabel(label1);
            stmt1.Gen(label1, a);
            Emit("goto L" + a);
            EmitLabel(label2);
            stmt2.Gen(label2, a);
        }
    }
}
