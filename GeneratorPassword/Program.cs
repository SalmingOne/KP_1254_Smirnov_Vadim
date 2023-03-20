using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPassword
{
    internal class Program
    {
        static char GenUpper()
        {
            int code = random.Next(65, 91);
            return (char)code;
        }
        static char GenLower()
        {
            int code = random.Next(97, 123);
            return (char)code;
        }
        static char Number()
        {
            int code = random.Next(48, 58);
            return (char)code;
        }
        static char Special()
        {
            string s = "#$&*-.";
            int code = random.Next(s.Length);
            return (char)s[code];
        }

        static void changeSymbol(string[] args)
        {
            if ((args.Contains("-u") && args.Contains("-s")) || (args.Contains("--uppercase") && args.Contains("--special")) || args.Contains("-us"))
                {
                    symbols.Add("special");
                    symbols.Add("Upper");
                    symbols.Remove("Lower");
                }
            else if (args.Contains("-u") || args.Contains("--uppercase"))
                {
                    symbols.Add("Upper");
                    symbols.Remove("Lower");
                }
            else if (args.Contains("-s") || args.Contains("--special"))
                {
                    symbols.Add("special");
                }
        }

        static void changeParam(string[] args)
        {
            if (args[0][0] != '-')
            {
                len = Convert.ToInt32(args[0]);
            }
            if (args[0].Contains("--length"))
            {
                string[] a = args[0].Split('=');
                len = Convert.ToInt32(a[1]);
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("--digits"))
                {
                    string[] b = args[i].Split('=');
                    min_dig = Convert.ToInt32(b[1]);
                }
                if (args[i].Contains("--letters"))
                {
                    string[] c = args[i].Split('=');
                    min_let = Convert.ToInt32(c[1]);
                }
            }
        }

        static string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
        static char genPass(List<string> list)
        {
            char str = new char();
            int rand2 = random.Next(symbols.Count);
            switch (symbols[rand2])
            {
                case "Lower":
                    str = GenLower();
                    break;
                case "Number":
                    str = Number();
                    break;
                case "Upper":
                    str = GenUpper();
                    break;
                case "special":
                    str = Special();
                    break;
            }
            return str;
        }
        static List<string> symbols = new List<string> {"Number", "Lower"};
        
        static Random random = new Random();
        static int len = 16;
        static string s = "";
        static int min_dig = 0;
        static int min_let = 0;
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                changeSymbol(args);
                changeParam(args);
                if (min_dig != 0 || min_let != 0)
                {
                    if (min_dig != 0 && min_let != 0)
                    {
                        int sum_len = min_let + min_dig;

                        if ((sum_len < len && !symbols.Contains("special")))
                        {
                            Console.WriteLine("Недостаочно символов для пароля");
                            return;
                        }
                        else
                        {
                            while (min_dig > 0)
                            {
                                s += Number();
                                min_dig -= 1;
                            }
                            if (symbols.Contains("Lower"))
                            {
                                while (min_let > 0)
                                {
                                    s += GenLower();
                                    min_let -= 1;
                                }

                            }
                            else if (symbols.Contains("Upper"))
                            {
                                while (min_let > 0)
                                {
                                    s += GenUpper();
                                    min_let -= 1;
                                }
                            }

                            if (len > sum_len)
                            {
                                while (s.Length < len)
                                {
                                    Special();
                                }
                            }
                        }

                    }
                    else if (min_dig != 0)
                    {
                        List<string> new_symbols = symbols;
                        new_symbols.Remove("Number");
                        while (min_dig > 0)
                        {
                            s += Number();
                            min_dig -= 1;
                        }

                        while (s.Length < len)
                        {
                            s += genPass(new_symbols);
                        }
                    }
                    else if (min_let != 0)
                    {
                        List<string> new_symbols = symbols;
                        if (symbols.Contains("Lower"))
                        {
                            ;
                            new_symbols.Remove("Lower");
                            while (min_let > 0)
                            {
                                s += GenLower();
                                min_let -= 1;
                            }

                        }
                        else if (symbols.Contains("Upper"))
                        {
                            new_symbols.Remove("Upper");
                            while (min_let > 0)
                            {
                                s += GenUpper();
                                min_let -= 1;
                            }
                        }

                        while (s.Length < len)
                        {
                            s += genPass(new_symbols);
                        }


                    }
                    s = Shuffle(s);

                }
                else
                {
                    while (s.Length < len)
                    {
                        s += genPass(symbols);
                    }
                }
            }
            else
            {
                while (s.Length < len)
                {
                    s += genPass(symbols);
                }
            }
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
