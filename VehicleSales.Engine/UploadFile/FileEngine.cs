
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using VehicleSales.Model.Base.Exception;
using VehicleSales.Model.Interfaces.Engine.File;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Engine.UploadFile
{
    public class FileEngine: IFileEngine
    {
        private readonly IVehicleSalesRepo _vehicleSalesRepo;
        private IFormFile _file;
        private string _fullPath;

        public FileEngine(IVehicleSalesRepo vehicleSalesRepo)
        {
            _vehicleSalesRepo = vehicleSalesRepo;
        }

        public void SetParameters(IFormFile file)
        {
            _file = file;
            Upload();
        }

        public void ProcessFile()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            bool isHeader = true;

            List<VehicleSale> vehicleSales = new();

            using (var stream = System.IO.File.Open(_fullPath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    using var reader = ExcelReaderFactory.CreateCsvReader(stream);
                    while (reader.Read())
                    {
                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        };

                        var vehicleSale = new VehicleSale
                        {
                            DealNumber = int.Parse(reader.GetValue(0).ToString()),
                            CustomerName = reader.GetValue(1).ToString(),
                            DealershipName = reader.GetValue(2).ToString(),
                            Vehicle = reader.GetValue(3).ToString(),
                            Price = decimal.Parse(reader.GetValue(4).ToString()),
                            Date = System.DateTime.Parse(reader.GetValue(5).ToString())
                        };

                        if (ValidateVehicleSaleExists(vehicleSale.DealNumber))
                            continue;

                        vehicleSales.Add(vehicleSale);

                    }
                }
                catch (System.Exception ex)
                {
                    throw new EntityException(ex.Message, ex);
                }
                
            }

            _vehicleSalesRepo.AddRange(vehicleSales);

        }

        private void Upload()
        {
            var folderName = Path.Combine("Resources", "Files");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileName = ContentDispositionHeaderValue.Parse(_file.ContentDisposition).FileName.Trim('"');
            _fullPath = Path.Combine(pathToSave, fileName);

            try
            {
                using var stream = new FileStream(_fullPath, FileMode.Create);
                _file.CopyTo(stream);
            }
            catch (System.Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
            
        }

        private bool ValidateVehicleSaleExists(int dealNumber)
        {
            var vehicleSale = _vehicleSalesRepo.FindBy(x => x.DealNumber == dealNumber);

            if (vehicleSale != null)
                return true;

            return false;
        }
    }
}
