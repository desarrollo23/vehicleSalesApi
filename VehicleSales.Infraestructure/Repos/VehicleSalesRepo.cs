using System;
using VehicleSales.Infraestructure.Base.Context;
using VehicleSales.Infraestructure.Base.Repository;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Infraestructure.Repos
{
    public class VehicleSalesRepo: Repository<VehicleSale>, IVehicleSalesRepo
    {
        public VehicleSalesRepo(VehicleSalesContext vehicleSalesContext): base(vehicleSalesContext)
        {
        }
    }
}
