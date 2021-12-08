using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateEL1 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (char.IsLetterOrDigit(letter) || letter == '.')
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
            }
            else if (letter == ';')
            {
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
                analizer.lexems.Add(letter.ToString());
                analizer.typeLexems.Add("Разделитель");
                analizer.State = new StateEN();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
