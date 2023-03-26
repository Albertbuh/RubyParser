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
        List<Stmt> whens = new List<Stmt>();
        Stmt? els;
        public Case(Expr ex, Stmt? el = null)
        {
            expr = ex;
            els = el;
        }
        public void Add(Stmt when)
        {
            whens.Add(when);
        }

        
        public override void Gen(int b, int a)
        {
            Emit("CASE " + expr.ToString());

            int[] labels = new int[whens.Count];
            for (int i=0; i<labels.Length; i++)
            {
                 labels[i] = NewLabel();
            }
            for (int i = 0; i < whens.Count; i++)
            {
                whens[i].Gen(0, labels[i]);
            }

            int label = 0;
            if (els != null && label != 0)
            {
                EmitLabel(label);
                Emit("ELSE NOT" + " goto L" + a);
                els?.Gen(label, a);
            }
        }
    
    }
}
