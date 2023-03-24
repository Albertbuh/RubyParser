using RubyParser.lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubyParser.inter;

namespace RubyParser.symbols_types
{
    /// <summary>
    /// Work with symbol tables (helpful when program has several namespaces or nesting blocks 
    /// </summary>
    public class Env
    {
        private Dictionary<Token, Identificator> table;
        protected Env prev;
        public Env(Env p)
        {
            table = new Dictionary<Token, Identificator>();
            prev = p;
        }

        public void Put(Token token, Identificator id)
        {
            table.Add(token, id);
        }

        public Identificator? Get(Token token)
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
