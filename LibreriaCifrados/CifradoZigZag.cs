using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaCifrados
{
    public class CifradoZigZag
    {
        protected List<string[]> CadenaOlas = new List<string[]>();

        void CrearDiccionario(object key, int Lonclave) 
        {
            int LongitudObj = (Convert.ToString(key).Length) % Lonclave;
            string[] Olas = new string[Lonclave];
            for (int i = 0; i < LongitudObj; i++)
            {
                CadenaOlas.Add(Olas);
            }
        }

        public void EncryptZZ(object cadena, int key) 
        {
            int LongOla = 2 + 2 * (key - 2);
            CrearDiccionario(cadena, LongOla);
        }

    }
}
