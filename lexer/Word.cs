using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    /// <summary>
    /// Works with lexemes for reserved words, identificators and 
    /// composed tokens like !=, >= etc.
    /// minus is equal '-'. For example -2 is "minus" 2.
    /// </summary>
    public class Word : Token
    {
        public string Lexeme = "";
        /// <summary>
        /// Create new token which represents reserved word, identificator or composed token
        /// </summary>
        /// <param name="lexeme"> terminal </param>
        /// <param name="tag"> token name(nonterminal) </param>
        public Word(string lexeme, int tag) : base(tag)
        {
            Lexeme = lexeme;
        }
        public override string ToString()
        {
            return Lexeme;
        }

        public static readonly Word
            and         = new Word("&&", Tag.AND),   //the word version like 'and' 'not' 'or' added in lexer constructor
            or          = new Word("||", Tag.OR),
            equal       = new Word("==", Tag.EQUAL),
            not_equal   = new Word("!=", Tag.NOTEQUAL),
            lower_equal = new Word("<=", Tag.LOWEREQUAL),
            great_equal = new Word(">=", Tag.GREATEQUAL),
            minus       = new Word("minus", Tag.MINUS),
            True        = new Word("true", Tag.TRUE),
            False       = new Word("false", Tag.FALSE),
            temp        = new Word("temp", Tag.TEMP);

    }
}
