using System;
using System.Collections.Generic;
using VehicleSales.Model.Sales;

namespace VehicleSales.Model.Interfaces.Engine.VehicleSales
{
    public interface IVehicleSalesEngine
    {
        void Create(VehicleSale request);
        List<VehicleSale> GetVehicleSales();
    }
}
