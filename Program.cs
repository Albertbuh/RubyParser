// See https://aka.ms/new-console-template for more information

using RubyParser.lexer;
using RubyParser.parser;

if(args.Length >= 1)
{
    Lexer lex = new Lexer(args[0]);
    RParser parser = new RParser(lex);
    parser.Program();
    Console.WriteLine('\n');
}
else
{
    Console.WriteLine("Please, add path to file");
}
