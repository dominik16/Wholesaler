using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wholesaler.Data;

namespace Wholesaler.IntegrationTests.NonAuth
{
    public class ProductControllerTestsNonAuth
    {
        private HttpClient _client;

        public ProductControllerTestsNonAuth()
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

        [Theory]
        [InlineData("products?pagesize=3&pagenumber=1")]
        [InlineData("products?pagesize=5&pagenumber=1")]
        [InlineData("products?pagesize=10&pagenumber=1")]
        public async Task GetUnauthorizedStatus_WithUri_ReturnUnauthorizedResult(string url)
        {
            //act
            var response = await _client.GetAsync("/api/v1/" + url);

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}