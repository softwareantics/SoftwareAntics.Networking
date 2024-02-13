namespace SoftwareAntics.Networking.Tests.Servers;

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using SoftwareAntics.Networking.Invocation;
using SoftwareAntics.Networking.Servers;

[TestFixture]
public sealed class ServerTests
{
    private ITcpListenerFactory factory;

    private ITcpListenerInvoker invoker;

    private ILogger<Server> logger;

    private IOptionsSnapshot<ServerOptions> options;

    private Server server;

    [Test]
    public void AddressShouldReturnOptionsAddressWhenInvoked()
    {
        // Arrange
        string expected = this.options.Value.Address;

        // Act
        string actual = this.server.Address;

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Server(this.logger, this.options, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Server(null, this.options, this.factory);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenOptionsIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Server(this.logger, null, this.factory);
        });
    }

    [Test]
    public void DisposeShouldInvokeListenerDisposeWhenInvoked()
    {
        // Act
        this.server.Dispose();

        // Assert
        this.invoker.Received(1).Dispose();
    }

    [Test]
    public void DisposeShouldNotInvokeListenerStopWhenAlreadyDisposed()
    {
        // Arrange
        this.server.Dispose();

        // Act
        this.server.Dispose();

        // Assert
        this.invoker.Received(1).Dispose();
    }

    [Test]
    public void IsRunningShouldReturnFalseWhenNotStarted()
    {
        // Act
        bool actual = this.server.IsRunning;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsRunningShouldReturnFalseWhenStopped()
    {
        // Arrange
        this.server.Start();

        // Act
        this.server.Stop();

        // Assert
        Assert.That(this.server.IsRunning, Is.False);
    }

    [Test]
    public void IsRunningShouldReturnTrueWhenStarted()
    {
        // Act
        this.server.Start();

        // Assert
        Assert.That(this.server.IsRunning, Is.True);
    }

    [Test]
    public void PortShouldReturnOptionsPortWhenInvoked()
    {
        // Arrange
        int expected = this.options.Value.Port;

        // Act
        int actual = this.server.Port;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.logger = Substitute.For<ILogger<Server>>();
        this.factory = Substitute.For<ITcpListenerFactory>();
        this.invoker = Substitute.For<ITcpListenerInvoker>();
        this.options = Substitute.For<IOptionsSnapshot<ServerOptions>>();

        this.factory.CreateTcpListener(Arg.Any<string>(), Arg.Any<int>()).Returns(this.invoker);

        this.options.Value.Returns(new ServerOptions()
        {
            Address = "127.0.0.1",
            Port = 45455,
        });

        this.server = new Server(this.logger, this.options, this.factory);
    }

    [Test]
    public void StartShouldInvokeListenerStartWhenNotStarted()
    {
        // Act
        this.server.Start();

        // Assert
        this.invoker.Received(1).Start();
    }

    [Test]
    public void StartShouldNotInvokeListenerStartWhenAlreadyStarted()
    {
        // Arrange
        this.server.Start();

        // Act
        this.server.Start();

        // Assert
        this.invoker.Received(1).Start();
    }

    [Test]
    public void StartShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.server.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.server.Start);
    }

    [Test]
    public void StopShouldInvokeListenerStopWhenStarted()
    {
        // Arrange
        this.server.Start();

        // Act
        this.server.Stop();

        // Assert
        this.invoker.Received(1).Stop();
    }

    [Test]
    public void StopShouldNotInvokeListenerStopWhenNotStarted()
    {
        // Act
        this.server.Stop();

        // Assert
        this.invoker.Received(0).Stop();
    }

    [Test]
    public void StopShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.server.Dispose();

        // Assert
        Assert.Throws<ObjectDisposedException>(this.server.Stop);
    }
}
