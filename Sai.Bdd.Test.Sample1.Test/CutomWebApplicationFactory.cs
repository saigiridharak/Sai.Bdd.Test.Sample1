using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sai.Bdd.Test.Sample1.Services;
using Sai.Bdd.Test.Sample1.Test.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace Sai.Bdd.Test.Sample1.Test
{
    public class CutomWebApplicationFactory : WebApplicationFactory<Startup>
    {
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			Console.WriteLine("Begin ConfigureWebHost");
			builder.ConfigureTestServices(services =>
			{
				//services.AddAuthentication("Test")
				//	.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
				//		"Test", options => { });

				var mock1 = new UserExternalRepoClientMock();

				services.AddTransient<IUserExternalRepoClient, UserExternalRepoClientMock>(s => {
					return mock1;
				});

				services.AddTransient<IUserExternalRepoMockClient, UserExternalRepoClientMock>(s => {
					return mock1;
				});

			});

			var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
			builder.ConfigureAppConfiguration(c => c.AddJsonFile(configPath));

			Console.WriteLine("End ConfigureWebHost");

		}
	}
}
