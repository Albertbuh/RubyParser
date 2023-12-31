﻿using RubyParser.inter;
using RubyParser.inter.Boolean;
using RubyParser.inter.Statements;
using RubyParser.lexer;
using RubyParser.symbols_types;
using System.Text;

namespace RubyParser.parser
{
    public class RParser
    {
        private Lexer lexer; //our lexer
        private Token look; //preview
        Env? top = null; //current symb table
        int used = 0; //memory for declare

        public RParser(Lexer lex)
        {
            lexer = lex;
            Move();
        }

        /// <summary>
        /// Set new lexeme to 'look'
        /// </summary>
        private void Move()
        {
            look = lexer.Scan();
        }

        private void Error(string msg)
        {
            throw new Exception("Near line " + Lexer.line + " " + look + ": " + msg);
        }

        /// <summary>
        /// Set new lexeme to 'look' and give Exception if smthing bad in user code
        /// </summary>
        /// <param name="t"></param>
        private void Match(int t)
        {
            if (look.tag == t)
                Move();
            else
                Error("syntax error");
        }

        public void Parse()
        {
            Program();
        }

        /// <summary>
        /// Program -> Block
        /// </summary>
        public void Program()
        {
            Stmt s = Block();
            int begin = s.NewLabel();
            int after = s.NewLabel();
            s.EmitLabel(begin);
            s.Gen(begin, after);
            s.EmitLabel(after);
        }

        /// <summary>
        /// Block -> { decls stmts }
        /// </summary>
        /// <returns></returns>
        private Stmt Block()
        {
            Match(Tag.BEGIN);
            Match(Tag.OPERATOREND);
            Env savedEnv = top;
            top = new Env(top);
            Stmt s = Stmts();
            Match(Tag.END);
            Match(Tag.OPERATOREND);
            top = savedEnv;
            return s;
        }

        /// <summary>
        /// <para> Tag.END need to check 'end' keyword</para>
        /// <para> Tag.ELSE added to process construction 'if..else..end'</para>
        /// </summary>
        private bool IsEndOfBlock
        {
            get
            {
                return look.tag == Tag.END
                      || look.tag == Tag.ELSE;
            }
        }

        /// <summary>
        /// used for constructions with only one front (with end) For example -> if..end
        /// </summary>
        /// <returns></returns>
        private Stmt BlockWithEnd()
        {
            Env savedEnv = top;
            top = new Env(top);
            Stmt s = Stmts();
            Match(Tag.END);
            Match(Tag.OPERATOREND);
            top = savedEnv;
            return s;
        }
        /// <summary>
        /// Used for constructinos without fronts. For example -> (if..else)..end (no 'end' before 'else')
        /// </summary>
        /// <returns></returns>
        private Stmt BlockWithoutEnd()
        {
            Env savedEnv = top;
            top = new Env(top);
            Stmt s = Stmts();
            top = savedEnv;
            return s;
        }

        /// <summary>
        /// Create sequences for Block of code
        /// </summary>
        /// <returns></returns>
        private Stmt Stmts()
        {
            if (IsEndOfBlock)
                return Stmt.Null;
            else
                return new Sequence(pStmt(), Stmts());
        }

        
        private Stmt pStmt()
        {
            Expr x;
            Stmt s, s1, s2;
            Stmt savedStmt;

            switch (look.tag)
            {
                case Tag.OPERATOREND: // '\n' or ';'
                    Move();
                    return Stmt.Null;
                case Tag.CASE:
                    Match(Tag.CASE);
                    x = pExpr();
                    Match(Tag.OPERATOREND);
                    Case casenode = new Case(x);
                    while (look.tag == Tag.WHEN)
                    {
                        s = Whens();
                        casenode.Add(s);
                    }

                    if (look.tag != Tag.ELSE)
                    {
                        Match(Tag.END);
                        Match(Tag.OPERATOREND);
                        return casenode;
                    }
                    Match(Tag.ELSE);
                    Match(Tag.OPERATOREND);
                    s = BlockWithEnd();
                    try
                    {
                        Match(Tag.OPERATOREND);
                    }
                    catch { }
                    casenode.els = s;
                    return casenode;

                // if (pBool) stmt end
                // if (PBool) stmt else stmt end
                case Tag.IF:
                    Match(Tag.IF);
                    Match('(');
                    x = pBool();
                    Match(')');
                    Match(Tag.OPERATOREND);
                    s1 = BlockWithoutEnd();
                    if (look.tag != Tag.ELSE)
                    {
                        //construction if..end
                        Match(Tag.END);
                        return new If(x, s1);
                    }

                    Match(Tag.ELSE);
                    if (look.tag != Tag.IF)
                        Match(Tag.OPERATOREND);
                    s2 = BlockWithEnd();
                    return new Else(x, s1, s2);

                case Tag.WHILE:
                    While whilenode = new While();
                    //push bp
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = whilenode;
                    Match(Tag.WHILE);
                    Match('(');
                    x = pBool();
                    Match(')');
                    s1 = pStmt();
                    whilenode.Init(x, s1);
                    //pop bp
                    Stmt.Enclosing = savedStmt;
                    return whilenode;
                case Tag.UNTIL:
                    Until untilnode = new Until();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = untilnode;
                    Match(Tag.UNTIL);
                    Match('(');
                    x = pBool();
                    Match(')');
                    s1 = pStmt();
                    untilnode.Init(x, s1);
                    return untilnode;
                case Tag.LOOP:
                    Loop loopnode = new Loop();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = loopnode;
                    Match(Tag.LOOP);
                    s = pStmt();
                    loopnode.Init(s);
                    Stmt.Enclosing = savedStmt;
                    return loopnode;
                case Tag.BREAK:
                    Match(Tag.BREAK);
                    Match(Tag.OPERATOREND);
                    return new Break();
                case Tag.PUTS:
                    Match(Tag.PUTS);
                    StringBuilder str = new StringBuilder();
                    do
                    {
                        str.Append(look.ToString());
                        Move();
                    }
                    while (look.tag != Tag.OPERATOREND);
                    Match(Tag.OPERATOREND);
                    return new Puts(str.ToString());
                case Tag.BEGIN:
                    return Block();
                default:
                    return Assign(); //assignment
            }
        }

        /// <summary>
        /// Create sequences for CASE Block of code 
        /// </summary>
        /// <returns></returns>
        private Stmt WhenStmts()
        {
            //(special case) of EndOfBlock
            if (look.tag == Tag.WHEN || look.tag == Tag.ELSE || look.tag == Tag.END)
                return Stmt.Null;
            else
                return new Sequence(pStmt(), WhenStmts());
        }
        /// <summary>
        /// Construct WHEN blocks of code for "case" construction
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private Stmt Whens()
        {
            Stmt s;
            Expr x;
            if (look.tag == Tag.WHEN)
            {
                Move();
                x = pExpr();
                Match(Tag.OPERATOREND);
                s = WhenStmts();
                return new When(x, s);
            }
            Error("Case error: 'when' undefiend");
            throw new InvalidOperationException("Case error: no 'when' node");
        }

        /// <summary>
        /// Assignment to variables (declaration included)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>

        private Stmt Assign()
        {
            Stmt stmt;
            Token t = look;
            Match(Tag.IDENTIFICATOR);
            Identificator? identificator = top.Get(t);
            if (look.tag == '=')
            {
                Move();
                Expr ex = pBool(); //our right part of id = ...
                if (identificator == null)
                {
                    LType type = ex.Type;
                    Identificator new_id = new Identificator((Word)t, type, used);
                    top.Put(t, new_id);
                    used += type.width;
                    stmt = new Set(new_id, ex);
                }
                else
                {
                    stmt = new Set(identificator, ex);
                }
                Match(Tag.OPERATOREND);
                return stmt;
            }
            Error(" '=' expected (or you've tried to write Key word but make mistake ;0");
            throw new InvalidOperationException(" '=' expected");

        }


        private Expr pBool()
        {
            Expr x = Join();
            while (look.tag == Tag.OR)
            {
                Token token = look;
                Move();
                x = new Or(token, x, Join());
            }
            return x;
        }
        private Expr Join()
        {
            Expr x = Equality();
            while (look.tag == Tag.AND)
            {
                Token token = look;
                Move();
                x = new And(token, x, Equality());
            }
            return x;
        }
        private Expr Equality()
        {
            Expr x = pRel();
            while (look.tag == Tag.EQUAL || look.tag == Tag.NOTEQUAL)
            {
                Token token = look;
                Move();
                x = new Rel(token, x, pRel());
            }
            return x;
        }
        private Expr pRel()
        {
            Expr x = pExpr();
            switch (look.tag)
            {
                case '<' or Tag.LOWEREQUAL or Tag.GREATEQUAL or '>':
                    {
                        Token token = look;
                        Move();
                        return new Rel(token, x, pExpr());
                    }
                default:
                    return x;
            }
        }
        private Expr pExpr()
        {
            Expr x = pTerm();
            while (look.tag == '+' || look.tag == '-')
            {
                Token token = look;
                Move();
                x = new Arithm(token, x, pTerm());
            }
            return x;
        }
        private Expr pTerm()
        {
            Expr x = pUnary();
            while (look.tag == '*' || look.tag == '/')
            {
                Token token = look;
                Move();
                x = new Arithm(token, x, pUnary());
            }
            return x;
        }
        private Expr pUnary()
        {
            switch (look.tag)
            {
                case '-':
                    Move();
                    return new Unary(Word.minus, pUnary());
                case '!' or Tag.NOT:
                    {
                        Token token = look;
                        Move();
                        return new Not(token, pUnary());
                    }
                default:
                    return Factor();
            }
        }

        /// <summary>
        /// Set different multipliers and constants
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Expr Factor()
        {
            Expr? x = null;
            switch (look.tag)
            {
                case '(':
                    Move();
                    x = pBool();
                    Match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(look, LType.Int);
                    Move();
                    return x;
                case Tag.REAL:
                    x = new Constant(look, LType.Float);
                    Move();
                    return x;
                case Tag.TRUE:
                    x = Constant.True;
                    Move();
                    return x;
                case Tag.FALSE:
                    x = Constant.False;
                    Move();
                    return x;
                case Tag.IDENTIFICATOR:
                    {
                        string s = look.ToString();
                        Identificator? id = top.Get(look);
                        if (id is null)
                        {
                            Error(look.ToString() + " undeclared");
                            throw new Exception();
                        }
                        else
                        {
                            Move();
                            if (look.tag != '[')
                                return id;
                            else
                                return Offset(id);
                        }
                    }
                default:
                    {
                        Error("syntax error");
                        throw new Exception();
                    }

            }
        }
        /// <summary>
        /// Need to find array addresses
        /// <para> I -> [E] | [E] I</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        private Access Offset(Identificator id)
        {
            Expr i;
            Expr w; //width
            Expr t1, t2;
            Expr loc;
            LType? type = id.Type;
            Match('[');
            i = pBool();
            Match(']');
            if (type != null)
            {
                type = ((symbols_types.Array)type).TypeOf;
                w = new Constant(type.width);
                t1 = new Arithm(new Token('*'), i, w);
                loc = t1;
                while (look.tag == '[')
                {
                    Match('[');
                    i = pBool(); //index
                    Match(']');
                    w = new Constant(type.width);
                    t1 = new Arithm(new Token('*'), i, w); //index*width
                    t2 = new Arithm(new Token('+'), loc, t1); //location + elem
                    loc = t2;
                }
                return new Access(id, loc, type);
            }
            else
            {
                Error("type error");
                throw new Exception();
            }
        }
    }
}

