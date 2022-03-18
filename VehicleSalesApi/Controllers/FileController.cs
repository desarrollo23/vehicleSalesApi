using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleSales.Model.Interfaces.Engine.File;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleSalesApi.Controllers
{
    [Route("api/upload-file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileEngine _uploadFileEngine;

        public FileController(IFileEngine uploadFileEngine)
        {
            _uploadFileEngine = uploadFileEngine;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            _uploadFileEngine.Upload();
            return Ok();
        }
    }
}
