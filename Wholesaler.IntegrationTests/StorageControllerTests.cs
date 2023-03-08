using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Wholesaler.IntegrationTests
{
    public class StorageControllerTests
    {
        private HttpClient _client;

        public StorageControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUnauthorizedStatus_WithUri_ReturnUnauthorizedResult()
        {
            //act
            var response = await _client.GetAsync("/api/v1/storages");

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
