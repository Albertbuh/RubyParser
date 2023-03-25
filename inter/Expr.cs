using RubyParser.lexer;
using RubyParser.symbols_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.inter
{
    /// <summary>
    /// Class which work with expressions
    /// </summary>
    public class Expr : Node
    {
        public Token Operator;
        public LType? Type;
        private protected Expr(Token operat, LType? type)
        {
            Operator = operat;
            Type = type;
        }

        /// <summary>
        /// Return right part of command
        /// For example for E = E1 + E2 *gen* return E1 + E2
        /// </summary>
        /// <returns> Some Expression </returns>
        public virtual Expr Gen()
        { return this; }

        /// <summary>
        /// Method which encapsulate expression to single address
        /// </summary>
        /// <returns>Encapsed Expression -> Constant, Identificator or Temporary variable</returns>
        public virtual Expr Reduce()
        { return this; }

        /// <summary>
        /// <para>Go to 't' if it's true </para>
        /// <para>Go to 'f' if it's false</para>
        /// </summary>
        /// <param name="t"> true </param>
        /// <param name="f"> false </param>
        public virtual void Jumping(int t, int f)
        {
            EmitJumps(this.ToString(), t, f);
        }
        public void EmitJumps(string test, int t, int f)
        {
            if (t != 0 && f != 0)
            {
                Emit("if (" + test + ") goto L" + t);
                Emit("goto L" + f);
            }
            else if (t != 0)
                Emit("ifTrue (" + test + ") goto L" + t);
            else if (f != 0)
                Emit("ifFalse (" + test + ") goto L" + f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Operator of expression</returns>
        public override string ToString()
        {
            return Operator.ToString();
        }
    }
}
