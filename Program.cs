// See https://aka.ms/new-console-template for more information


using RubyParser;
using RubyParser.lexer;
using RubyParser.parser;

Lexer lex = new Lexer("testR.txt");
RParser parser = new RParser(lex);
parser.Program();
Console.WriteLine('\n');