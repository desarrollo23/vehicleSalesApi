using System;
using Microsoft.AspNetCore.Http;

namespace VehicleSales.Model.Interfaces.Engine.File
{
    public interface IFileEngine
    {
        void ProcessFile();
        void SetParameters(IFormFile file);
    }
}
