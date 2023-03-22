using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;
using Wholesaler.Services;

namespace Wholesaler.IntegrationTests
{
    public class AuthControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        private Mock<IAuthService> _accountServiceMock = new Mock<IAuthService>();

        public AuthControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<IAuthService>(_accountServiceMock.Object);

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("WholesalerDb"));
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task AddUser_WithValidModel_ReturnOkStatus()
        {
            var model = new CreateUserDto()
            {
                Email = "test@test.com",
                Password = "Test Password",
                ConfirmPassword = "Test Password"
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/auth/register", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AddUser_WithWrongModel_ReturnBadRequest()
        {
            var model = new CreateUserDto()
            {
                Email = "It's not an email",
                Password = "Test Password",
                ConfirmPassword = "Test Password"
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/auth/register", httpContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_ForRegisteredUser_ReturnsOk()
        {
            // arrange
            _accountServiceMock.Setup(e => e.CheckUserExists(It.IsAny<LoginUserDto>())).Returns(Task.FromResult(true));
            _accountServiceMock.Setup(e => e.GenerateJwt(It.IsAny<LoginUserDto>())).Returns(Task.FromResult("jwt"));


            var login = new LoginUserDto()
            {
                Email = "test@test.com",
                Password = "password"
            };


            var jsonModel = JsonConvert.SerializeObject(login);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _client.PostAsync("/api/v1/auth/login", httpContent);

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Login_ForNonRegisteredUser_ReturnsOk()
        {
            // arrange
            _accountServiceMock.Setup(e => e.GenerateJwt(It.IsAny<LoginUserDto>())).Returns(Task.FromResult("jwt"));

            var login = new LoginUserDto()
            {
                Email = "test@test.com",
                Password = "password"
            };

            var jsonModel = JsonConvert.SerializeObject(login);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _client.PostAsync("/api/v1/auth/login", httpContent);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
