// See https://aka.ms/new-console-template for more information


using RubyParser.lexer;
using RubyParser.parser;

Lexer lex = new Lexer("test.txt");
Parser parser = new Parser(lex);
parser.Program();
Console.WriteLine('\n');