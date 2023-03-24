using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class If : Stmt
    {
        private Expr expr;
        private Stmt stmt;
        public If(Expr expr, Stmt stmt)
        {
            this.expr = expr;
            this.stmt = stmt;
            if (expr.Type != LType.Bool)
                expr.Error("boolean required in if");
        }

        public override void Gen(int b, int a)
        {
            //label of instruction's code
            int label = NewLabel();
            //skip if 'true', go to "a" if 'false'
            expr.Jumping(0, a);
            EmitLabel(label);
            stmt.Gen(label, a);
        }
    }
}
