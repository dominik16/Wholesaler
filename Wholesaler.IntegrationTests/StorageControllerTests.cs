using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.IntegrationTests
{
    public class StorageControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public StorageControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.AddMvc(option => option.Filters.Add(new FakeuserFilter()));

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("WholesalerDb"));
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAllStorages_WithNoParameters_ReturnOkStatus()
        {
            //act
            var response = await _client.GetAsync("/api/v1/storages");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddStorage_WithValidModel_ReturnOkStatus()
        {
            var model = new CreateStorageDto()
            {
                Name= "Test Storage",
                Address = "Test Address",
                City = "Test City",
                Type = "Test Type"
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/storages", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteStorage_ForNonExistingStorage_ReturnNotFound()
        {
            //act 
            var response = await _client.DeleteAsync("/api/v1/storages/999");

            //assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteStorage_ForExistingStorage_ReturnOkStatus()
        {
            //arrange
            var storage = new Storage()
            {
                Id = 1,
                Name = "Test Storage"
            };

            // seed
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Storages.Add(storage);
            _dbContext.SaveChanges();

            //act 
            var response = await _client.DeleteAsync("/api/v1/storages/" + storage.Id);

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
