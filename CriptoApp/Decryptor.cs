using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CriptoApp
{
    internal class Decryptor
    {
        public string Decrypt(string key, string textToDecrypt)
        {
            DecryptContainer container = new DecryptContainer(key);
            string s = "";
            
            for (int i = 0; i< textToDecrypt.Length; i++)
            {
                s += container.Decryptors[i % container.Decryptors.Count].Process(textToDecrypt[i]);
            }
            return s;
        }
    }
}
