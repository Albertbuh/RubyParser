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
        public Stmt? els;
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

            int nextlabel = NewLabel();
            //start 'when' blocks from end of List  
            for (int i = whens.Count - 1; i > 0; i--)
            {
                whens[i].Gen(0, nextlabel);
                nextlabel = ((When)whens[i]).nextL;
            }

            if (els!=null)
            {
                whens[0].Gen(0, nextlabel);
                nextlabel = ((When)whens[0]).nextL;
                EmitLabel(nextlabel);
                Emit("ELSE NOT" + " goto L" + a);
                els?.Gen(0, a);
            }
            else
            {
                ((When)whens[0]).Gen(0, nextlabel, a);
            }
        }
    
    }
}
