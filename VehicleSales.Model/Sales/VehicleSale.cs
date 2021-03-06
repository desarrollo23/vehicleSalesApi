using System;
using VehicleSales.Model.Base.Model;

namespace VehicleSales.Model.Sales
{
    public class VehicleSale: Entity
    {
        public int DealNumber { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
