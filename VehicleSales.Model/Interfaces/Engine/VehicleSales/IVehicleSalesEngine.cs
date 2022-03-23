using System;
using System.Collections.Generic;
using VehicleSales.Common.Response;
using VehicleSales.Model.Sales;

namespace VehicleSales.Model.Interfaces.Engine.VehicleSales
{
    public interface IVehicleSalesEngine
    {
        EntityResponse Create(VehicleSale request);
        EntityResponse GetVehicleSales();
    }
}
