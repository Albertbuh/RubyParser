using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Boolean
{
    public class Constant : Expr
    {
        public Constant(Token token, LType type) : base(token, type)
        { }
        public Constant(int val) : base(new Num(val), LType.Int)
        { }

        public static readonly Constant
            True = new Constant(Word.True, LType.Bool),
            False = new Constant(Word.False, LType.Bool);

        public override void Jumping(int t, int f)
        {
            if (this == True && t != 0)
                Emit("goto L" + t);
            else if (this == False && f != 0)
                Emit("goto L" + f);
        }

    }
}
