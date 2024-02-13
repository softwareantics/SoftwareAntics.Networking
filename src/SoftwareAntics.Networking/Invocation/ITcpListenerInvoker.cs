// <copyright file="ITcpListenerInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Net.Sockets;

/// <inheritdoc cref="TcpListener"/>
internal interface ITcpListenerInvoker : IDisposable
{
    /// <inheritdoc cref="TcpListener.Start()"/>
    void Start();

    /// <inheritdoc cref="TcpListener.Stop"/>
    void Stop();
}
