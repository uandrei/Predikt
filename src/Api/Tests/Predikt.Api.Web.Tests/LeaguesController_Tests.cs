using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Predikt.Api.Web.Controllers;
using Predikt.Contract;
using Predikt.Contract.Model;
using Shouldly;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Predikt.Api.Web.Tests
{
    public class LeaguesController_Tests
    {
        private readonly AutoMocker _autoMocker = null;
        private readonly LeaguesController _sutController = null;
        private readonly IFixture _modelMocker = null;

        public LeaguesController_Tests()
        {
            _modelMocker = new Fixture();
            _autoMocker = new AutoMocker();
            _sutController = _autoMocker.CreateInstance<LeaguesController>();
        }

        [Fact]
        public void GetAll_ShouldSucceed()
        {
            var expectedOutput = _modelMocker.CreateMany<League>();
            _autoMocker.GetMock<IRepository>().Setup(x => x.GetAllLeagues()).Returns(Task.FromResult(expectedOutput));

            var output = _sutController.Get().Result as OkObjectResult;

            output.ShouldNotBeNull();
            output.StatusCode.ShouldNotBeNull();
            output.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            JsonConvert.SerializeObject(output.Value).ShouldBe(JsonConvert.SerializeObject(expectedOutput));
        }
    }
}
