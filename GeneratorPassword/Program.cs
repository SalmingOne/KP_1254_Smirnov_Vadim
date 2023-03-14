using System;
using System.Collections.Generic;
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
        static Random random = new Random();
        static int len = 16;
        static string s = "";
        static int default_start = 0;
        static int default_stop = 2;
        static int min_dig = 0;
        static int min_let = 0;
        static bool v = true;
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                    if ((args.Contains("-u") || args.Contains("--uppercase")) && (args.Contains("-s") || args.Contains("--special")))
                    {
                        default_start = 1; default_stop = 4;v= false;
                    }
                    else if (args.Contains("-u") || args.Contains("--uppercase"))
                    {
                        default_start = 1; default_stop = 3;
                    }
                    else if (args.Contains("-s") || args.Contains("--special"))
                    {
                        default_start = -1;v= false;
                    }
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
                if (min_dig!=0 || min_let!=0) 
                { 
                    if (min_dig!=0 && min_let != 0)
                    {
                        int sum_len = min_let + min_dig;

                        if ((sum_len < len && v))
                        {
                            return;
                        }
                        else
                        {
                            while (min_dig > 0)
                            {
                                s += Number();
                                min_dig -= 1;
                            }
                            if (default_start == -1 || default_start == 0)
                            {
                                while (min_let > 0)
                                {
                                    s += GenLower();
                                    min_let -= 1;
                                }

                            }
                            else
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
                                    int rand1 = random.Next(default_start, default_stop);
                                    switch (rand1)
                                    {
                                        case -1:
                                            s += Special();
                                            break;
                                        case 3:
                                            s += Special();
                                            break;
                                    }
                                }
                            }
                        }

                    }
                    else if (min_dig != 0)
                    {
                        while (min_dig > 0)
                        {
                            s += Number();
                            min_dig -= 1;
                        }

                        int n = min_dig;
                        if (len > n)
                        {
                            while (s.Length < len)
                            {
                                int rand1 = random.Next(default_start,default_stop);
                                switch (rand1)
                                {
                                    case -1:
                                        s += Special();
                                        break;
                                    case 1:
                                        s += GenLower();
                                        break;
                                    case 2:
                                        s += GenUpper();
                                        break;
                                    case 3:
                                        s += Special();
                                        break;
                                }
                            }
                        }
                    }
                    else if (min_let != 0)
                    {
 
                        if (default_start == -1 || default_start == 0)
                        {
                            while (min_let > 0)
                            {
                                s += GenLower();
                                min_let -= 1;
                            }

                        }
                        else
                        {
                            while (min_let > 0)
                            {
                                s += GenUpper();
                                min_let -= 1;
                            }
                        }
                        int l = min_let;
                        int[] mas = { default_start, 1, default_stop };
                        if (len > l)
                        {
                            while (s.Length < len)
                            {
                                int rand1 = random.Next(mas.Length);
                                switch (mas[rand1])
                                {
                                    case -1:
                                        s += Special();
                                        break;
                                    case 1:
                                        s += Number();
                                        break;
                                    case 3:
                                        s += Special();
                                        break;
                                }
                            }
                        }


                    }
                    s = Shuffle(s);
                   
                }
                while (s.Length < len)
                {
                    int rand2 = random.Next(default_start, default_stop);
                    switch (rand2)
                    {
                        case -1:
                            s += Special();
                            break;
                        case 0:
                            s += GenLower();
                            break;
                        case 1:
                            s += Number();
                            break;
                        case 2:
                            s += GenUpper();
                            break;
                        case 3:
                            s += Special();
                            break;
                    }
                }
            }
            else
            {
                while (s.Length < len)
                {
                    int rand = random.Next(default_start, default_stop);
                    switch (rand)
                    {
                        case 0:
                            s += GenLower();
                            break;
                        case 1:
                            s += Number();
                            break;
                    }
                }

            }
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
