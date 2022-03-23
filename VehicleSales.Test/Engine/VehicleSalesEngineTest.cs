using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleSales.Engine.VehicleSales;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Test.Engine
{
    public class VehicleSalesEngineTest
    {
        private IVehicleSalesEngine _vehicleSalesEngine;
        private Mock<IVehicleSalesRepo> _vehicleSalesRepoMock;
        private List<VehicleSale> _vehicleSalesList;

        public VehicleSalesEngineTest()
        {
            Reset();
        }

        public void Reset()
        {
            _vehicleSalesList = new List<VehicleSale>();
            _vehicleSalesRepoMock = new Mock<IVehicleSalesRepo>();
            _vehicleSalesEngine = new VehicleSalesEngine(_vehicleSalesRepoMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            Reset();

            _vehicleSalesList.Add(new VehicleSale
            {
                DealNumber = 5962,
                CustomerName = "Adam",
                DealershipName = "Tom",
                Vehicle = "Ford fiesta",
                Price = 598,
                Date = DateTime.Now
            });
            _vehicleSalesList.Add(new VehicleSale
            {
                DealNumber = 8741,
                CustomerName = "George",
                DealershipName = "Mat",
                Vehicle = "Mazda 3",
                Price = 600,
                Date = DateTime.Now
            });
            _vehicleSalesList.Add(new VehicleSale
            {
                DealNumber = 1596,
                CustomerName = "Andrew",
                DealershipName = "Allison",
                Vehicle = "Toyota TXL",
                Price = 1200,
                Date = DateTime.Now
            });
        }

        [Test]
        public void Get_VehicleSalesList_ReturnList()
        {
            _vehicleSalesRepoMock.Setup(x => x.FindAll()).Returns(_vehicleSalesList);

            var response = _vehicleSalesEngine.GetVehicleSales();

            Assert.AreEqual(_vehicleSalesList, response.Result);
        }

        [Test]
        public void Get_VehicleSalesList_ReturnEmptyList()
        {
            _vehicleSalesList = new List<VehicleSale>();

            _vehicleSalesRepoMock.Setup(x => x.FindAll()).Returns(_vehicleSalesList);

            var response = _vehicleSalesEngine.GetVehicleSales();

            Assert.AreEqual(_vehicleSalesList, response.Result);
        }
    }
}
