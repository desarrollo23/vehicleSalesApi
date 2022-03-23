using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleSales.Engine.UploadFile;
using VehicleSales.Model.Interfaces.Engine.File;
using VehicleSales.Model.Interfaces.Repos.VehicleSales;
using VehicleSales.Model.Sales;

namespace VehicleSales.Test.Engine
{
    public class FileEngineTest
    {
        private Mock<IVehicleSalesRepo> _vehicleSalesRepoMock;
        private IFileEngine _fileEngine;
        private Mock<IFormFile> _formFileMock;
        private int _dealNumber;
        private VehicleSale _vehicleSale;
        private List<VehicleSale> _vehicleSalesList;


        public FileEngineTest()
        {
            Reset();
        }

        public void Reset()
        {
            _vehicleSalesRepoMock = new Mock<IVehicleSalesRepo>();
            _formFileMock = new Mock<IFormFile>();
            _vehicleSale = new VehicleSale();
            _vehicleSalesList = new List<VehicleSale>();
            _fileEngine = new FileEngine(_vehicleSalesRepoMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            Reset();

            _dealNumber = 8965;

            _vehicleSale = new VehicleSale
            {
                DealNumber = _dealNumber,
                CustomerName = "Tom",
                DealershipName = "Sophia",
                Price = 410,
                Vehicle = "Nissan march",
                Date = DateTime.Now
            };

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

            #region Mock file
            _formFileMock.Setup(_ => _.ContentDisposition).Returns("form-data; name='file'; filename='Dealertrack-CSV-Example.csv'");
            #endregion
        }

        [Test]
        public void ProccessFile_ReturnOK()
        {
            _vehicleSale = null;
            _vehicleSalesRepoMock.Setup(x => x.GetByDealNumber(_dealNumber)).Returns(_vehicleSale);
            _vehicleSalesRepoMock.Setup(x => x.AddRange(It.IsAny<List<VehicleSale>>())).Verifiable();

            _fileEngine.SetParameters(_formFileMock.Object);
            var response = _fileEngine.ProcessFile();

            _vehicleSalesRepoMock.Verify(x => x.AddRange(It.IsAny<List<VehicleSale>>()), Times.Once);

            Assert.IsTrue(response.StatusCode == 201);
        }

        [Test]
        public void ProccessFile_ReturnInternalError_WhenTrySaveFile()
        {
            var exception = new Exception("An Exception has ocurred");
            var _fullPath = "C:/Users/Andres/source/repos/vehicleSalesApi/VehicleSalesApi/Resources/Files/Dealertrack-CSV-Example.csv";
            using var fileStream = new FileStream(_fullPath, FileMode.Create);
            _formFileMock.Setup(x => x.CopyTo(It.IsAny<Stream>())).Throws(exception);

            try
            {
                _fileEngine.SetParameters(_formFileMock.Object);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exception.Message, ex.Message);
            }
        }
    }
}
