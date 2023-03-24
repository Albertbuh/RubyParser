using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.symbols
{
    public class LType : Word
    {
        public int width = 0; //used to allocate memory
        public LType(string lex, int tag, int w) : base(lex, tag)
        {
            width = w;
        }

        //basic lang types
        public static readonly LType
            Int = new LType("int", Tag.BASIC, 4),
            Float = new LType("float", Tag.BASIC, 8),
            Char = new LType("char", Tag.BASIC, 1),
            Bool = new LType("bool", Tag.BASIC, 1);

        public static Boolean IsNumeric(LType p)
        {
            if (p == LType.Char || p == LType.Int || p == LType.Float)
                return true;
            else 
                return false;
        }

        //Method for types convertation 
        public static LType? Max(LType p1, LType p2)
        {
            if (!IsNumeric(p1) || !IsNumeric(p2))
                return null;
            else if (p1 == LType.Float || p2 == LType.Float)
                return LType.Float;
            else if (p1 == LType.Int || p2 == LType.Int)
                return LType.Int;
            else
                return LType.Char;
        }
    }
}
