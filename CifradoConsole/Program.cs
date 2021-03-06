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
                    Console.WriteLine("Ingrese la ruta del archivo original");
                    string Texto = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la ruta del archivo nuevo");
                    string Textonuevo = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la Clave del Texto");
                    int Clave = Convert.ToInt32(Console.ReadLine());

                    object aCifrar = Texto;

                    CifZigZag.EncryptZZ(Texto, Textonuevo, Clave);
                    Console.ReadLine();
                }
                else if(numero==2)
                {
                    Console.WriteLine("Ingrese la ruta del archivo que quiere cifrar que desea cifrar");
                    string Texto = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la ruta del archivo nuevo");
                    string Textonuevo = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la Clave del Texto");
                    string Clave = Convert.ToString(Console.ReadLine());

                    object aCifrar = Texto;
                    CifCesar.Encrypt(Clave, Texto, Textonuevo);
                    CifCesar.Decrypt(Clave, Textonuevo, "C:\\Users\\randr\\OneDrive\\Escritorio\\NuevoResultado.txt");


                }
                else if (numero==3)
                {
                    Console.WriteLine("Ingrese el Texto que desea decifrar");
                    string Texto = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la ruta del archivo nuevo");
                    string Textonuevo = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Ingrese la Clave del Texto");
                    int Clave = Convert.ToInt32(Console.ReadLine());

                    object aCifrar = Texto;

                    CifZigZag.Decrypt(Texto, Textonuevo, Clave);
                    Console.ReadLine();
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
