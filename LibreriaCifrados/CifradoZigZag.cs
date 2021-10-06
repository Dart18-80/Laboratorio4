using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaCifrados
{
    public class CifradoZigZag
    {
        protected List<string[]> CadenaOlas = new List<string[]>();

        void CrearDiccionario(object mensaje, int Lonclave) 
        {
            int LongitudObj = (Convert.ToString(mensaje).Length) % Lonclave;
            int NumerodeOlas = default;
            char[] Letra = new char[Lonclave];

            if (LongitudObj==0)//Significa que las olas estan llenas
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
                else if (i == key-1)
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado += CadenaOlas[j][key-1];
                    }
                }
                else
                {
                    for (int j = 0; j < CantOlas; j++)
                    {
                        TextoCifrado += CadenaOlas[j][i];
                        TextoCifrado += CadenaOlas[j][LongOla-i];
                    }
                }
            }
            return TextoCifrado;
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
