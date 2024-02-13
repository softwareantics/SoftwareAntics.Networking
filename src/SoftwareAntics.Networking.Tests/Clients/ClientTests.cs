namespace SoftwareAntics.Networking.Tests.Clients;

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using SoftwareAntics.Networking.Clients;
using SoftwareAntics.Networking.Invocation;

[TestFixture]
public sealed class ClientTests
{
    private Client client;

    private ITcpClientFactory factory;

    private ITcpClientInvoker invoker;

    private ILogger<Client> logger;

    private IOptionsSnapshot<ClientOptions> options;

    [Test]
    public void AddressShouldReturnOptionsAddressWhenInvoked()
    {
        // Arrange
        string expected = this.options.Value.Address;

        // Act
        string actual = this.client.Address;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConnectShouldInvokeClientConnectWhenNotConnected()
    {
        // Arrange
        this.invoker.Connected.Returns(false);

        this.invoker.When(x =>
        {
            x.Connect(this.options.Value.Address, this.options.Value.Port);
        }).Do(x =>
        {
            this.invoker.Connected.Returns(true);
        });

        // Act
        this.client.Connect();

        // Assert
        this.invoker.Received(1).Connect(this.options.Value.Address, this.options.Value.Port);
    }

    [Test]
    public void ConnectShouldNotInvokeClientConnectWhenIsConnected()
    {
        // Arrange
        this.invoker.Connected.Returns(true);

        // Act
        this.client.Connect();

        // Assert
        this.invoker.Received(0).Connect(this.options.Value.Address, this.options.Value.Port);
    }

    [Test]
    public void ConnectShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.client.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.client.Connect);
    }

    [Test]
    public void ConstructorShouldInvokeFactoryCreateClientWhenInvoked()
    {
        // Assert
        this.factory.Received(1).CreateClient();
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Client(this.logger, this.options, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Client(null, this.options, this.factory);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenOptionsIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new Client(this.logger, null, this.factory);
        });
    }

    [Test]
    public void DisconnectShouldNotInvokeClientCloseWhenDisconnected()
    {
        // Act
        this.client.Disconnect();

        // Assert
        this.invoker.Received(0).Close();
    }

    [Test]
    public void DisconnectShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.client.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.client.Disconnect);
    }

    [Test]
    public void DisposeShouldInvokeClientCloseWhenConnected()
    {
        // Arrange
        this.invoker.Connected.Returns(true);

        // Act
        this.client.Dispose();

        // Assert
        this.invoker.Received(1).Close();
    }

    [Test]
    public void DisposeShouldNotCloseConnectionWhenDisposed()
    {
        // Arrange
        this.invoker.Connected.Returns(true);
        this.client.Dispose();

        // Act
        this.client.Dispose();

        // Assert
        this.invoker.Received(1).Close();
    }

    [Test]
    public void IsConnectedShouldReturnFalseWhenClientConnectedIsFalse()
    {
        // Arrange
        this.invoker.Connected.Returns(false);

        // Act
        bool actual = this.client.IsConnected;

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public void IsConnectedShouldReturnTrueWhenClientConnectedIsTrue()
    {
        // Arrange
        this.invoker.Connected.Returns(true);

        // Act
        bool actual = this.client.IsConnected;

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public void IsConnectedShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.client.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            _ = this.client.IsConnected;
        });
    }

    [Test]
    public void PortShouldReturnOptionsPortWhenInvoked()
    {
        // Arrange
        int expected = this.options.Value.Port;

        // Act
        int actual = this.client.Port;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.logger = Substitute.For<ILogger<Client>>();
        this.options = Substitute.For<IOptionsSnapshot<ClientOptions>>();
        this.factory = Substitute.For<ITcpClientFactory>();
        this.invoker = Substitute.For<ITcpClientInvoker>();

        this.factory.CreateClient().Returns(this.invoker);

        this.options.Value.Returns(new ClientOptions()
        {
            Address = "127.0.0.1",
            Port = 45455,
        });

        this.client = new Client(this.logger, this.options, this.factory);
    }
}
