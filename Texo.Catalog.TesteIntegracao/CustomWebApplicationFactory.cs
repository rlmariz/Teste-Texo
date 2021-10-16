using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Texo.Catalog.TesteIntegracao
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                Console.WriteLine("Teste");


            });
            builder.UseEnvironment("Testing");
        }


        protected override IWebHostBuilder CreateWebHostBuilder() =>
            base.CreateWebHostBuilder().UseEnvironment("Testing");
    }
}
