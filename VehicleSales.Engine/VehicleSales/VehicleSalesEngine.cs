using System;
using System.Linq;
using System.Collections.Generic;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Engine.VehicleSales
{
    public class VehicleSalesEngine: IVehicleSalesEngine
    {
        private readonly IVehicleSalesRepo _vehicleSalesRepo;

        public VehicleSalesEngine(IVehicleSalesRepo vehicleSalesRepo)
        {
            _vehicleSalesRepo = vehicleSalesRepo;
        }

        public void Create(VehicleSale request)
        {
            _vehicleSalesRepo.Add(request);
        }

        public List<VehicleSale> GetVehicleSales()
        {
            return _vehicleSalesRepo.FindAll().ToList();
        }
    }
}
