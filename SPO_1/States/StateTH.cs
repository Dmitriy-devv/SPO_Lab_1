using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateTH : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                if (analizer.lexems[analizer.lexems.Count - 1] != "then")
                {
                    analizer.IsError = true;
                    return;
                }
                analizer.typeLexems.Add("Ключевое слово");
                analizer.State = new StateT4();
            }
            else if (char.IsLetter(letter))
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
