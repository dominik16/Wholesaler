using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Wholesaler.IntegrationTests
{
    public class ProductControllerTests
    {
        private HttpClient _client;

        public ProductControllerTests() 
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
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