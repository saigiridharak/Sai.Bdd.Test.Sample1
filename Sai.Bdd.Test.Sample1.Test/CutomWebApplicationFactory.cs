using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.IO;
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
			builder.ConfigureTestServices(services =>
			{
				// This mock if for two interfaces
				var mockUserService = new UserExternalRepoClientMock();

				// 1. Mock for Refit client
				services.AddTransient<IUserExternalRepoClient, UserExternalRepoClientMock>(s => {
					return mockUserService;
				});

				// 2. Mock for injecting / setting test data
				services.AddTransient<IUserExternalRepoMockClient, UserExternalRepoClientMock>(s => {
					return mockUserService;
				});

			});

			var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
			builder.ConfigureAppConfiguration(c => c.AddJsonFile(configPath));
		}
	}
}
