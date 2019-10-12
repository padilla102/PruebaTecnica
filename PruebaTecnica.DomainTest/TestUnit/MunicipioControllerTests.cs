using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using PruebaTecnica.WebUI.Controllers;
using PruebaTecnica.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PruebaTecnica.DomainTest.TestUnit
{
    public class MunicipioControllerTests
    {
        [Fact]
        public void Puede_Paginar()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1, RegionId=1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1, RegionId=1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1, RegionId=1},
                new Municipio{Id=4, Name = "Municipio4",StatusId = 1, RegionId=1},
                new Municipio{Id=5, Name = "Municipio5",StatusId = 1, RegionId=1}
            }).AsQueryable<Municipio>());

            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();

            MunicipioController controller = new MunicipioController(mock1.Object,mock2.Object, mock3.Object);

            controller.PageSize = 3;

            // prueba
            IEnumerable<Municipio> result =
            controller.List(2) as IEnumerable<Municipio>;
            // Verificación
            Municipio[] muniArray = result.ToArray();
            Assert.True(muniArray.Length == 2);
            Assert.Equals("Municipio1", muniArray[0].Name);
            Assert.Equals("Municipio2", muniArray[1].Name);

        }


        [Fact]
        public void Edit_RotornaUsuario()
        {
            // preparación
            var idMunicipio = 1;
            var autorMock = new Municipio { Id = 1, Name = "Municipio1", StatusId = 1, RegionId = 1 };
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(x => x.Get(idMunicipio)).Returns(autorMock);
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();
            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();
            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object, mock3.Object);

            // prueba
            var resultado = GetViewModel<Municipio>(target.Edit(idMunicipio));

            // Verificación
            Assert.IsNotNull(resultado.Id);
            Assert.AreEqual(resultado.Id, autorMock.Id);
            Assert.AreEqual(resultado.Name, autorMock.Name);
        }

        [Fact]
        public void Puede_Edit_Municipio()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1, RegionId=1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1, RegionId=1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1, RegionId=1},
                new Municipio{Id=4, Name = "Municipio4",StatusId = 1, RegionId=1},
                new Municipio{Id=5, Name = "Municipio5",StatusId = 1, RegionId=1}
            }).AsQueryable<Municipio>());
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();
            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();
            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object, mock3.Object);

            // prueba
            Municipio m1 = GetViewModel<Municipio>(target.Edit(1));
            Municipio m2 = GetViewModel<Municipio>(target.Edit(2));
            Municipio m3 = GetViewModel<Municipio>(target.Edit(3));
            // Verificación
            Assert.Equals(1, m1.Id);
            Assert.Equals(3, m3.Id);
            Assert.Equals(2, m2.Id);
        }

        [Fact]
        public void NoPuede_Editar_Nonexist_Municipio()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1, RegionId=1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1, RegionId=1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1, RegionId=1}
            }).AsQueryable<Municipio>());
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();
            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();
            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object, mock3.Object);
            // prueba
            Municipio result = GetViewModel<Municipio>(target.Edit(4));
            // Verificación
            Assert.Null(result);
        }

        [Fact]
        public void Puede_Guardar_Validar_Cambios()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();
            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object, mock3.Object)
            {
                TempData = tempData.Object
            };

            // prueba
            MunicipioViewModel vm = new MunicipioViewModel { Municipio = new Municipio { Name = "Test" }};
            var result = target.Edit(vm);
            mock1.Verify(m => m.Save(vm.Municipio));
            // Verificación
            Assert.Equals("List", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            // preparación
            var autorMock = new Municipio { Id = 1, Name = "Municipio1", StatusId = 1, RegionId = 1 };
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1, RegionId=1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1, RegionId=1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1, RegionId=1}
            }).AsQueryable<Municipio>());
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();
            Mock<IRegionRepository> mock3 = new Mock<IRegionRepository>();
            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object, mock3.Object);
            // prueba
            int Id = 1;
            target.Delete(Id);
            // Verificación
            mock1.Verify(m => m.Delete(Id));
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
