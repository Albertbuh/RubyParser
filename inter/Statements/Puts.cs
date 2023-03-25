using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Puts : Stmt
    {
        Expr x;
        String str;
        public Puts(Expr x)
        {
            this.x = x;
            str = "";
        }
        public Puts(string str)
        {
            this.str = str;
            this.x = null;
        }

        public override void Gen(int b, int a)
        {
            if (x != null)
                Emit("puts " + x.ToString());
            else
                Emit("puts " + str);
        }


    }
}
