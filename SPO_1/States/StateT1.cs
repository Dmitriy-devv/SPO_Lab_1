using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateT1 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                return;
            }
            if (letter == '=')
            {
                analizer.lexems.Add("=");
                analizer.typeLexems.Add("Знак сравнения");
                analizer.State = new StateC();
            }
            else if (letter == '<' || letter == '>')
            {
                analizer.lexems.Add(letter.ToString());
                analizer.State = new StateC1();
            }
            else if (letter == ':')
            {
                analizer.lexems.Add(letter.ToString());
                analizer.State = new StateEQ();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
