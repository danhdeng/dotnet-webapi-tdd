using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using CloudCustomers.API.Controllers;
using FluentAssertions;
using Moq;
using CloudCustomers.API.Services;
using System.Collections.Generic;
using CloudCustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetAllTestUsers());
        var usersController = new UsersController(mockUsersService.Object);

        //  Act
        var result = (OkObjectResult)await usersController.Get();

        // Assert

        result.StatusCode.Should().Be(200);

    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserService()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
             .Setup(service => service.GetAllUsers())
             .ReturnsAsync(new List<User>());

        var usersController = new UsersController(mockUsersService.Object);
        //  Act
        var result =await usersController.Get();

        // Assert
        mockUsersService.Verify(service => service.GetAllUsers(), Times.Once);

    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetAllTestUsers()) ;

        var usersController = new UsersController(mockUsersService.Object);
        //  Act
        var result = (OkObjectResult)await usersController.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var resultObject = (OkObjectResult)result;

        resultObject.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNotUserFound_Return404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var usersController = new UsersController(mockUsersService.Object);
        //  Act
        var result =(NotFoundResult)await usersController.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var resultObject= (NotFoundResult)result;
        resultObject.StatusCode.Should().Be(404);
    }
}