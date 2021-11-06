using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibreriaCifrados
{
    public class CifradoDeCesar
    {
        protected Hashtable InitialDiccionary = new Hashtable();
        protected Hashtable DescifrarDiccionary = new Hashtable();

        void CreateNewDiccionary(byte[] key)
        {
            if (key != null) 
            {
                byte Contador = 0;
                byte CorrerByte = 0;
                for (long i = 0; i < key.Length; i++) 
                {
                    InitialDiccionary[Contador] = key[i];
                    DescifrarDiccionary[key[i]] = Contador;
                    Contador++;
                }
                while (Contador <= 255) 
                {
                    if (Saltar(Contador, key) == false)
                    {
                        InitialDiccionary[Contador] = CorrerByte;
                        DescifrarDiccionary[CorrerByte] = Contador;
                    }
                    else 
                    {
                        CorrerByte++;
                        InitialDiccionary[Contador] = CorrerByte;
                        DescifrarDiccionary[CorrerByte] = Contador;
                    }
                    Contador++;
                    CorrerByte++;
                }

            }
        }

        bool Saltar(byte Contador, byte[] key) 
        {
            for (long i = 0; i < key.Length; i++)
            {
                if (Contador == key[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void Encrypt(string Key, string ArchivoNuevo, string ArchivoCifrado)
        {
            int contador = 0;
            long Caracteres = 0;
            byte[] Arreglo = new byte[12000000];
            byte[] Nuevo = new byte[12000000];

            using (Stream Text = new FileStream(ArchivoNuevo, FileMode.OpenOrCreate, FileAccess.Read))
            {
                Caracteres = Text.Length;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(ArchivoNuevo, FileMode.Open)))
            {
                foreach (byte nuevo in reader.ReadBytes((int)Caracteres))
                {

                    Arreglo[contador] = nuevo;
                    contador++;
                }
            }

            string[] KeyChar = Key.Split();
            byte[] KeyByte = new byte[KeyChar.Length-1];
            for (long i = 0; i < KeyChar.Length; i++) 
            {
                KeyByte[i] = Convert.ToByte(KeyChar[i]);
            }

            CreateNewDiccionary(KeyByte);

            for (int i = 0; i<contador; i++) 
            {
                Nuevo[i] = (byte)InitialDiccionary[Arreglo[i]];
            }


            using (BinaryWriter writer = new BinaryWriter(File.Open(ArchivoCifrado, FileMode.Create)))
            {
                for (int i = 0; i < contador; i++)
                {
                    writer.Write(Nuevo[i]);
                }
            }
        }

        public void Decrypt(string Key, string ArchivoCifrado, string ArchivoDescifrado) 
        {
            int contador = 0;
            long Caracteres = 0;
            byte[] Arreglo = new byte[12000000];
            byte[] Nuevo = new byte[12000000];

            using (Stream Text = new FileStream(ArchivoCifrado, FileMode.OpenOrCreate, FileAccess.Read))
            {
                Caracteres = Text.Length;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(ArchivoCifrado, FileMode.Open)))
            {
                foreach (byte nuevo in reader.ReadBytes((int)Caracteres))
                {

                    Arreglo[contador] = nuevo;
                    contador++;
                }
            }

            string[] KeyChar = Key.Split();
            byte[] KeyByte = new byte[KeyChar.Length - 1];
            for (long i = 0; i < KeyChar.Length; i++)
            {
                KeyByte[i] = Convert.ToByte(KeyChar[i]);
            }

            CreateNewDiccionary(KeyByte);

            for (int i = 0; i < contador; i++)
            {
                Nuevo[i] = (byte)DescifrarDiccionary[Arreglo[i]];
            }


            using (BinaryWriter writer = new BinaryWriter(File.Open(ArchivoDescifrado, FileMode.Create)))
            {
                for (int i = 0; i < contador; i++)
                {
                    writer.Write(Nuevo[i]);
                }
            }
        }
    }
}
