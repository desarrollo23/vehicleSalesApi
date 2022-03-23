using System;
using Microsoft.AspNetCore.Http;
using VehicleSales.Common.Response;

namespace VehicleSales.Model.Interfaces.Engine.File
{
    public interface IFileEngine
    {
        EntityResponse ProcessFile();
        void SetParameters(IFormFile file);
    }
}
