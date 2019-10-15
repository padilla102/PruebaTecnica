using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PruebaTecnica.Data.Entities;
using PruebaTecnica.Domain.Abstract;
using PruebaTecnica.WebUI.Controllers;
using PruebaTecnica.WebUI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PruebaTecnica.DomainTest
{
    [TestClass]
    public class MunicipioControllerTests
    {
        [TestMethod]
        public void PuedePaginar()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1},
                new Municipio{Id=4, Name = "Municipio4",StatusId = 1},
                new Municipio{Id=5, Name = "Municipio5",StatusId = 1}
            }).AsQueryable<Municipio>());

            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            MunicipioController controller = new MunicipioController(mock1.Object, mock2.Object);

            controller.PageSize = 3;

            // Prueba
            //IEnumerable<Municipio> result = controller.List(2) as IEnumerable<Municipio>;

            var result = GetViewModel<MunicipioListViewModel>(controller.List(2));

            // Verificación
            Assert.IsTrue(result.Municipios.Count() == 2);

        }

        [TestMethod]
        public void EditarRotornaMunicipio()
        {
            // preparación
            var idMunicipio = 1;
            var autorMock = new Municipio { Id = 1, Name = "Municipio1", StatusId = 1};
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(x => x.Get(idMunicipio)).Returns(autorMock);
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            MunicipioController controller = new MunicipioController(mock1.Object, mock2.Object);

            // prueba
            var resultado = GetViewModel<MunicipioViewModel>(controller.Edit(idMunicipio));

            // Verificación
            Assert.IsNotNull(resultado.Municipio.Id);
            Assert.AreEqual(resultado.Municipio.Id, autorMock.Id);
            Assert.AreEqual(resultado.Municipio.Name, autorMock.Name);
        }

        [TestMethod]
        public void PuedeEditarMunicipio()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1},
                new Municipio{Id=4, Name = "Municipio4",StatusId = 1},
                new Municipio{Id=5, Name = "Municipio5",StatusId = 1}
            }).AsQueryable<Municipio>());

            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            MunicipioController controller = new MunicipioController(mock1.Object, mock2.Object);

            // prueba
            MunicipioViewModel m1 = GetViewModel<MunicipioViewModel>(controller.Edit(1));
            MunicipioViewModel m2 = GetViewModel<MunicipioViewModel>(controller.Edit(2));
            MunicipioViewModel m3 = GetViewModel<MunicipioViewModel>(controller.Edit(3));
            // Verificación
            Assert.Equals(1, m1.Municipio.Id);
            Assert.Equals(3, m3.Municipio.Id);
            Assert.Equals(2, m2.Municipio.Id);
        }

        [TestMethod]
        public void NoPuedeEditarNonexistMunicipio()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1}
            }).AsQueryable<Municipio>());
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            MunicipioController target = new MunicipioController(mock1.Object, mock2.Object);
            // prueba
            MunicipioViewModel result = GetViewModel<MunicipioViewModel>(target.Edit(4));
            // Verificación
            Assert.IsNull(result.Municipio);
        }

        [TestMethod]
        public void PuedeGuardarValidarCambios()
        {
            // preparación
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            MunicipioController controller = new MunicipioController(mock1.Object, mock2.Object)
            {
                TempData = tempData.Object
            };

            // prueba
            MunicipioViewModel vm = new MunicipioViewModel { Municipio = new Municipio { Name = "Test" } };
            var result = controller.Edit(vm);
            mock1.Verify(m => m.Save(vm.Municipio));
            // Verificación
            Assert.Equals("List", (result as RedirectToActionResult).ActionName);
        }

        [TestMethod]
        public void PuedeEliminarMunicipios()
        {
            // preparación
            var autorMock = new Municipio { Id = 1, Name = "Municipio1", StatusId = 1};
            Mock<IMunicipioRepository> mock1 = new Mock<IMunicipioRepository>();
            mock1.Setup(m => m.List()).Returns((new Municipio[]{
                new Municipio{Id=1, Name = "Municipio1",StatusId = 1},
                new Municipio{Id=2, Name = "Municipio2",StatusId = 1},
                new Municipio{Id=3, Name = "Municipio3",StatusId = 1}
            }).AsQueryable<Municipio>());
            Mock<IStatusRepository> mock2 = new Mock<IStatusRepository>();

            MunicipioController controller = new MunicipioController(mock1.Object, mock2.Object);
            // prueba
            int Id = 1;
            controller.Delete(Id);
            // Verificación
            mock1.Verify(m => m.Delete(Id));
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
