using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateI : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (char.IsLetter(letter))
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
            }
            else if (analizer.escapeSymbols.Contains(letter))
            {

                if (analizer.lexems[analizer.lexems.Count - 1] == "if")
                {
                    analizer.typeLexems.Add("Ключевое слово");
                    analizer.State = new StateT0();

                }
                else
                {
                    if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                    {
                        analizer.typeLexems.Add("Число");
                    }
                    else analizer.typeLexems.Add("Идентификатор");
                    analizer.State = new StateT1();
                }

            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
