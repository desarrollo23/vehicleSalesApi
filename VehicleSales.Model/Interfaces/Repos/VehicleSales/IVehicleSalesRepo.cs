using System;
using VehicleSales.Model.Base.Repository;
using VehicleSales.Model.Sales;

namespace VehicleSales.Model.Interfaces.Repos.VehicleSales
{
    public interface IVehicleSalesRepo: IRepository<VehicleSale>
    {
        VehicleSale GetByDealNumber(int dealNumber);
    }
}
