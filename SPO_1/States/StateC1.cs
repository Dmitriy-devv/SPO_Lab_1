using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateC1 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                analizer.typeLexems.Add("Знак сравнения");
                analizer.State = new StateT2();
            }
            else if (letter == '=')
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
                analizer.typeLexems.Add("Знак сравнения");
                analizer.State = new StateC();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
