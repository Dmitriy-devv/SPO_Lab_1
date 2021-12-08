using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateEQ1 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                analizer.State = new StateT6();
            }
            else if (char.IsLetterOrDigit(letter))
            {
                analizer.lexems.Add(letter.ToString());
                analizer.State = new StateEL1();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
