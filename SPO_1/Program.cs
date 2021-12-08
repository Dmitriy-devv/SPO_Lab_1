using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SPO_1
{
    class Program
    {

        static void Main(string[] args)
        {
            var path = @"C:\SPO\note.txt";
            var textReader = new TextReader(path);
            var code = textReader.Read();


            //Этап 1 - Лексический анализатор
            Console.WriteLine($"Текст из файла:\n{code} ");
            Space();
            Console.WriteLine($" Лексема\t| Тип лексемы");
            Space();
            var analizer = new Analizer(new StateN());
            foreach(var letter in code)
            {
                if (analizer.Run(letter)) break;
            }

            
            if (analizer.IsError)
            {
                Console.WriteLine(analizer.ErrorState);
                return;
            }

            int i = 0;
            foreach (string line in analizer.lexems)
            {
                Console.WriteLine($" {line}\t\t| {analizer.typeLexems[i]}");
                i++;
            }

            //Этап 2 - Дерево выводов
            // Форматирование лексем
            var formated = FormatLexems(analizer.typeLexems, analizer.descriptions, analizer.lexems);

            // Вывод дерева свертки
            var triads = CreateTree(analizer.descriptions, analizer.keywords, formated, analizer.signs);

            // #3
            // Формирование триады
            Triads(triads, analizer.lexems, analizer.typeLexems, analizer.descriptions, out var cleared);

            // Ассемблерные команды
            var asmCommands = AsmCommands(cleared);

            // Оптимизация
            var optimized = Optimized(asmCommands);

            // Вывод на экран
            PrintOptimizedCommands(optimized);

            Console.ReadKey();

        }

        private static void Space()
        {
            Console.WriteLine("------------------------------------------");
        }

        private static List<string> FormatLexems(IReadOnlyList<string> types, IReadOnlyList<string> descriptions,
            IReadOnlyCollection<string> lexems)
        {
            Console.Write("\n");
            Console.Write("Лексемы:\n");
            Console.WriteLine(string.Join(" ", lexems));

            List<string> formated = new(lexems);

            // Замена нетерминальных символов как "a"
            for (var i = 0; i < lexems.Count; i++)
            {
                if (types[i] != descriptions[1] && types[i] != descriptions[2]) continue;

                formated.RemoveAt(i);
                formated.Insert(i, descriptions[7]);
            }

            return formated;
        }

        private static IEnumerable<List<string>> CreateTree(IReadOnlyList<string> descriptions,
            IReadOnlyList<string> keywords,
            IReadOnlyList<string> formated, IReadOnlyList<char> signs)
        {
            Console.Write("\n");
            Console.Write("Дерево:");

            List<List<string>> triads = new();

            // Правила свертки
            List<List<string>> rules = new()
            {
                new List<string>
                {
                    descriptions[6],
                    signs[4].ToString()
                },
                new List<string>
                {
                    descriptions[7],
                    signs[0].ToString(),
                    descriptions[7],
                },
                new List<string>
                {
                    descriptions[7],
                    signs[1].ToString(),
                    descriptions[7],
                },
                new List<string>
                {
                    descriptions[7],
                    signs[2].ToString(),
                    descriptions[7],
                },
                new List<string>
                {
                    descriptions[7],
                    signs[3].ToString() + signs[2],
                    descriptions[7],
                },
                new List<string>
                {
                    keywords[0],
                    descriptions[6],
                    keywords[1],
                    descriptions[6],
                    keywords[2],
                    descriptions[6]
                },
                new List<string>
                {
                    keywords[0],
                    descriptions[6],
                    keywords[1],
                    descriptions[6]
                }
            };

            List<string> result = new();  
            List<string> cloned = new(); 

            var count = 0;
            for (var i = 0; i < formated.Count * 10; i++)
            {
                if (i < formated.Count)
                {
                    result.Add(formated[i]);
                    cloned.Add(formated[i]);
                }

                var flag = false;

                for (var j = result.Count - 1; j >= 0; j--)
                {
                    var convs = new Stack<string>(); // Добавление в стек дял проверки правил
                    var convsCloned = new Stack<string>();

                    for (var k = result.Count - 1; k >= j; k--)
                    {
                        if (flag) break;

                        convs.Push(result[k]);
                        convsCloned.Push(cloned[k]);

                        // Сравнение с правилами свертки
                        if (!rules.Any(rule => rule.SequenceEqual(convs))) continue;

                        Console.Write("\n");
                        Console.Write(string.Join(" ", result));
                        Console.Write(" -->");

                        // #3
                        // Добавление в триады
                        if (!convs.Contains(";"))
                        {
                            triads.Add(new List<string>());
                            triads[count].AddRange(convsCloned);
                            triads[count].Add((count + 1).ToString());

                            count++;
                        }

                        
                        result.RemoveRange(j, convs.Count);
                        result.Add(descriptions[6]);

                        cloned.RemoveRange(j, convs.Count);
                        cloned.Add(count.ToString());

                        // Вывод отредактированных лексем
                        Console.Write("\n");
                        Console.Write(string.Join(" ", result));
                        Console.Write(" -->");

                        flag = true;
                    }
                }
            }

            return triads;
        }

        private static void Triads(IEnumerable<List<string>> triads, IEnumerable<string> lexems,
            IReadOnlyList<string> types,
            IReadOnlyList<string> descriptions, out List<List<string>>? cleared)
        {
            Console.Write("\n\n");
            Console.Write("Триады:");

            cleared = triads.ToList();

            // Get all variables and numbers from code
            List<string> values = lexems.Where((_, i) => types[i] == descriptions[1] || types[i] == descriptions[2])
                .ToList();

            var count = 0;
            foreach (var list in cleared)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    var lexem = list[i];
                    if (lexem != descriptions[7]) continue;

                    // Replace each var/number with "a" symbol
                    list.Remove(lexem);
                    list.Insert(i, values[count]);
                    count++;
                }

                Console.Write("\n");
                Console.Write(list.Last());
                Console.Write("\t");
                for (var index = 0; index < list.Count - 1; index++)
                {
                    var lexem = list[index];
                    Console.Write($"{lexem} ");
                }
            }
        }

        private static IEnumerable<List<string>> AsmCommands(List<List<string>>? cleared)
        {
            Console.Write("\n\n");
            Console.Write("Ассемблерные команды:");

            List<List<string>> asmCommands = new();

            if (cleared != null)
                foreach (var command in cleared)
                {
                    switch (command[1])
                    {
                        case ">":
                        case "<":
                        case "=":
                            {
                                var cmd = new List<string> // (#) cmp op1, op2
                            {
                                command[3], // Индекс операции
                                "cmp",
                                command[0],
                                command[2]
                            };

                                asmCommands.Add(cmd);
                                break;
                            }
                        case ":=":
                            {
                                var cmd = new List<string> // (#) mov op1, op2
                            {
                                command[3],
                                "mov",
                                command[0],
                                command[2]
                            };

                                asmCommands.Add(cmd);
                                break;
                            }
                        default:
                            {
                                if (command[4] == "else")
                                {
                                    // (#) cmp op1, op2
                                    // jl (^1)
                                    // jmp (^2)
                                    var cmd = new List<string>
                                {
                                    command[6],
                                    command[1],
                                    "jl",
                                    command[3],
                                    "jmp",
                                    command[5]
                                };

                                    asmCommands.Add(cmd);
                                }
                                else
                                {
                                    // (#) cmp op1, op2
                                    // jl (^1)
                                    var cmd = new List<string>
                                {
                                    command[4],
                                    command[1],
                                    "jl",
                                    command[3]
                                };

                                    asmCommands.Add(cmd);
                                }

                                break;
                            }
                    }
                }

            // Print table of assembly commands
            foreach (var asmCommand in asmCommands)
            {
                Console.Write("\n");
                Console.Write(string.Join(" ", asmCommand));
            }

            return asmCommands;
        }

        private static List<List<string>> Optimized(IEnumerable<List<string>> asmCommands)
        {
            Console.Write("\n\n");
            Console.Write("Оптимизация ассемблерных команд:");

            List<List<string>> optimized = new(asmCommands);

            // Оптимизация ассемблерных команд удалением дупликатов
            for (var i = 0; i < optimized.Count; i++)
            {
                for (var j = i + 1; j < optimized.Count; j++)
                {
                    // Если похожая операция в листе
                    if (!optimized[i].GetRange(1, optimized[i].Count - 1)
                        .SequenceEqual(optimized[j].GetRange(1, optimized[j].Count - 1))) continue;

                    foreach (var command in optimized)
                    {
                        // Изменение ссылки
                        for (var k = 1; k < command.Count; k++)
                        {
                            if (command[k] == optimized[j][0])
                            {
                                command[k] = optimized[i][0];
                            }
                        }
                    }

                    // Удаление повтора
                    optimized.RemoveAt(j);
                    break;
                }
            }

            return optimized;
        }

        private static void PrintOptimizedCommands(List<List<string>>? optimized)
        {
            if (optimized == null) return;

            foreach (var command in optimized)
            {
                Console.Write("\n");
                Console.Write(string.Join(" ", command));
            }
        }
    }
}

