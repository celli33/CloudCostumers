using CloudCostumers.API.Controllers;
using CloudCostumers.API.Controllers.Services;
using CloudCostumers.API.Models;
using CloudCostumers.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCostumers.UnitTests.Systems.Controllers;

public class TestUsersController
{

    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        var mockUserService = new Mock<IUserService>();
        var sut = new UsersController(mockUserService.Object);
        mockUserService
           .Setup(service => service.GetAllUsers())
           .ReturnsAsync(UsersFixture.GetTestUsers());

        var result = (OkObjectResult) await sut.Get();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyeOnce()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        var result = await sut.Get();

        mockUserService.Verify(service => service.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListsOfUsers()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        var result = (OkObjectResult) await sut.Get();

        result.Should().BeOfType<OkObjectResult>();

        var objectResult = result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        var result = (NotFoundResult)await sut.Get();

        result.StatusCode.Should().Be(404);
    }
}