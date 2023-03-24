using RubyParser.symbols;
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
            Reserve(new Word("break", Tag.BREAK));
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
        private void readch()
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
        private bool readch(char c)
        {
            readch();
            if (peek != c)
                return false;
            peek = ' ';
            return true;
        }

        public Token scan()
        {
            //skip space and tabs
            for(; ; readch())
            {
                if (peek == ' ' || peek == '\t') continue;
                else if (peek == '\n') line++;
                else break;
            }

            //find composed tokens
            switch(peek)
            {
                case '&':
                    if (readch('&'))
                        return Word.and;
                    else
                        return new Token('&');

                case '|':
                    if (readch('|'))
                        return Word.or;
                    else
                        return new Token('|');

                case '=':
                    if (readch('='))
                        return Word.equal;
                    else
                        return new Token('=');
                case '!':
                    if (readch('='))
                        return Word.not_equal;
                    else
                        return new Token('!');

                case '<':
                    if (readch('='))
                        return Word.lower_equal;
                    else
                        return new Token('<');

                case '>':
                    if (readch('='))
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
                    val = 10 * val + Convert.ToByte(peek);
                    readch();
                } while(Char.IsDigit(peek));
                if (peek != '.')
                    return new Num(val);

                float fl_val = val;
                const float const_10 = 10;
                for(; ;)
                {
                    readch();
                    if (!Char.IsNumber(peek))
                        break;
                    fl_val = fl_val + Convert.ToByte(peek) / const_10;
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
                    readch();
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
