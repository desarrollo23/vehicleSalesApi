using System;
using System.Linq;
using VehicleSales.Infraestructure.Base.Context;
using VehicleSales.Infraestructure.Base.Repository;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Infraestructure.Repos
{
    public class VehicleSalesRepo: Repository<VehicleSale>, IVehicleSalesRepo
    {
        private VehicleSalesContext _vehicleSalesContext;
        public VehicleSalesRepo(VehicleSalesContext vehicleSalesContext): base(vehicleSalesContext)
        {
            _vehicleSalesContext = vehicleSalesContext;
        }

        public VehicleSale GetByDealNumber(int dealNumber)
        {
            return _vehicleSalesContext.VehicleSales.FirstOrDefault(x => x.DealNumber == dealNumber);
        }
    }
}
