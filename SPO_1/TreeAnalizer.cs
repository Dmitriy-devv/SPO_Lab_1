using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class TreeAnalizer
    {
        private Analizer Analizer {get; set; }

        private List<Symbol> symbols;

        public TreeAnalizer(Analizer analizer)
        {
            Analizer = analizer;
            symbols = new();
            symbols.Add(new Symbol(SymbolName.IF));
            symbols.Add(new Symbol(SymbolName.THEN));
            symbols.Add(new Symbol(SymbolName.ELSE));
            symbols.Add(new Symbol(SymbolName.LITERAL));
            symbols.Add(new Symbol(SymbolName.ENDLINE));
            symbols.Add(new Symbol(SymbolName.ASSIGN));
            symbols.Add(new Symbol(SymbolName.MORE));
            symbols.Add(new Symbol(SymbolName.LESS));
            symbols.Add(new Symbol(SymbolName.EQUALS));
        }
        public string Run()
        {
            var stack = new Stack<string>();
            var result = new List<string>();
            int i = 0;
            foreach(var lexem in Analizer.lexems)
            {
                if(Analizer.typeLexems[i] == "Ключевое слово")
                {
                    if(lexem == "if")
                    {
                        //if(stack.Peek == 
                    }
                    else if(lexem == "then")
                    {

                    }
                    else if (lexem == "else")
                    {

                    }

                }
                else if(Analizer.typeLexems[i] == "Идентификатор")
                {

                }
                else if (Analizer.typeLexems[i] == "Число")
                {

                }
                else if (Analizer.typeLexems[i] == "Знак сравнения")
                {

                }
                else if (Analizer.typeLexems[i] == "Знак присваивания")
                {

                }
                else if (Analizer.typeLexems[i] == "Разделитель")
                {

                }
            }


            return string.Empty;
        }

    }

    public enum SymbolName
    {
        IF,
        THEN,
        ELSE,
        LITERAL,
        ENDLINE,
        EQUALS,
        MORE,
        LESS,
        ASSIGN
    }

    public enum Operation
    {
        Left,
        Right,
        None
    }
}
