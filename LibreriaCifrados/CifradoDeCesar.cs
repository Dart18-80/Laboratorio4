using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaCifrados
{
    public class CifradoDeCesar
    {
        protected Hashtable InitialDiccionary = new Hashtable();
        protected Hashtable NewDiccionary = new Hashtable();

        public void CreateDiccionary() 
        {
            for (byte i = 33; i <= 126; i++) 
            {
                InitialDiccionary.Add(i, (char)i);
            }
        }

        void CreateNewDiccionary(string key)
        {
            byte[] Composition = Encoding.ASCII.GetBytes(key);
            byte[] NewKey = Composition.Distinct().ToArray();
            int Long = NewKey.Length;
            byte FirstValue = 33;
            for (int i = Long; i >= 0; i--) 
            {
                InitialDiccionary[NewKey[i]] = '¬';
            }
            for (int i = 0; i >= Long; i++) 
            {
                NewDiccionary.Add(FirstValue,(char)NewKey[i]);
                FirstValue++;
            }
            for (byte i = 33; i<=126; i++) 
            {
                char Aux = (char)InitialDiccionary[i];
                if (Aux != '¬') 
                {
                    NewDiccionary.Add(FirstValue,Aux);
                    FirstValue++;
                }
            }
        }

        public string Encrypt(object Cadena, string Key)
        {
            CreateDiccionary();
            string Archivo = Convert.ToString(Cadena);
            byte[] Traducir = Encoding.ASCII.GetBytes(Archivo);
            int LongitudArchivo = Traducir.Length;
            CreateNewDiccionary(Key);
            string NewArchivo = "";
            for (int i = 0; i <= LongitudArchivo; i++) 
            {
                NewArchivo += NewDiccionary[Traducir[i]];
            }
            NewDiccionary.Clear();
            InitialDiccionary.Clear();
            return NewArchivo;
        }

        public string Decrypt(object Cadena, string Key) 
        {
            CreateDiccionary();
            string Archivo = Convert.ToString(Cadena);
            byte[] Traducir = Encoding.ASCII.GetBytes(Archivo);
            int LongitudArchivo = Traducir.Length;
            CreateNewDiccionary(Key);
            string NewArchivo = "";
            for (int i = 0; i <= LongitudArchivo; i++)
            {
                NewArchivo += NewDiccionary[Traducir[i]];
            }
            NewDiccionary.Clear();
            InitialDiccionary.Clear();
            return NewArchivo;
        }
    }
}
