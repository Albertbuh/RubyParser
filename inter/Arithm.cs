using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    public class Arithm : Operation
    {
        public Expr Expr1, Expr2;
        public Arithm(Token token, Expr expr1, Expr expr2) : base(token, null)
        {
            Expr1 = expr1;
            Expr2 = expr2;
            Type = LType.Max(Expr1.Type, Expr2.Type);
            if (Type == null)
                Error("Type error");
        }
        /// <summary>
        /// <para> Construct right part of command </para> 
        /// <para>Example: a+b*c</para>
        /// <para>Reduce() will return 'a'  and 't' where 't' is Reduce() for b*c</para>
        /// <para>Return: new Arithm(+, a, t);</para>
        /// </summary>
        /// <returns> </returns>
        public override Expr Gen()
        {
            return new Arithm(Operator, Expr1.Reduce(), Expr2.Reduce());
        }

        public override string ToString()
        {
            return Expr1.ToString() +  " " + Operator.ToString() + " " + Expr2.ToString();
        }
    }
}
