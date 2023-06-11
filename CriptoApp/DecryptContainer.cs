using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoApp
{
    internal class DecryptContainer
    {
        List<DecryptProcessor> decryptors = new List<DecryptProcessor>();
        public DecryptContainer(string key)
        {
            DecryptProcessor decrypt = null;
            foreach (char c in key)
            {
                switch (c)
                {
                    case 'I':
                        decrypt = new DecrementDecrypt();
                        break;
                    case 'D':
                        decrypt = new IncrementDecrypt();
                        break;
                    case 'Z':
                        decrypt = new ZeroDecrypt();
                        break;
                }
                decryptors.Add(decrypt);
            }
        }
        public List<DecryptProcessor> Decryptors { get { return decryptors; } }
    }
}
