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
    public class ProductControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public ProductControllerTests()
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

        [Theory]
        [InlineData("products?pagesize=3&pagenumber=1")]
        [InlineData("products?pagesize=5&pagenumber=1")]
        [InlineData("products?pagesize=10&pagenumber=1")]
        public async Task GetUnauthorizedStatus_WithUri_ReturnOkStatus(string url)
        {
            //act
            var response = await _client.GetAsync("/api/v1/" + url);

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddStorage_WithValidModel_ReturnOkStatus()
        {
            var model = new CreateProductDto()
            {
                Name = "Test Storage",
                Description = "Test Description",
                Unit = "Test Unit",
                Price = 10,
                StorageId = 1
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/storages", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ForNonExistingProduct_ReturnNotFound()
        {
            //act 
            var response = await _client.DeleteAsync("/api/v1/products/999");

            //assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ForExistingProduct_ReturnOkStatus()
        {
            //arrange
            var product = new Product()
            {
                Id = 1,
                Name = "Test Product"
            };

            // seed
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            //act 
            var response = await _client.DeleteAsync("/api/v1/products/" + product.Id);

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
