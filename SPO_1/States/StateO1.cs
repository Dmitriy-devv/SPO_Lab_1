using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateO1 : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
                analizer.State = new StateT1();
            }
            else if (char.IsLetterOrDigit(letter) || letter == '.')
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
            }
            else if (letter == '=')
            {
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
                analizer.lexems.Add("=");
                analizer.typeLexems.Add("Знак сравнения");
                analizer.State = new StateC();
            }
            else if (letter == '<' || letter == '>')
            {
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
                analizer.lexems.Add(letter.ToString());
                analizer.State = new StateC1();
            }
            else
            {
                analizer.IsError = true;
            }
        }
    }
}
