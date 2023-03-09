using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;

namespace Wholesaler.IntegrationTests.NonAuth
{
    public class StorageControllerNonAuth
    {
        private HttpClient _client;

        public StorageControllerNonAuth()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("WholesalerDb"));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetUnauthorizedStatus_WithUri_ReturnUnauthorizedStatus()
        {
            //act
            var response = await _client.GetAsync("/api/v1/storages");

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task AddStorage_WithValidModel_ReturnUnauthorizedStatus()
        {
            var model = new CreateStorageDto()
            {
                Name = "Test Storage",
                Address = "Test Address",
                City = "Test City",
                Type = "Test Type"
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/storages", httpContent);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
