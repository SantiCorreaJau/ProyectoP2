using NUnit.Framework;
using Proyecto;
using DefaultNamespace;
using System.Collections.Generic;

namespace TestsProyecto
{
    [TestFixture]
    public class TestVendedorYExtras
    {
        [Test]
        public void Comentario_EditarYBorrar()
        {
            var c = new Comentario("1", "Texto original", "20-10-25", "v1", "i1", true);
            c.EditarTexto("Nuevo texto");
            Assert.AreEqual("Nuevo texto", c.texto);

            c.Borrar();
            Assert.IsFalse(c.visible);
        }

        [Test]
        public void Vendedor_VerNoLeidosYAgregarComentario()
        {
            var vendedor = new Vendedor("v1", "Juan", "P", "099", "v@mail");
            var inter = new Mensaje("i1", "20-10-25", "Tema", "no leido", "c1", "v1", new List<Comentario>(), "recibido", "txt", "sms");
            var repo = new RepositorioInteracciones();
            repo.Agregar(inter);

            var noLeidos = vendedor.verNoLeidos(repo);
            Assert.AreEqual(1, noLeidos.Count);

            vendedor.agregarComentario("i1", "Buen trabajo", repo);
            Assert.AreEqual(1, inter.comentarios.Count);
        }

        [Test]
        public void Vendedor_AgregarEtiquetaACliente()
        {
            var vendedor = new Vendedor("v1", "Juan", "P", "099", "v@mail");
            var repoC = new RepositorioClientes();
            var repoE = new RepositorioEtiquetas();

            var cliente = new Cliente("c1", "Pedro", "A", "123", "p@mail", "M", "01-01-01", "10-10-25");
            var etiqueta = new Etiqueta("e1", "VIP", "Cliente importante");
            repoC.RepoClientes.Add(cliente);
            repoE.RepoEtiquetas.Add(etiqueta);

            vendedor.agregarEtiquetaACliente("c1", "e1", repoC, repoE);

            Assert.Contains("e1", cliente.etiquetasIds);
        }

        [Test]
        public void ProductoYCotizacion_DeberianCrearseCorrectamente()
        {
            var p = new Producto("p1", "Mouse", "sku1", "Inalambrico");
            var c = new Cotizacion("c1", "20-10-25", 100.5f, "activa");

            Assert.AreEqual("Mouse", p.Nombre);
            Assert.AreEqual(100.5f, c.Importe);
        }

        [Test]
        public void VentaYItem_DeberianCrearseCorrectamente()
        {
            var v = new Venta("v1", "20-10-25", 15);
            var item = new VentaItem("vi1", 2, 25);

            Assert.AreEqual(2, item.cantidad);
            Assert.AreEqual(50, item.subtotal);
            Assert.AreEqual(15, v.total);
        }
    }
}
