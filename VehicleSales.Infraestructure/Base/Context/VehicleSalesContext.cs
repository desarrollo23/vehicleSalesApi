using System;
using Microsoft.EntityFrameworkCore;
using VehicleSales.Model.Sales;

namespace VehicleSales.Infraestructure.Base.Context
{
    public class VehicleSalesContext: DbContext
    {
        public VehicleSalesContext(DbContextOptions<VehicleSalesContext> options)
            : base(options)
        {

        }

        public DbSet<VehicleSale> VehicleSales { get; set; }
    }
}
