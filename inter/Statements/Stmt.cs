using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter.Statements
{

    /// <summary>
    /// Base class for all intructions
    /// </summary>
    public class Stmt : Node
    {
        public Stmt() { }
        //empty sequence of instructions
        public static Stmt Null = new Stmt();
        /// <summary>
        /// Generation of triaddress code
        /// </summary>
        /// <param name="a"> first command after code for this instruction</param>
        /// <param name="b"> start of code label</param>
        public virtual void Gen(int b, int a)
        { }
        //need to save lable 'a' 
        public int after = 0;
        //need to find all construction
        public static Stmt Enclosing = Stmt.Null;
    }
}
