using NUnit.Framework;
using Proyecto;
using System.Collections.Generic;

namespace TestsProyecto
{
    [TestFixture]
    public class TestInteracciones
    {
        [Test]
        public void CorreoElectronico_RegistrarEnRepositorio()
        {
            var repo = new RepositorioInteracciones();
            var correo = new CorreoElectronico("1", "20-10-25", "Tema1", "pendiente", "c1", "v1", new List<Comentario>(), "enviado", "asunto", "texto");
            correo.Registrar(repo);

            Assert.AreEqual(1, repo.RepoInteracciones.Count);
        }

        [Test]
        public void MensajeYReunion_DeberianRegistrarseCorrectamente()
        {
            var repo = new RepositorioInteracciones();
            var mensaje = new Mensaje("2", "20-10-25", "hola", "pendiente", "c2", "v2", new List<Comentario>(), "enviado", "texto", "WhatsApp");
            var reunion = new Reunion("3", "21-10-25", "Reu", "confirmada", "c3", "v3", new List<Comentario>(), "Oficina");

            mensaje.Registrar(repo);
            reunion.Registrar(repo);

            Assert.AreEqual(2, repo.RepoInteracciones.Count);
        }

        [Test]
        public void Interaccion_CambiarEstadoYVerificarPendiente()
        {
            var inter = new Mensaje("4", "19-10-25", "Tema", "no leido", "c4", "v4", new List<Comentario>(), "recibido", "txt", "sms");
            inter.CambiarEstado("pendiente");
            Assert.IsTrue(inter.EsPendiente());
        }

        [Test]
        public void RepositorioInteracciones_FiltrarPorTipoClienteVendedor()
        {
            var repo = new RepositorioInteracciones();
            var i1 = new CorreoElectronico("1", "20-10-25", "A", "pendiente", "c1", "v1", new List<Comentario>(), "enviado", "a", "b");
            var i2 = new Mensaje("2", "19-10-25", "B", "leido", "c2", "v2", new List<Comentario>(), "enviado", "t", "wa");
            repo.Agregar(i1);
            repo.Agregar(i2);

            var filtrados = repo.FiltrarTipo("correo");
            Assert.AreEqual(1, filtrados.Count);

            var porCliente = repo.FiltrarCliente("c2", repo.RepoInteracciones);
            Assert.AreEqual(1, porCliente.Count);

            var porVendedor = repo.FiltrarVendedor("v1", repo.RepoInteracciones);
            Assert.AreEqual(1, porVendedor.Count);
        }

        [Test]
        public void RepositorioInteracciones_FiltrarPendienteYReciente()
        {
            var repo = new RepositorioInteracciones();
            repo.Agregar(new Mensaje("1", System.DateTime.Now.ToString("dd-MM-yy"), "T", "pendiente", "c", "v", new List<Comentario>(), "recibido", "c", "c"));

            var pendientes = repo.FiltrarPendiente(repo.RepoInteracciones);
            Assert.AreEqual(1, pendientes.Count);

            var recientes = repo.FiltrarRecientes(repo.RepoInteracciones);
            Assert.AreEqual(1, recientes.Count);
        }
    }
}
