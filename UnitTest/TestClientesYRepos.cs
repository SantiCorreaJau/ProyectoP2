using NUnit.Framework;
using Proyecto;
using DefaultNamespace;
using System.Collections.Generic;

namespace TestsProyecto
{
    [TestFixture]
    public class TestClientesYRepos
    {
        [Test]
        public void CrearYModificarCliente_DeberiaActualizarDatos()
        {
            var cliente = new Cliente("1", "Juan", "Pérez", "099123", "juan@mail.com", "M", "01-01-00", "10-10-25");
            cliente.Actualizar(nombre: "Carlos", telefono: "098765");

            Assert.AreEqual("Carlos", cliente.nombre);
            Assert.AreEqual("098765", cliente.telefono);
        }

        [Test]
        public void RepositorioClientes_CrearYBuscarCliente()
        {
            var repo = new RepositorioClientes();
            repo.crearCliente("1", "Ana", "Lopez", "099", "ana@mail.com", "F", "02-02-00", "20-10-25", "1");

            var buscado = repo.buscarCliente("Ana");
            Assert.NotNull(buscado);
            Assert.AreEqual("Ana", buscado.nombre);
        }

        [Test]
        public void RepositorioClientes_ModificarYEliminarCliente()
        {
            var repo = new RepositorioClientes();
            repo.crearCliente("1", "Pepe", "Ramos", "123", "p@mail.com", "M", "01-01-01", "10-10-25", "v1");

            repo.modificarCliente("1", nombre: "Pedro");
            Assert.AreEqual("Pedro", repo.buscarCliente("1").nombre);

            repo.eliminarCliente("1");
            Assert.IsNull(repo.buscarCliente("1"));
        }

        [Test]
        public void RepositorioEtiquetas_CrearBuscarYEliminar()
        {
            var repo = new RepositorioEtiquetas();
            repo.crearEtiqueta("e1", "VIP", "Cliente especial");
            var etiqueta = repo.buscarPorId("e1");

            Assert.NotNull(etiqueta);
            Assert.AreEqual("VIP", etiqueta.nombre);

            repo.RenombrarEtiqueta("e1", "PREMIUM");
            Assert.AreEqual("PREMIUM", etiqueta.nombre);

            repo.EliminarEtiqueta("e1");
            Assert.IsNull(repo.buscarPorId("e1"));
        }
    }
}
