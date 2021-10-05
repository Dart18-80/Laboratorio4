using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Route("api/cipher/zz/{Clave}")]
        [HttpPost]
        public IActionResult ZizZag([FromForm] IFormFile file, string clave)
        {
            object Lectura = Archivo(file);
            return Ok();
        }
        [Route("api/cipher/csr/{Clave}")]
        [HttpPost]
        public IActionResult Cesar([FromForm] IFormFile file, string clave)
        {
            object Lectura = Archivo(file);
            return Ok();
        }
            public object Archivo(IFormFile file)
        {
            string uploadsFolder = null;
            object aCifrar=default;
            string ccc = default;

            if (file!=null)
            {
                uploadsFolder = Path.Combine(fistenviroment.ContentRootPath, "Upload");
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
