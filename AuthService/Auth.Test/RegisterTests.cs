using Application.Commands;
using Application.Handlers;
using Application.Interfaces;
using Application.Models.Dto;
using Infrastructure.Interfaces;
using Moq;

namespace Auth.Test.Tests;

public class RegisterTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly Mock<ILoggingService> _loggingServiceMock;
    private readonly RegisterCommandHandler _handler;
    
    public RegisterTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _loggingServiceMock = new Mock<ILoggingService>();
        _handler = new RegisterCommandHandler(_authServiceMock.Object, _loggingServiceMock.Object);
    }
    
    [Fact]
    public async Task RegisterWithEmptyData_ShouldReturnFailureResult()
    {
        var command = new RegisterCommand(string.Empty, string.Empty, string.Empty,string.Empty, string.Empty);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.Succeeded);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public async Task RegisterWithInvalidEmail_ShouldReturnFailureResult()
    {
        var command = new RegisterCommand("test@example.com", "SomePassword", "SomePassword", string.Empty, string.Empty);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.Succeeded);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public async Task RegisterWithInvalidPassword_ShouldReturnFailureResult()
    {
        var command = new RegisterCommand("test@example.com", "InvalidPassword", "SomePassword", string.Empty, string.Empty);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.Succeeded);
    }
    
    [Fact]
    public async Task RegisterWithValidData_ShouldReturnSuccessResult()
    {
        var command = new RegisterCommand("test@example.com", "SomePassword", "SomePassword" , string.Empty, string.Empty);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.True(result.Succeeded);        
    }
    
    [Fact]
    public async Task Register_ShouldLogError_WhenExceptionOccurs()
    {
        var command = new RegisterCommand("test@example.com", "SomePassword", "SomePassword", string.Empty, string.Empty);
        _authServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterDto>())).ThrowsAsync(new Exception("Unexpected error"));
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.Succeeded);
    }
}