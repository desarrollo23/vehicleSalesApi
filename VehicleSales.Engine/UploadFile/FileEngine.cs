
using VehicleSales.Model.Interfaces.Engine.File;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;

namespace VehicleSales.Engine.UploadFile
{
    public class FileEngine: IFileEngine
    {
        private readonly IVehicleSalesRepo _vehicleSalesRepo;

        public FileEngine(IVehicleSalesRepo vehicleSalesRepo)
        {
            _vehicleSalesRepo = vehicleSalesRepo;
        }

        public void Upload()
        {
            // Logic here
        }
    }
}
