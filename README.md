# RubyParser

A small parser for [ruby](https://www.ruby-lang.org/en/) written using c# and recusrive descent algorithm.
The parser supports basic ruby types such as *numbers, booleans, strings and symbols*.
It also analize all ruby cycles like *for*, *while* or *case*.

The full list of processed operations here:
- Assignments for variables of numbers, booleans and symbols
- Unary and logical operations
- Expressions 
- Cycles (if, for, while, until, case, etc.)
- Some basic functions like *puts*
- break, continue

## Installation

- Requires dotnet 6 runtime, which you can load [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime?cid=getdotnetcore&os=windows&arch=x64)
- Any terminal emulator which you prefer :smile:

To install this project you just need to clone this repository to your device. After that use cmd or your terminal emulator to parse your ruby code. To clone this repository write in console:
```bash
git clone https://github.com/Albertbuh/RubyParser.git
```

## Using

Your ruby file must starts with keyword **begin** and finish with **end**
To parse ruby code you can use dotnet sdk like this:
```bash
dotnet run <path_to_ruby_file>
```
After that you will see exception if you has an error or you will get the output which represents your code structure.

## Troubleshooting

If you get an error about **unknown symbol** after **end** keyword in the end of file just add new line to the end of file, this error will be fixed in soon releases (maybe :smirk:) 
