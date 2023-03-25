using RubyParser.inter;
using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.lexer
{
    public class Lexer
    {
        public static int line = 1;
        private char peek = ' ';
        private Dictionary<string, Word> words = new Dictionary<string, Word>();
        private void Reserve(Word word)
        {
            words.Add(word.Lexeme, word);
        }

        private string filename;
        StreamReader sr;

        public Lexer(string fname)
        {
            filename = fname;
            sr = new StreamReader(filename);
            //Reserve keywords
            Reserve(new Word("if", Tag.IF));
            Reserve(new Word("else", Tag.ELSE));
            Reserve(new Word("while", Tag.WHILE));
            Reserve(new Word("do", Tag.DO));
            //Reserve(new Word("do", Tag.DOWHILE)); //to langs, which use do while construction
            Reserve(new Word("break", Tag.BREAK));
            Reserve(new Word("and", Tag.AND));
            Reserve(new Word("or", Tag.OR));
            Reserve(new Word("not", Tag.NOT));
            Reserve(new Word("begin", Tag.BEGIN));
            Reserve(new Word("end", Tag.END));

            Reserve(new Word(";", Tag.OPERATOREND));
            Reserve(new Word("\n", Tag.OPERATOREND));

            Reserve(Word.True);
            Reserve(Word.False);
            //Reserve other objects
            Reserve(LType.Int);
            Reserve(LType.Bool);
            Reserve(LType.Char);
            Reserve(LType.Float);
        }

        /// <summary>
        /// Method read new input symbol and put it in "peek"
        /// </summary>
        private void Readch()
        {
            try
            {
                peek = (char)sr.Read();
            }
            catch(Exception ex)
            {
                sr.Close();
                Console.WriteLine("Lexer error in file reading:" + ex.Message);
            }
        }

        /// <summary>
        /// needed to find composed tokens
        /// </summary>
        private bool Readch(char c)
        {
            Readch();
            if (peek != c)
                return false;
            peek = ' ';
            return true;
        }

        public Token Scan()
        {
            //skip space and tabs
            for(; ; Readch())
            {
                if (peek == ' ' || peek == '\t' || peek == '\r') continue;                
                else break;
            }

            //goto new line 
            switch(peek)
            {
                case '\n':
                    do
                        line++;
                    while (Readch('\n'));
                    return new Token(Tag.OPERATOREND);
                case ';':
                    Readch();
                    for (; ; Readch())
                    {
                        if (peek == ' ' || peek == '\t' || peek == '\r') continue;
                        else if (peek == '\n')
                            line++;
                        else break;
                    }
                    return new Token(Tag.OPERATOREND);
                default:
                    break;
            }

            //find composed tokens
            switch(peek)
            {
                case '&':
                    if (Readch('&'))
                        return Word.and;
                    else
                        return new Token('&');

                case '|':
                    if (Readch('|'))
                        return Word.or;
                    else
                        return new Token('|');

                case '=':
                    if (Readch('='))
                        return Word.equal;
                    else
                        return new Token('=');
                case '!':
                    if (Readch('='))
                        return Word.not_equal;
                    else
                        return new Token('!');

                case '<':
                    if (Readch('='))
                        return Word.lower_equal;
                    else
                        return new Token('<');

                case '>':
                    if (Readch('='))
                        return Word.great_equal;
                    else
                        return new Token('>');               
                default:
                    break;
            }

            //find numbers
            if(Char.IsNumber(peek))
            {
                int val = 0;
                do
                {
                    val = 10 * val + (byte)Char.GetNumericValue(peek);//Convert.ToByte(peek);
                    Readch();
                } while(Char.IsDigit(peek));
                if (peek != '.')
                    return new Num(val);

                float fl_val = val;
                for(float divider = 10; ; divider*=10)
                {
                    Readch();
                    if (!Char.IsNumber(peek))
                        break;
                    fl_val = fl_val + (byte)Char.GetNumericValue(peek) / divider;
                }
                return new Real(fl_val);
            }

            //find word [ letter_ (letter_ | digit)* ]
            if(Char.IsLetter(peek))
            {
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    stringBuilder.Append(peek);
                    Readch();
                } while (Char.IsLetterOrDigit(peek));

                string terminal = stringBuilder.ToString();
                //check if we has such word in our dictionary
                if (words.ContainsKey(terminal))
                    return words[terminal];

                Word word = new Word(terminal, Tag.IDENTIFICATOR);
                words.Add(terminal, word);
                return word;
            }

            //all other symbols are simple tokens
            Token token = new Token(peek);
            peek = ' ';
            return token;            
        }
    }
}

