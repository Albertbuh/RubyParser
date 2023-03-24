
using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.symbols_types
{
    public class Array : LType
    {
        public LType TypeOf; //Type of array
        public int Size; //Number of elements
        public Array(int size, LType type) : base("[]", Tag.INDEX, size * type.width)
        {
            Size = size;
            TypeOf = type;
        }
        public override string ToString()
        {
            return "[" + Size + "] " + TypeOf?.ToString();
        }

    }
}
