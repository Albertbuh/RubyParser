// See https://aka.ms/new-console-template for more information


using RubyParser.lexer;
using RubyParser.parser;

Lexer lex = new Lexer("test.txt");
Parser parse = new Parser(lex);
parse.Program();
Console.WriteLine('\n');