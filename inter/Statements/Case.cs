using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Case : Stmt
    {
        Expr expr;
        Stmt whens;
        Stmt? els;
        public Case(Expr ex, Stmt w, Stmt? el = null)
        {
            expr = ex;
            whens = w;
            els = el;
        }
        public override void Gen(int b, int a)
        {
            Emit("CASE " + expr.ToString());
            
            if (els != null)
            {
                int label = NewLabel();
                whens.Gen(b, label);
                EmitLabel(label);
                Emit("ELSE NOT" + " goto L" + a);
                els?.Gen(label, a);
            }
            else
                whens.Gen(b, a);
        }
    
    }
}
