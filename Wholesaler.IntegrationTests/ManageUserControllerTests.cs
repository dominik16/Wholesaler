using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;

namespace Wholesaler.IntegrationTests
{
    public class ManageUserControllerTests
    {
        private HttpClient _client;

        public ManageUserControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.AddMvc(option => option.Filters.Add(new FakeuserFilter()));

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("WholesalerDb"));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetAllUsers_WithNoParameters_ReturnOkStatus()
        {
            //act
            var response = await _client.GetAsync("/api/v1/users");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
