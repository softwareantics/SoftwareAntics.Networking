// <copyright file="TcpListenerFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Net;

[ExcludeFromCodeCoverage(Justification = "Wrapper")]
internal sealed class TcpListenerFactory : ITcpListenerFactory
{
    public ITcpListenerInvoker CreateTcpListener(string host, int port)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(host, nameof(host));
        ArgumentOutOfRangeException.ThrowIfLessThan(port, IPEndPoint.MinPort, nameof(port));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, IPEndPoint.MaxPort, nameof(port));

        if (!IPAddress.TryParse(host, out var address))
        {
            throw new ArgumentException($"Failed to parse IP Address: '{host}'");
        }

        return new TcpListenerInvoker(address, port);
    }
}
