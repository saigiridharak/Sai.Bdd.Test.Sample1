using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using Sai.Bdd.Test.Sample1.Test.Mocks;
using Sai.Bdd.Test.Sample1.Models;
using Newtonsoft.Json;
using Refit;
using System.Net.Http;

namespace Sai.Bdd.Test.Sample1.Test.Tests
{
    public class UserControllerTests : IClassFixture<CutomWebApplicationFactory>
    {
        private readonly CutomWebApplicationFactory _factory;
        public UserControllerTests(CutomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        #region Tests

        [Theory]
        [MemberData(nameof(PositiveTestData))]
        public async Task Given_All_Dependencies_Work_Correctly_When_Controller_Calls_GetUsers_Then_Controller_Returns_200
            (int total, User user)
        {
            // Arrange
            var sut = _factory.CreateClient();
            var yy = _factory.Services.GetService(typeof(IUserExternalRepoMockClient));
            ((IUserExternalRepoMockClient)yy).Total = total;
            ((IUserExternalRepoMockClient)yy).User = user;

            // Act
            var response = await sut.GetAsync("api/v1/users");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNull();
            var users = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
            users.Should().NotBeEmpty();
            users[0].FirstName.Should().Be(user.FirstName);
        }

        [Theory]
        [MemberData(nameof(NegativeTestData))]
        public async Task Given_GetUsers_Return_ApiException_When_Controller_Calls_GetUsers_Then_Controller_Returns_500
            (Task<ApiException> exception)
        {
            // Arrange
            var sut = _factory.CreateClient();
            var yy = _factory.Services.GetService(typeof(IUserExternalRepoMockClient));
            ((IUserExternalRepoMockClient)yy).Error = exception;

            // Act
            var response = await sut.GetAsync("api/v1/users");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion


        #region Test data
        public static IEnumerable<object[]> NegativeTestData()
        {
            return new List<object[]>
            {
                new object[] {ApiException.Create(
                    new HttpRequestMessage(), 
                    HttpMethod.Get,
                    new HttpResponseMessage(HttpStatusCode.BadRequest), null, null)},
                // Add more data to cover all possible negative scenarios such as 404, 5xx etc.
            };
        }

        public static IEnumerable<object[]> PositiveTestData()
        {
            return new List<object[]>
            {
                new object[] {10, new User {FirstName = "John" } },
                new object[] {20, new User {FirstName = "Mike" } },
                // Add more data to cover all possible positive scenarios such as 204, 206 etc.
            };
        }

        #endregion
    }
}
