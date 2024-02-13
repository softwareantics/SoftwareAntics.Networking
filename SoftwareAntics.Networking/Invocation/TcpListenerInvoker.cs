// <copyright file="TcpListenerInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

[ExcludeFromCodeCoverage(Justification = "Wrapper")]
internal sealed class TcpListenerInvoker : TcpListener, ITcpListenerInvoker
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="TcpListenerInvoker"/> class.
    /// </summary>
    /// <param name="localaddr">
    ///   An <see cref="IPAddress"/> that represents the local IP address.
    /// </param>
    /// <param name="port">
    ///   The port on which to listen for incoming connection attempts.
    /// </param>
    public TcpListenerInvoker(IPAddress localaddr, int port)
        : base(localaddr, port)
    {
    }
}
