﻿using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;

namespace Wholesaler.IntegrationTests
{
    public class AuthControllerTests
    {
        private HttpClient _client;

        public AuthControllerTests()
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
        public async Task AddUser_WithValidModel_ReturnOkStatus()
        {
            var model = new CreateUserDto()
            {
                Email = "test@test.com",
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                Password = "Test Password",
                ConfirmPassword = "Test Password"
            };

            var jsonModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/auth/register", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
