using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq.AutoMock;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Predikt.Contract;
using Predikt.Contract.Model;
using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Predikt.Api.Web.IntegrationTests
{
    public class LeaguesController_IntegrationTests
    {
        private readonly TestServer _server = null;
        private readonly HttpClient _client = null;
        private readonly AutoMocker _autoMocker = null;
        private readonly IFixture _modelMocker = null;

        public LeaguesController_IntegrationTests()
        {
            _modelMocker = new Fixture();
            _autoMocker = new AutoMocker();
            _server = new TestServer(new WebHostBuilder()
                .ConfigureServices(ConfigureServices)
                .UseStartup<Startup>());
            _client = _server.CreateClient();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.RemoveAll<IRepository>();
            services.AddScoped<IRepository>(_ => _autoMocker.GetMock<IRepository>().Object);
        }

        [Fact]
        public async void GetAllLeagues_ShouldSucceed()
        {
            var data = _modelMocker.CreateMany<League>();
            _autoMocker.Setup<IRepository>(x => x.GetAllLeagues()).Returns(Task.FromResult(data));
            var response = await _client.GetAsync("/Leagues");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            responseContent.ShouldNotBeNull();
            responseContent.ToLower().ShouldBe(JsonConvert.SerializeObject(data).ToLower());
        }
    }
}
