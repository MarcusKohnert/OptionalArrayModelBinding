using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using OptionalArrayModelBinding;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace OptionalArrayModelBindingTest
{
    public class TestOptionalArrayCustomModelBinder
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public TestOptionalArrayCustomModelBinder()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            client = server.CreateClient();
        }

        [Fact]
        public async Task SuccessWithoutProvidingIds()
        {
            var response = await client.GetAsync("/api/values");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SuccessWithValidIds()
        {
            var response = await client.GetAsync("/api/values?ids=aaa001&ids=bbb002");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task FailureWithOneInvalidId()
        {
            var response = await client.GetAsync("/api/values?ids=xaaa001&ids=bbb002");

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}