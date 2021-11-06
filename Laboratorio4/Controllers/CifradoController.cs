using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaCifrados;
using System.Text.RegularExpressions;

namespace Laboratorio4.Controllers
{
    [Route("")]
    [ApiController]
    public class CifradoController : ControllerBase
    {
        public readonly IHostingEnvironment fistenviroment;
        public CifradoController(IHostingEnvironment enviroment)
        {
            this.fistenviroment = enviroment;
        }

        CifradoDeCesar CifCesar = new CifradoDeCesar();
        CifradoZigZag CifZigZag = new CifradoZigZag();

        [Route("api/cipher/zz")]//Cifrado ZigZag
        [HttpPost]
        public IActionResult ZizZag([FromForm] IFormFile file, [FromForm] string key)
        {
           
            string uploadsFolder = Path.Combine(fistenviroment.ContentRootPath, "Upload");
            string filepath = Path.Combine(uploadsFolder, file.FileName);
                if (!System.IO.File.Exists(filepath))
                {
                    using (var INeadLearn = new FileStream(filepath, FileMode.CreateNew))
                    {
                        file.CopyTo(INeadLearn);
                    }
                }

            string uploadsNewFolder = Path.Combine(fistenviroment.ContentRootPath, "UploadCifrados");
            string nom=Convert.ToString(file.FileName).Replace(".txt", string.Empty);
            string direccionNuevo = Path.Combine(uploadsNewFolder, nom + ".zz");
            System.IO.File.WriteAllLines(direccionNuevo, new string[0]);

            CifZigZag.EncryptZZ(filepath, direccionNuevo, Convert.ToInt32(key));



            return Ok("El archivo se creo exitosamente, se guardo en la carpeta UploadCifrados del Laboratorio");
        }
        [Route("api/cipher/csr")]
        [HttpPost]
        public IActionResult Cesar([FromForm] IFormFile file, [FromForm] string key)
        {
            object Lectura = Archivo(file, 1);
            string mensaje = Lectura.ToString();
            string[] cadenas = Regex.Split(mensaje, "[\r\n]+");
            int ciclo = cadenas.Length;
            string Linea = default;

            string uploadsNewFolder = Path.Combine(fistenviroment.ContentRootPath, "UploadCifrados");
            string nom = Convert.ToString(file.FileName).Replace(".txt", string.Empty);
            string direccionNuevo = Path.Combine(uploadsNewFolder, nom + ".csr");

            for (int i = 0; i < ciclo; i++)
            {
                if (!String.IsNullOrEmpty(cadenas[i]))
                {
                    Linea += CifCesar.Encrypt(cadenas[i].ToString(), key) + "\r\n";
                }
            }

            using (StreamWriter outFile = new StreamWriter(direccionNuevo))
                outFile.WriteLine(Linea);

            return Ok("El archivo se creo exitosamente, se guardo en la carpeta UploadCifrados del Laboratorio");
        }

        [Route("api/decipher")]
        [HttpPost]
        public IActionResult DecifrarCesar([FromForm] IFormFile file, [FromForm] string key) 
        {
            object Lectura = Archivo(file,2);
            string mensaje = Lectura.ToString();
            string[] cadenas = Regex.Split(mensaje, "[\r\n]+");
            int ciclo = cadenas.Length;
            string Linea = default;
            string[] extencion = file.FileName.Split('.');


            string uploadsFolder = Path.Combine(fistenviroment.ContentRootPath, "UploadCifrados");
            string filepath = Path.Combine(uploadsFolder, file.FileName);
            if (!System.IO.File.Exists(filepath))
            {
                using (var INeadLearn = new FileStream(filepath, FileMode.CreateNew))
                {
                    file.CopyTo(INeadLearn);
                }
            }

            string uploadsNewFolder = Path.Combine(fistenviroment.ContentRootPath, "Upload");
            string direccionNuevo = Path.Combine(uploadsNewFolder, extencion[0] +"DES"+ ".txt");
            System.IO.File.WriteAllLines(direccionNuevo, new string[0]);


            if (extencion[1] == "zz")
            {
                CifZigZag.Decrypt(filepath,direccionNuevo, Convert.ToInt32(key));

                return Ok("El archivo se creo exitosamente, se guardo en la carpeta UploadCifrados del Laboratorio");
            }
            else if (extencion[1] == "csr")
            {
                for (int i = 0; i < ciclo; i++)
                {
                    if (!String.IsNullOrEmpty(cadenas[i]))
                    {
                        Linea += CifCesar.Decrypt(cadenas[i].ToString(), key) + "\r\n";
                    }
                }
                using (StreamWriter outFile = new StreamWriter(direccionNuevo))
                    outFile.WriteLine(Linea);

                return Ok("El archivo se creo exitosamente, se guardo en la carpeta UploadCifrados del Laboratorio");
            }
            else
            {
                return StatusCode(500);
            }
        }

        public object Archivo(IFormFile file, int num)
        {
            string uploadsFolder = null;
            object aCifrar=default;
            string ccc = default;

            if (file!=null)
            {
                if (num==1)
                {
                    uploadsFolder = Path.Combine(fistenviroment.ContentRootPath, "Upload");
                }
                else
                {
                    uploadsFolder = Path.Combine(fistenviroment.ContentRootPath, "UploadCifrados");
                }
                string filepath = Path.Combine(uploadsFolder, file.FileName);
                if (!System.IO.File.Exists(filepath))
                {
                    using (var INeadLearn = new FileStream(filepath, FileMode.CreateNew))
                    {
                        file.CopyTo(INeadLearn);
                    }
                }
                ccc = System.IO.File.ReadAllText(filepath);
                aCifrar = ccc;

            }
            return aCifrar;
        }


    }
}
