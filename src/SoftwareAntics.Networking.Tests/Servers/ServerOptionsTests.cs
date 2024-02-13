namespace SoftwareAntics.Networking.Tests.Servers;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using NUnit.Framework;
using SoftwareAntics.Networking.Servers;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

[TestFixture]
public sealed class ServerOptionsTests
{
    private ServerOptions options;

    [Test]
    public void AddressShouldHaveRequiredAttributeWhenInvoked()
    {
        // Act and assert
        Assert.That(this.options.GetType().GetProperty(nameof(ServerOptions.Address)).GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
    }

    [Test]
    public void AddressShouldReturnSameAsInputWhenInvoked()
    {
        // Arrange
        const string expected = "127.0.0.1";

        this.options.Address = expected;

        // Act
        string actual = this.options.Address;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorShouldNotThrowException()
    {
        // Assert
        Assert.DoesNotThrow(() =>
        {
            new ServerOptions();
        });
    }

    [Test]
    public void PortShouldHaveMaxRangeAttributeWhenInvoked()
    {
        // Arrange
        var range = this.options.GetType().GetProperty(nameof(ServerOptions.Port)).GetCustomAttribute<RangeAttribute>();

        // Act
        object actual = range.Maximum;

        // Assert
        Assert.That(actual, Is.EqualTo(IPEndPoint.MaxPort));
    }

    [Test]
    public void PortShouldHaveMinRangeAttributeWhenInvoked()
    {
        // Arrange
        var range = this.options.GetType().GetProperty(nameof(ServerOptions.Port)).GetCustomAttribute<RangeAttribute>();

        // Act
        object actual = range.Minimum;

        // Assert
        Assert.That(actual, Is.EqualTo(IPEndPoint.MinPort));
    }

    [Test]
    public void PortShouldHaveRangeAttributeWhenInvoked()
    {
        // Act and assert
        Assert.That(this.options.GetType().GetProperty(nameof(ServerOptions.Port)).GetCustomAttribute<RangeAttribute>(), Is.Not.Null);
    }

    [Test]
    public void PortShouldHaveRequiredAttributeWhenInvoked()
    {
        // Act and assert
        Assert.That(this.options.GetType().GetProperty(nameof(ServerOptions.Port)).GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
    }

    [Test]
    public void PortShouldReturnSameAsInputWhenInvoked()
    {
        // Arrange
        const int expected = 45455;

        this.options.Port = expected;

        // Act
        int actual = this.options.Port;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [SetUp]
    public void Setup()
    {
        this.options = new ServerOptions();
    }
}
