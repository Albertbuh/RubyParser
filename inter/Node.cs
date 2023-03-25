using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubyParser.lexer;

namespace RubyParser.inter
{
    /// <summary>
    /// Node for syntax tree
    /// </summary>
    public class Node
    {
        private int lexline = 0; //save line to find errors
        private protected Node()
        {
            lexline = Lexer.line;
        }

        protected internal void Error(String s)
        {
            throw new Exception("near line " + lexline + ": " + s);
        }
        

        //methods for code printing
        private static int lables = 0;
        public int NewLabel()
        {
            return ++lables;
        }

        //print label
        public void EmitLabel(int i)
        {
            Console.Write("L" + i + ":");
        }

        //print triaddress command
        public void Emit(String codeline)
        {
            Console.WriteLine("\t" + codeline);
        }
    }
}
