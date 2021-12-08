using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPO_1
{
    public class Analizer
    {
        public List<string> lexems;
        public List<string> typeLexems;

        public IAnalizerState State;
        public string ErrorState { get; set; }
        private bool isError;
        public bool IsError {
            get
            {
                return isError;    // возвращаем значение свойства
            }
            set
            {
                ErrorState = "Ошибка из состояния: " + State.ToString();
                isError = value;
            }
        }

        public List<char> escapeSymbols = new()
        {
            '\t',
            '\n',
            ' ',
            '\r'
        };

        public List<string> descriptions = new()
        {
            "Ключевое слово",
            "Идентификатор",
            "Число",
            "Знак сравнения",
            "Знак присваивания",
            "Разделитель",
            "E",
            "a"
        };

        public List<string> keywords = new()
        {
            "if",
            "then",
            "else"
        };

        public List<char> signs = new()
        {
            '>',
            '<',
            '=',
            ':',
            ';'
        };

        public Analizer(IAnalizerState state)
        {
            State = state;
            lexems = new();
            typeLexems = new();
        }

        public bool Run(char letter)
        {
            State.Read(this, letter);
            return IsError;
        }

        public bool IsRealNumber(string line)
        {
            bool isContainsLetter = false;
            foreach (char letter in line)
            {
                if (char.IsLetter(letter) && letter != '.')
                {
                    isContainsLetter = true;
                }
            }
            return !isContainsLetter;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
