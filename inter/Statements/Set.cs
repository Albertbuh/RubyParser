using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    /// <summary>
    /// Class which implements assignments
    /// </summary>
    public class Set : Stmt
    {
        public Identificator Id;
        public Expr Expr;
        public Set(Identificator id, Expr expr)
        {
            Id = id;
            Expr = expr;
            if (Check(Id.Type, Expr.Type) == null)
                Error("type error");
        }

        public LType? Check(LType? t1, LType? t2)
        {
            if (LType.IsNumeric(t1) && LType.IsNumeric(t2))
                return t2;
            else if (t1 == LType.Bool && t2 == LType.Bool)
                return t2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            Emit(Id.ToString() + " = " + Expr.Gen().ToString()); 
        }
    }
}
