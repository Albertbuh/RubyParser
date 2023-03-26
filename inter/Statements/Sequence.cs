using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    /// <summary>
    /// Generates instructions sequence
    /// </summary>
    public class Sequence : Stmt
    {
        private Stmt stmt1;
        private Stmt stmt2;
        public Sequence(Stmt stmt1, Stmt stmt2)
        {
            this.stmt1 = stmt1;
            this.stmt2 = stmt2;
        }
        public override void Gen(int b, int a)
        {
            
            if (stmt1 == Stmt.Null)
                stmt2.Gen(b, a);
            else if (stmt2 == Stmt.Null)
                stmt1.Gen(b, a);
            else
            {
                if (stmt1 == Stmt.Null && stmt2 == Stmt.Null)
                    Error("No statements in sequence");
                int label = NewLabel();
                stmt1.Gen(b, label);
                EmitLabel(label);
                stmt2.Gen(label, a);
            }
        }
    }
}
