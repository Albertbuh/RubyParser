using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    public class Operation : Expr
    {
        public Operation(Token token, LType? type) : base(token, type) 
        { }

        public override Expr Reduce()
        {
            Expr x = Gen();
            Temp t = new Temp(Type);
            Emit(t.ToString() + " = " + x.ToString());
            return t;
        }
    }
}
