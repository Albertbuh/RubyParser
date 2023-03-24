﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    public class Break : Stmt
    {
        Stmt? stmt;
        public Break()
        {
            if (Enclosing == null)
                Error("unenclosed break");
            stmt = Enclosing;
        }
        public override void Gen(int b, int a)
        {
            Emit("goto L" + stmt?.after);
        }
    }
}
