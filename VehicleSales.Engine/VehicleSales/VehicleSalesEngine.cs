using System;
using System.Linq;
using System.Collections.Generic;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;
using VehicleSales.Common.Response;

namespace VehicleSales.Engine.VehicleSales
{
    public class VehicleSalesEngine: IVehicleSalesEngine
    {
        private readonly IVehicleSalesRepo _vehicleSalesRepo;

        public VehicleSalesEngine(IVehicleSalesRepo vehicleSalesRepo)
        {
            _vehicleSalesRepo = vehicleSalesRepo;
        }

        public EntityResponse Create(VehicleSale request)
        {
            _vehicleSalesRepo.Add(request);
            return EntityResponse.Create(System.Net.HttpStatusCode.OK);
        }

        public EntityResponse GetVehicleSales()
        {
            var vehicleSalesList = _vehicleSalesRepo.FindAll().ToList();
            return EntityResponse.Create(System.Net.HttpStatusCode.OK, vehicleSalesList);
        }
    }
}
