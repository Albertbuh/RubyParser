using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    /// <summary>
    /// defines constants for tokens
    /// (INDEX, MINUS, TEMP) - not are lex tokens, they are used in syntax tree
    /// </summary>
    public class Tag
    {
        public readonly static int
            AND = 1025, BASIC = 1026, BREAK = 1027, DO = 1028,
            ELSE = 1029, EQUAL = 1030, FALSE = 1031, GREATEQUAL = 1032,
            IDENTIFICATOR = 1033, IF = 1034, INDEX = 1035, LOWEREQUAL = 1036,
            MINUS = 1037, NOTEQUAL = 1038, NUM = 1039, OR = 1040,
            REAL = 1041, TEMP = 1042, TRUE = 1043, WHILE = 1044;
    }
}
