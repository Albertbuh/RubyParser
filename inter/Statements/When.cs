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
        public int nextL;
        public When(Expr expr, Stmt stmt)
        {
            this.expr = expr;
            this.stmt = stmt;
        }

        public  override void Gen(int b, int a)
        {
            //b=0 => next command
            //a => next 'when' label
            EmitLabel(a);
            nextL = NewLabel();
            Emit("WHEN NOT " + expr.ToString() + " goto L" + nextL); //go to next 'when' label
            stmt.Gen(0, nextL);
        }

        /// <summary>
        /// Just for case when we havn't "else", and we couldn't exit
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <param name="c"></param>
        public  void Gen(int b, int a, int c)
        {
            //b=0 => next command
            //a => next 'when' label
            EmitLabel(a);
            Emit("WHEN NOT " + expr.ToString() + " goto L" + c); //go to next 'when' label
            stmt.Gen(0, c);
        }
    }
}
