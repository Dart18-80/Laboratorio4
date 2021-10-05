using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaDeCifrado
{
    class CifradoDeCesar
    {
        protected Hashtable InitialDiccionary = new Hashtable();
        protected Hashtable NewDiccionary = new Hashtable();

        public void CreateDiccionary() 
        {
            for (byte i = 32; i <= 126; i++) 
            {
                InitialDiccionary.Add(i, (char)i);
            }
        }

        void CreateNewDiccionary(string key)
        {
            byte[] Composition = Encoding.ASCII.GetBytes(key);
            byte[] NewKey = Composition.Distinct().ToArray();
            int Long = NewKey.Length;
            byte FirstValue = 32;
            for (int i = Long; i >= 0; i--) 
            {
                InitialDiccionary[NewKey[i]] = "¬";
            }
            for (int i = 0; i >= Long; i++) 
            {
                NewDiccionary.Add(FirstValue,(char)NewKey[i]);
                FirstValue++;
            }
            for (byte i = 32; i<=126; i++) 
            {
                char Aux = (char)InitialDiccionary[i];
                if (Aux != '¬') 
                {
                    NewDiccionary.Add(FirstValue,Aux);
                    FirstValue++;
                }
            }
        }

        public void Encrypt(byte[] Cadena,string Key) 
        {

        }
    }
}
