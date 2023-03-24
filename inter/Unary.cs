using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    public class Unary : Operation
    {
        public Expr Expr;
        public Unary(Token token, Expr expr) : base(token, null)
        {
            Expr = expr;
            Type = LType.Max(LType.Int, Expr.Type);
            if (Type == null)
                Error("Type error");
        }
    }
}
