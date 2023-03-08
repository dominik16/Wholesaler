using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wholesaler.Data;

namespace Wholesaler.IntegrationTests
{
    public class ManageUserControllersTests
    {
        private HttpClient _client;

        public ManageUserControllersTests()
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
        public async Task GetUnauthorizedStatus_WithUri_ReturnUnauthorizedResult()
        {
            //act
            var response = await _client.GetAsync("/api/v1/users");

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
