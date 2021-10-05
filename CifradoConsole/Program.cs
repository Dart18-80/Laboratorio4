using System;
using System.Text;
using LibreriaCifrados;

namespace CifradoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cifrado por ZizZag y Cesar");
            Console.WriteLine("_____________________________________________________________________");
            bool verificar = true;
            string DatoString = default;
            byte[] DatoByte = default;
            CifradoDeCesar CifCesar = new CifradoDeCesar();
            CifradoZigZag CifZigZag = new CifradoZigZag();

            while (verificar)
            {
                Console.WriteLine("Seleccione el numero que desea");
                Console.WriteLine("1. Cifrar ZizZag");
                Console.WriteLine("2. Cifrar Cesar");
                Console.WriteLine("3. Decifrar ZizZag");
                Console.WriteLine("4. Decifrar Cesar");
                Console.WriteLine("5. Salir");

                int numero = Convert.ToInt32(Console.ReadLine());

                if (numero==1)
                {
                    Console.WriteLine("Ingrese el Texto que desea cifrar");
                    string Texto = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la Clave del Texto");
                    int Clave = Convert.ToInt32(Console.ReadLine());

                    object aCifrar = Texto;

                    CifZigZag.EncryptZZ(aCifrar, Clave);
                }
                else if(numero==2)
                {
                    Console.WriteLine("Ingrese el Texto que desea cifrar");
                    string Texto = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la Clave del Texto");
                    string Clave = Convert.ToString(Console.ReadLine());

                    object aCifrar = Texto;
                    string MensajeCifrado=CifCesar.Encrypt(aCifrar, Clave);
                    string MensajeCifrados = CifCesar.Decrypt(aCifrar, Clave);


                }
                else if (numero==3)
                {

                }
                else if (numero==4)
                {

                }
                else if (numero==5)
                {
                    verificar = false;
                }
                else
                {
                    Console.WriteLine("Seleccione un numero dentro del rango");
                    Console.ReadLine();

                }
            }
        }
    }
}
