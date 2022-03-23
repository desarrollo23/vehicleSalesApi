using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleSales.Common.Response;
using VehicleSales.Model.Interfaces.Engine.File;

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
        public ActionResult<EntityResponse> UploadFile()
        {
            var formCollection = Request.ReadFormAsync().Result;
            var file = formCollection.Files.First();

            if (file.Length > 0)
            {
                _uploadFileEngine.SetParameters(file);
                var response =_uploadFileEngine.ProcessFile();

                return Ok(response);
            }
            else
                return BadRequest();
        }
    }
}
