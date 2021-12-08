using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateEQ : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (letter == '=')
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
                analizer.typeLexems.Add("Знак присваивания");
                analizer.State = new StateEQ1();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
