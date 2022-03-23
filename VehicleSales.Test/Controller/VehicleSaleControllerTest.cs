using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleSales.Common.Response;
using VehicleSales.Model.Interfaces.Engine.VehicleSales;
using VehicleSales.Model.Sales;
using VehicleSalesApi.Controllers;

namespace VehicleSales.Test.Controller
{
    public class VehicleSaleControllerTest
    {
        private Mock<IVehicleSalesEngine> _vehicleSalesEngineMock;
        private VehicleSaleController _vehicleSaleController;
        private EntityResponse _entityResponse;
        private List<VehicleSale> _vehicleSalesList;

        public VehicleSaleControllerTest()
        {
            Reset();
        }

        public void Reset()
        {
            _vehicleSalesEngineMock = new Mock<IVehicleSalesEngine>();
            _vehicleSalesList = new List<VehicleSale>();
            _vehicleSaleController = new VehicleSaleController(_vehicleSalesEngineMock.Object);
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

            _entityResponse = EntityResponse.Create(System.Net.HttpStatusCode.OK, _vehicleSalesList);
        }

        [Test]
        public void Get_VehicleSales_ReturnOk()
        {
            _vehicleSalesEngineMock.Setup(x => x.GetVehicleSales()).Returns(_entityResponse).Verifiable();
            ActionResult<EntityResponse> result =_vehicleSaleController.GetSales();

            EntityResponse response = result.Value;

            _vehicleSalesEngineMock.Verify(x => x.GetVehicleSales(), Times.Once);

            Assert.IsTrue(response.StatusCode == 200);
            Assert.AreEqual(_entityResponse, response);
        }
    }
}
