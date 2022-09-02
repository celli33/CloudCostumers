using CloudCostumers.API.Config;
using CloudCostumers.API.Controllers.Services;
using CloudCostumers.API.Models;
using CloudCostumers.UnitTests.Fixtures;
using CloudCostumers.UnitTests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCostumers.UnitTests.Systems.Services;
public class TestUserService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpRequest()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        await sut.GetAllUsers();

        handlerMock.Protected().Verify(
            "SendAsync", 
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
        );

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = "https://example.com/users"
        });
        var sut = new UserService(httpClient, config);

        var result = await sut.GetAllUsers();

        result.Should().BeOfType<List<User>>();
        result.Count().Should().Be(3);

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
        );

    }

    public async Task GetAllUsers_WhenCalled_ReturnsEmpty()
    {
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = "https://example.com/users"
        });
        var sut = new UserService(httpClient, config);

        var result = await sut.GetAllUsers();

        result.Count().Should().Be(0);  

    }

    public async Task GetAllUsers_WhenCalled_InvocesConfiguredExternalUrl()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = "https://example.com/users"
        });
        var sut = new UserService(httpClient, config);

        var uri = new Uri(endpoint);

       
        var result = await sut.GetAllUsers();

        handlerMock.Protected().Verify(
           "SendAsync",
           Times.Exactly(1),
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
           ItExpr.IsAny<CancellationToken>()
       );

    }
}

