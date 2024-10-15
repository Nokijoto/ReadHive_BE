using Application.Commands;
using Application.Handlers;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Moq;

namespace Auth.Test;

public class LoginTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly Mock<ILoggingService> _loggingServiceMock;
    private readonly LoginCommandHandler _handler;
    
    public LoginTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _loggingServiceMock = new Mock<ILoggingService>();
        _handler = new LoginCommandHandler(_authServiceMock.Object, _loggingServiceMock.Object);
    }

    [Fact]
    public async Task VerifyCorrectLogin_ShouldReturnSuccessResult()
    {
        var command = new LoginCommand("test@example.com", "ValidPassword");
        var loginResponse = new AuthenticationResultDto 
        {
            Succeeded = true,
            Token = "token123",
            Expiration = DateTime.Now.AddHours(1)
        };
        _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(loginResponse);
            
        var result = await _handler.Handle(command, CancellationToken.None);
            
        Assert.True(result.Succeeded);
        Assert.Equal("token123", result.Token);
        Assert.NotNull(result.Expiration);
        _loggingServiceMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
    }
    
   [Fact]
        public async Task VerifyIncorrectLogin_ShouldReturnFailureResult()
        {
            var command = new LoginCommand("test@example.com", "InvalidPassword");
            var loginResponse = new AuthenticationResultDto
            {
                Succeeded = false,
                Token = string.Empty,
                Expiration = null,
                Errors = new List<Exception>
                {
                     new Exception("Invalid credentials")
                }
            };            
            _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(loginResponse);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.False(result.Succeeded);
            Assert.Empty(result.Token);
            Assert.Contains("Invalid credentials", result.Errors);
            _loggingServiceMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenExceptionOccurs()
        {
            var command = new LoginCommand("test@example.com", "SomePassword");
            _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ThrowsAsync(new Exception("Unexpected error"));

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.False(result.Succeeded);
            Assert.Empty(result.Token);
            _loggingServiceMock.Verify(x => x.LogError("Error in LoginCommandHandler", It.IsAny<Exception>()), Times.Once);
        }

        [Fact]
        public async Task LoginWithEmptyData_ShouldReturnFailureResult()
        {
            var command = new LoginCommand(string.Empty, string.Empty);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.False(result.Succeeded);
            Assert.Empty(result.Token);
            Assert.Contains("Email or Password cannot be empty", result.Errors);
            _loggingServiceMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
        }
}