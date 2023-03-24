using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    public class Logical : Expr
    {
        public Expr Expr1, Expr2;
        protected Logical(Token op, Expr expr1, Expr expr2) : base(op, null)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Type = Check(Expr1.Type, Expr2.Type);
            if (Type == null)
                Error("type error");
        }

        public virtual LType? Check(LType? t1, LType? t2)
        {
            if (t1 == LType.Bool && t2 == LType.Bool)
                return LType.Bool;
            return null;
        }

        public override Expr Gen()
        {
            int f = NewLabel();
            int a = NewLabel();
            Temp temp = new Temp(Type);
            Jumping(0, f); // 0 is special mark that means that no goto
            Emit(temp.ToString() + " = true ");
            Emit("goto L" + a);
            EmitLabel(f);
            Emit(temp.ToString() + " = false ");
            EmitLabel(a);
            return temp;

        }

        public override string ToString()
        {
            return Expr1.ToString() + " " + Operator.ToString() + " " + Expr2.ToString();
        }
    }
}
