using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{

    /// <summary>
    /// general functionality for classes Or And Not and the like.
    /// </summary>
    public class Logical : Expr
    {
        public Expr Expr1, Expr2; //logical operands
        protected Logical(Token op, Expr expr1, Expr expr2) : base(op, null)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Type = Check(Expr1.Type, Expr2.Type);
            if (Type == null)
                Error("type error");
        }

        //we need 2 operands of boolean type
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
            Jumping(0, f); // that means that True exit is next command after this one
                           // Fales exit is new label 'f'
            Emit(temp.ToString() + " = true"); //temp = true
            Emit("goto L" + a); //goto new label a
            EmitLabel(f);       //Gen label 'f' ->
            Emit(temp.ToString() + " = false "); // -> where temp is false
            EmitLabel(a); //generate 'a' label
            return temp;

        }

        public override string ToString()
        {
            return Expr1.ToString() + " " + Operator.ToString() + " " + Expr2.ToString();
        }
    }
}
