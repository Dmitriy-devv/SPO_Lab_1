using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class Symbol
    {
        public Dictionary<SymbolName, Operation> operations { get; set; }
        public SymbolName Name { get; set; }

        public Symbol(SymbolName name)
        {
            Name = name;
            operations = new();
            SetOperations();
        }

        public void SetOperations()
        {
            switch (Name)
            {
                case SymbolName.IF:
                case SymbolName.THEN:
                case SymbolName.ELSE:
                    operations.Add(SymbolName.IF, Operation.Left);
                    operations.Add(SymbolName.THEN, Operation.None);
                    operations.Add(SymbolName.ELSE, Operation.None);
                    operations.Add(SymbolName.LITERAL, Operation.Left);
                    operations.Add(SymbolName.ENDLINE, Operation.None);
                    operations.Add(SymbolName.ASSIGN, Operation.None);
                    operations.Add(SymbolName.MORE, Operation.None);
                    operations.Add(SymbolName.LESS, Operation.None);
                    operations.Add(SymbolName.EQUALS, Operation.None);
                    break;
                case SymbolName.LITERAL:
                    operations.Add(SymbolName.IF, Operation.None);
                    operations.Add(SymbolName.THEN, Operation.None);
                    operations.Add(SymbolName.ELSE, Operation.None);
                    operations.Add(SymbolName.LITERAL, Operation.None);
                    operations.Add(SymbolName.ENDLINE, Operation.Right);
                    operations.Add(SymbolName.ASSIGN, Operation.Left);
                    operations.Add(SymbolName.MORE, Operation.Left);
                    operations.Add(SymbolName.LESS, Operation.Left);
                    operations.Add(SymbolName.EQUALS, Operation.Left);
                    break;
                case SymbolName.ENDLINE:
                    operations.Add(SymbolName.IF, Operation.Right);
                    operations.Add(SymbolName.THEN, Operation.Right);
                    operations.Add(SymbolName.ELSE, Operation.Right);
                    operations.Add(SymbolName.LITERAL, Operation.Right);
                    operations.Add(SymbolName.ENDLINE, Operation.None);
                    operations.Add(SymbolName.ASSIGN, Operation.None);
                    operations.Add(SymbolName.MORE, Operation.None);
                    operations.Add(SymbolName.LESS, Operation.None);
                    operations.Add(SymbolName.EQUALS, Operation.None);
                    break;
                case SymbolName.EQUALS:
                case SymbolName.MORE:
                case SymbolName.LESS:
                case SymbolName.ASSIGN:
                    operations.Add(SymbolName.IF, Operation.None);
                    operations.Add(SymbolName.THEN, Operation.None);
                    operations.Add(SymbolName.ELSE, Operation.None);
                    operations.Add(SymbolName.LITERAL, Operation.Left);
                    operations.Add(SymbolName.ENDLINE, Operation.None);
                    operations.Add(SymbolName.ASSIGN, Operation.None);
                    operations.Add(SymbolName.MORE, Operation.None);
                    operations.Add(SymbolName.LESS, Operation.None);
                    operations.Add(SymbolName.EQUALS, Operation.None);
                    break;
                default:
                    break;
            }
        }
    }
}
