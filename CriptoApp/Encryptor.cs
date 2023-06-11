using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoApp
{
    class Encryptor
    {
        public delegate char EncyrptFunction(char c);
        EncyrptFunction encryptFunc;
        string name;
        string key;

        public Encryptor(EncyrptFunction func, string name, string key)
        {
            encryptFunc = func;
            this.name = name;
            this.key = key;
        }
        public string Key { get { return key; } }

        public char encrypt(char symbol)
        {
            return encryptFunc(symbol);
        }
    }
}
