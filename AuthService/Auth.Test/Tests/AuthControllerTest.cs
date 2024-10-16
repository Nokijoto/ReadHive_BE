﻿using Application.Commands;
using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Application.Models.Results;
using AuthService.Controllers;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Auth.Test.Tests;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILoggingService> _logMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _logMock = new Mock<ILoggingService>();

        _authController = new AuthController(
            _mediatorMock.Object,
            _logMock.Object);
    }
    
    [Fact]
    public async Task Login_ReturnsOk_WhenLoginIsSuccessful()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "password123" };
        var token= "token";
        var authResult = new AuthResult(true, token, DateTime.Now.AddHours(1));


        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(authResult);

        var result = await _authController.Login(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AuthResponse>(okResult.Value);
        Assert.Equal("token", response.Token);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenLoginFails()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "wrongpassword" };

        var authResult = new AuthResult(new List<string> { "Invalid credentials" });

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(authResult);

        var result = await _authController.Login(request);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);

        Assert.Contains("Invalid credentials", errorResponse.Errors);
    }
    [Fact]
    public async Task Register_ReturnsNoContent_WhenRegistrationIsSuccessful()
    {
        var request = new RegisterRequest
        {
            UserName = "newuser",
            Email = "newuser@example.com",
            Password = "password123",
            FirstName = "John",
            LastName = "Doe"
        };
        var registerResult =  new RegisterResult(
            true,
            new List<string>());

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
            .ReturnsAsync(registerResult);

        var result = await _authController.Register(request);

        Assert.IsType<NoContentResult>(result);
    }

    // [Fact]
    // public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
    // {
    //     // Arrange
    //     var request = new RegisterRequest
    //     {
    //         UserName = "newuser",
    //         Email = "newuser@example.com",
    //         Password = "password123",
    //         FirstName = "John",
    //         LastName = "Doe"
    //     };
    //     var registerResult = new RegisterResult(
    //         false,
    //         new List<string>
    //         {
    //             "User already exists"
    //         }
    //     );
    //
    // _mediatorMock
    //         .Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
    //         .ReturnsAsync(registerResult);
    //
    //     // Act
    //     var result = await _authController.Register(request);
    //
    //     // Assert
    //     var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //     var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
    //     Assert.Contains("User already exists", errorResponse.Errors);
    // }
    [Fact]
    public async Task VerifyToken_ReturnsNoContent()
    {
        // Act
        var result = await _authController.VerifyToken();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

}
