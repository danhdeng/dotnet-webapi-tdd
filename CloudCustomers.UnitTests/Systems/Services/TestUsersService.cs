

using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services;
public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest() {

        //Arrange
        var expectedResponse = UsersFixture.GetAllTestUsers();

        var handleMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);

        var httpClient = new HttpClient(handleMock.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var testUsersService = new UsersService(httpClient, config);

        //Act

        await testUsersService.GetAllUsers();

        //Assert
        //Verify Http request is made!!!
        handleMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req=>req.Method==HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );

    }
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
    {

        //Arrange
        var expectedResponse = UsersFixture.GetAllTestUsers();

        var handleMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);

        var httpClient = new HttpClient(handleMock.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var testUsersService = new UsersService(httpClient, config);

        //Act

       var result=await testUsersService.GetAllUsers();

        //Assert
        result.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnNotFound404()
    {

        //Arrange
        //var expectedResponse = UsersFixture.GetAllTestUsers();

        var handleMock = MockHttpMessageHandler<User>.SetupReturn404();

        var httpClient = new HttpClient(handleMock.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var testUsersService = new UsersService(httpClient, config);

        //Act

        var result = await testUsersService.GetAllUsers();

        //Assert
        result.Count.Should().Be(0);

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnExpectedNumberOfUsers()
    {

        //Arrange
        var expectedResponse = UsersFixture.GetAllTestUsers();

        var handleMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);

        var httpClient = new HttpClient(handleMock.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var testUsersService = new UsersService(httpClient, config);

        //Act

        var result = await testUsersService.GetAllUsers();

        //Assert
        result.Count.Should().Be(expectedResponse.Count);

    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl()
    {

        //Arrange
        var expectedResponse = UsersFixture.GetAllTestUsers();
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var handleMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        var config = Options.Create(new UserApiOptions
        {
            Endpoint = endpoint
        });
        var httpClient = new HttpClient(handleMock.Object);
        var testUsersService = new UsersService(httpClient, config);
        var requestUri = new Uri(endpoint);
        //Act

        await testUsersService.GetAllUsers();

        //Assert
        //result.Count.Should().Be(expectedResponse.Count);
        handleMock
           .Protected()
           .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri==requestUri),
               ItExpr.IsAny<CancellationToken>()
           );

    }
}
