using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyParser.symbols
{
    /// <summary>
    /// Work with symbol tables (helpful when program has several namespaces or nesting blocks 
    /// </summary>
    public class Env
    {
        private Dictionary<Token, Id> table;
        protected Env prev;
        public Env(Env p)
        {
            table = new Dictionary<Token, Id>();
            prev = p;
        }

        public void Put(Token token, Id id)
        {
            table.Add(token, id);
        }

        public Id? Get(Token token)
        {
            for(Env e = this; e != null; e = e.prev)
            {
                if(e.table.ContainsKey(token))
                    return e.table[token];
            }
            return null;
        }
    }
}
