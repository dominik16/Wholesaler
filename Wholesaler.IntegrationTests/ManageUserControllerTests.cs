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
    public class ManageUserControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public ManageUserControllerTests()
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
        public async Task GetAllUsers_WithNoParameters_ReturnOkStatus()
        {
            //act
            var response = await _client.GetAsync("/api/v1/users");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ForNonExistingUser_ReturnNotFound()
        {
            //act 
            var response = await _client.DeleteAsync("/api/v1/users/999");

            //assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ForExistingUser_ReturnOkStatus()
        {
            //arrange
            var user = new User()
            {
                Id = 1,
                Email = "test@test.com"
            };

            // seed
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            //act 
            var response = await _client.DeleteAsync("/api/v1/users/" + user.Id);

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
