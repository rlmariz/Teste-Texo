using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Teste_Texo_Api;

namespace Texo.Catalog.TesteIntegracao
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;

        public TestContext()
        {
            SetupCliente();
        }

        private void SetupCliente()
        {
            _server = new TestServer(new WebHostBuilder()
               .ConfigureAppConfiguration((context, builder) =>
               {
                   builder.AddJsonFile("appsettings.Testing.json");
               })
               .UseStartup<Startup>());

            Client = _server.CreateClient();
        }

    }
}
