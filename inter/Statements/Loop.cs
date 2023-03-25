using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Loop : Stmt
    {
        private Stmt? stmt;
        public Loop()
        {
            stmt = null;
        }
        public void Init(Stmt s)
        {
            stmt = s;
        }
        public override void Gen(int b, int a)
        {
            after = a; //save a
            stmt?.Gen(a, b);
            Emit("goto L" + b);
        }
    }
}
