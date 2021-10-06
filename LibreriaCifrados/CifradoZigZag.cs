using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LibreriaCifrados
{
    public class CifradoZigZag
    {
        protected List<string[]> CadenaOlas = new List<string[]>();
        protected List<string[]> CadenaInversa = new List<string[]>();

        void CrearDiccionario(object mensaje, int Lonclave)
        {
            int LongitudObj = (Convert.ToString(mensaje).Length) % Lonclave;
            int NumerodeOlas = default;
            char[] Letra = new char[Lonclave];

            if (LongitudObj == 0)//Significa que las olas estan llenas
            {
                char[] Cad1 = Convert.ToString(mensaje).ToCharArray();
                NumerodeOlas = Convert.ToString(mensaje).Length / Lonclave;
                List<string> cadena = new List<string>();

                for (int i = 0; i < Cad1.Length; i++)
                {
                    cadena.Add(Cad1[i].ToString());
                }
                CadenaOlas.Clear();
                LlenarOlas(cadena, Lonclave);
            }
            else
            {
                CrearDiccionario(LlenadoText(mensaje, Lonclave), Lonclave);
            }
        }

        public string EncryptZZ(object cadena, int key)
        {
            int LongOla = 2 + 2 * (key - 2);
            string mensaje = LlenadoText(cadena.ToString(), LongOla).ToString();
            int CantOlas = mensaje.Length / LongOla;

            CrearDiccionario(cadena, LongOla);

            string TextoCifrado = default;

            for (int i = 0; i < key; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado += CadenaOlas[j][0];
                    }
                }
                else if (i == key - 1)
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado += CadenaOlas[j][key - 1];
                    }
                }
                else
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado += CadenaOlas[j][i];
                        TextoCifrado += CadenaOlas[j][LongOla - i];
                    }
                }
            }
            return TextoCifrado;
        }
        public string Decrypt(object cadena, int clave)
        {
            string cad = cadena.ToString();
            int LongOla = 2 + 2 * (clave - 2);//la cantidad de caracteres de una ola
            int NumOlas = cad.Length / LongOla;

            SepararMensajeD(cadena, clave);

            string MensajeDescodificado = default;

            for (int i = 0; i < NumOlas; i++)
            {
                MensajeDescodificado += CadenaInversa[0][i];
                for (int j = 1; j < clave - 1; j++)
                {
                    MensajeDescodificado += CadenaInversa[j][2 * i];
                }
                MensajeDescodificado += CadenaInversa[clave - 1][i];
                for (int k = clave - 2; k > 0; k--)
                {
                    MensajeDescodificado += CadenaInversa[k][(2 * i) + 1];
                }
            }
            MensajeDescodificado = MensajeDescodificado.Replace("$" , string.Empty);
            return MensajeDescodificado;
        }
        void SepararMensajeD(object cadena, int clave) 
        {
            string cad = cadena.ToString();
            int LongOla = 2 + 2 * (clave - 2);//la cantidad de caracteres de una ola
            int NumOlas =cad.Length/LongOla;//cuantas olas hay en total
            char[] mensaje = cad.ToCharArray();
            CadenaInversa.Clear();
            string[] MensajeSeparado = new string[2 * NumOlas];

            int cont = 0;
            for (int i = 0; i < clave; i++)
            {
                MensajeSeparado = new string[2 * NumOlas];
                if (i == 0)
                {
                    for (int j = 0; j < NumOlas; j++)
                    {
                        MensajeSeparado[j] = mensaje[j].ToString();
                        cont++;
                    }
                    CadenaInversa.Add(MensajeSeparado);
                }
                else if (i == clave-1)
                {
                    for (int j = 0; j < NumOlas; j++)
                    {
                        MensajeSeparado[j] = mensaje[cont+j].ToString();
                    }
                    CadenaInversa.Add(MensajeSeparado);
                }
                else
                {
                    for (int j = 0; j < NumOlas * 2; j++)
                    {
                        MensajeSeparado[j] = mensaje[cont + j].ToString();
                    }
                    cont += NumOlas * 2;
                    CadenaInversa.Add(MensajeSeparado);
                }
            }
        }
        public object LlenadoText(object mensaje, int clave) 
        {
            int LongitudObj = (Convert.ToString(mensaje).Length) % clave;
            int Agregar = clave - LongitudObj;

            for (int i = 0; i < Agregar; i++)
            {
                mensaje+="$";
            }

            return mensaje;
        }

        public void LlenarOlas(List<string> cadena, int clave) 
        {
            if (cadena.Count != 0)
            {
                string[] letras = new string[clave];
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
            }
        }
    }
}
