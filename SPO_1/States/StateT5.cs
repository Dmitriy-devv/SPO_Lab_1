using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateT5 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                return;
            }
            if (char.IsLetter(letter))
            {
                analizer.lexems.Add(letter.ToString());
                analizer.State = new StateI();
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
