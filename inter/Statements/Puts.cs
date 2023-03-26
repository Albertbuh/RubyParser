using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Puts : Stmt
    {
        Expr ex;
        String str;
        public Puts(Expr x)
        {
            this.ex = x;
            str = "";
        }
        public Puts(string str)
        {
            this.str = str;
            this.ex = null;
        }

        public override void Gen(int b, int a)
        {
            if (ex != null)
                Emit("puts " + ex.ToString());
            else
                Emit("puts " + str);
        }


    }
}
