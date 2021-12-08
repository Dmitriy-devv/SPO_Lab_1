using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class StateEL : IAnalizerState
    {

        public void Read(Analizer analizer, char letter)
        {
            if (analizer.escapeSymbols.Contains(letter))
            {
                if (analizer.lexems[analizer.lexems.Count - 1] == "else")
                {
                    analizer.typeLexems.Add("Ключевое слово");
                    analizer.State = new StateT4();
                    return;
                }
                if (analizer.lexems[analizer.lexems.Count - 1] == "if")
                {
                    analizer.typeLexems.Add("Ключевое слово");
                    analizer.State = new StateT0();
                    return;
                }
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
                analizer.State = new StateT5();
            }
            else if (char.IsLetterOrDigit(letter) || letter == '.')
            {
                analizer.lexems[analizer.lexems.Count - 1] += letter.ToString();
            }
            else if (letter == ':')
            {
                if (analizer.lexems[analizer.lexems.Count - 1] == "else")
                {
                    analizer.IsError = true;
                    return;
                }
                if (analizer.IsRealNumber(analizer.lexems[analizer.lexems.Count - 1]))
                {
                    analizer.typeLexems.Add("Число");
                }
                else analizer.typeLexems.Add("Идентификатор");
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
