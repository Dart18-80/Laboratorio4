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
        protected Hashtable LetraDiccionary = new Hashtable();


        public void CreateDiccionary() 
        {
            for (byte i = 0; i <= 255; i++) 
            {
                InitialDiccionary.Add(i, i);
            }
        }

        void CreateNewDiccionary(byte[] key)
        {

        }

        void CreateNewLetraDiccionary(string key)
        {
            byte[] Composition = Encoding.ASCII.GetBytes(key);
            byte[] NewKey = Composition.Distinct().ToArray();
            int Long = NewKey.Length;
            byte FirstValue = 32;
            for (int i = Long-1; i >= 0; i--) 
            {
                InitialDiccionary[NewKey[i]] = '¬';
            }

            for (int i = 0; i < Long; i++)
            {
                LetraDiccionary.Add((char)NewKey[i], FirstValue);
                FirstValue++;
            }

            for (byte i = 32; i <= 126; i++)
            {
                char Aux = (char)InitialDiccionary[i];
                if (Aux != '¬')
                {
                    LetraDiccionary.Add(Aux, FirstValue);
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
            for (int i = 0; i < LongitudArchivo; i++) 
            {
                NewArchivo += NewDiccionary[Traducir[i]];
            }
            NewDiccionary.Clear();
            InitialDiccionary.Clear();
            LetraDiccionary.Clear();
            return NewArchivo;
        }

        public string Decrypt(object Cadena, string Key) 
        {
            CreateDiccionary();
            string ObjString = (string)Cadena;
            char[] Aux = ObjString.ToCharArray();
            int LongitudArchivo = Aux.Length;
            CreateNewLetraDiccionary(Key);
            InitialDiccionary.Clear();
            CreateDiccionary();
            string NewArchivo = "";
            for (int i = 0; i < LongitudArchivo; i++)
            {
                byte Suport = (byte)LetraDiccionary[Aux[i]];
                NewArchivo += InitialDiccionary[Suport];
            }
            NewDiccionary.Clear();
            InitialDiccionary.Clear();
            LetraDiccionary.Clear();
            return NewArchivo;
        }
    }
}
