using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LibreriaCifrados
{
    public class CifradoZigZag
    {
        protected List<byte[]> CadenaOlas = new List<byte[]>();
        protected List<byte[]> CadenaInversa = new List<byte[]>();

        void CrearDiccionario(byte[] Mensaje, int Lonclave)
        {
            int LongitudObj = (Mensaje.Length) % Lonclave;
            int NumerodeOlas = default;
            char[] Letra = new char[Lonclave];

            if (LongitudObj == 0)//Significa que las olas estan llenas
            {
                NumerodeOlas = Mensaje.Length / Lonclave;
                List<byte> cadena = new List<byte>();

                for (int i = 0; i < Mensaje.Length; i++)
                {
                    cadena.Add(Mensaje[i]);
                }
                CadenaOlas.Clear();
                LlenarOlas(cadena, Lonclave);
            }
            else
            {
                CrearDiccionario(LlenadoText(Mensaje, Lonclave), Lonclave);
            }
        }
        public void EncryptZZ(string ArchivoNuevo, string ArchivoCodificado, int key)
        {
            int LongOla = 2 + 2 * (key - 2);
            int contador = 0;
            long Caracteres = 0;
            byte[] Arreglo = new byte[12000000];

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
            byte[] ArregloZZ = new byte[contador];

            for (int i = 0; i < contador; i++)
            {
                ArregloZZ[i] = Arreglo[i];
            }


            byte[] mensajeCompleto = LlenadoText(ArregloZZ, LongOla);
            int CantOlas = mensajeCompleto.Length / LongOla;

            CadenaOlas.Clear();
            CadenaInversa.Clear();
            CrearDiccionario(mensajeCompleto, LongOla);

            List<byte> TextoCifrado = new List<byte>();
            for (int i = 0; i < key; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado.Add(CadenaOlas[j][0]);
                    }
                }
                else if (i == key - 1)
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado.Add(CadenaOlas[j][key - 1]);
                    }
                }
                else
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado.Add(CadenaOlas[j][i]);
                        TextoCifrado.Add(CadenaOlas[j][LongOla - i]);
                    }
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(ArchivoCodificado, FileMode.Create)))
            {
                for (int i = 0; i < TextoCifrado.Count; i++)
                {
                    writer.Write(TextoCifrado[i]);
                }
            }
        }
        public void Decrypt(string ArchivoNuevo, string ArchivoDecodificado, int clave)
        {
            int LongOla = 2 + 2 * (clave - 2);//la cantidad de caracteres de una ola
            int contador = 0;
            long Caracteres = 0;
            byte[] Arreglo = new byte[12000000];

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
            byte[] ArregloZZ = new byte[contador];

            for (int i = 0; i < contador; i++)
            {
                ArregloZZ[i] = Arreglo[i];
            }

            int NumOlas = ArregloZZ.Length / LongOla;

            SepararMensajeD(ArregloZZ, clave);

            List<byte> MensajeDescodificado = new List<byte>();

            for (int i = 0; i < NumOlas; i++)
            {
                MensajeDescodificado.Add(CadenaInversa[0][i]);
                for (int j = 1; j < clave - 1; j++)
                {
                    MensajeDescodificado.Add(CadenaInversa[j][2 * i]);
                }
                MensajeDescodificado.Add(CadenaInversa[clave - 1][i]);
                for (int k = clave - 2; k > 0; k--)
                {
                    MensajeDescodificado.Add(CadenaInversa[k][(2 * i) + 1]);
                }
            }
            MensajeDescodificado.Reverse();
            int cont = 0;
            bool llavewhile = true;
            while (llavewhile)
            {
                if (MensajeDescodificado[cont] == 0)
                {
                    MensajeDescodificado.RemoveAt(0);
                }
                else
                {
                    llavewhile = false;
                }
            }
            MensajeDescodificado.Reverse();

            using (BinaryWriter writer = new BinaryWriter(File.Open(ArchivoDecodificado, FileMode.Create)))
            {
                for (int i = 0; i < MensajeDescodificado.Count; i++)
                {
                    writer.Write(MensajeDescodificado[i]);
                }
            }
        }
        void SepararMensajeD(byte[] Cadena, int clave)
        {
            int LongOla = 2 + 2 * (clave - 2);//la cantidad de caracteres de una ola
            int NumOlas = Cadena.Length / LongOla;//cuantas olas hay en total
            CadenaInversa.Clear();
            byte[] MensajeSeparado = new byte[2 * NumOlas];

            int cont = 0;
            for (int i = 0; i < clave; i++)
            {
                MensajeSeparado = new byte[2 * NumOlas];
                if (i == 0)
                {
                    for (int j = 0; j < NumOlas; j++)
                    {
                        MensajeSeparado[j] = Cadena[j];
                        cont++;
                    }
                    CadenaInversa.Add(MensajeSeparado);
                }
                else if (i == clave - 1)
                {
                    for (int j = 0; j < NumOlas; j++)
                    {
                        MensajeSeparado[j] = Cadena[cont + j];
                    }
                    CadenaInversa.Add(MensajeSeparado);
                }
                else
                {
                    for (int j = 0; j < NumOlas * 2; j++)
                    {
                        MensajeSeparado[j] = Cadena[cont + j];
                    }
                    cont += NumOlas * 2;
                    CadenaInversa.Add(MensajeSeparado);
                }
            }
        }
        public byte[] LlenadoText(byte[] mensaje, int clave) 
        {
            int LongitudObj = (mensaje.Length) % clave;
            int Agregar = clave - LongitudObj;
            int extra = 0;
            if (LongitudObj != 0)
            {
                for (int i = 0; i < Agregar; i++)
                {
                    extra++;
                }
            }
            byte[] Nuevo = new byte [mensaje.Length+extra];
            for (int i = 0; i < Nuevo.Length; i++)
            {
                if (i<mensaje.Length)
                {
                    Nuevo[i] = mensaje[i];
                }
                else
                {
                    Nuevo[i] = default;
                }
            }
            return Nuevo;
        }

        public void LlenarOlas(List<byte> cadena, int clave) 
        {
            byte[] letras = new byte[clave];
            if (cadena.Count != 0)
            {    
                for (int i = 0; i < clave; i++)
                {
                    letras[i] = cadena[0];
                    cadena.RemoveAt(0);
                    if (i == clave - 1)
                    {
                        CadenaOlas.Add(letras);
                        LlenarOlas(cadena, clave);
                    }
                }
                letras = default;
            }
        }
    }
}
