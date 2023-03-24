using RubyParser.inter.Boolean;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{
    /// <summary>
    /// Set assignment for Array element
    /// </summary>
    public class SetElem : Stmt
    {
        public Identificator Array;
        public Expr Index;
        public Expr Expr;
        public SetElem(Access x, Expr y)
        {
            Array = x.Array;
            Index = x.Index;
            Expr = y;
            if (Check(x.Type, Expr.Type) == null)
                Error("type errro");
        }

        public LType? Check(LType? t1, LType? t2)
        {
            if (t1 is symbols_types.Array || t2 is symbols_types.Array)
            {
                return null;
            }
            else if (t1 == t2)
                return t2;
            else if (LType.IsNumeric(t1) && LType.IsNumeric(t2))
                return t2;
            else
                return null;
        }

        public override void Gen(int b, int a)
        {
            string s1 = Index.Reduce().ToString();
            string s2 = Expr.Reduce().ToString();
            Emit(Array.ToString() + " [ " + s1 + " ] = " + s2);
        }
    }
}
