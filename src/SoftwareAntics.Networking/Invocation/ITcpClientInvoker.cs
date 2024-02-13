// <copyright file="ITcpClientInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Net.Sockets;

//// TODO: Create issue about the problem with checking if the client is connected.
//// TODO: Finish up writing unit tests for server and client.
//// TODO: Documentation
//// TODO: Code and documentation review.
//// TODO: Start on handling client connections as well as sending and receving packets.
//// TODO: Add in simple framing support.
//// TODO: Add support for packet handlers.
//// TODO: Ensure testing is good and then celebrate for SoftwareAntics.Networking v2024.2.0.0-pre

/// <inheritdoc cref="TcpClient"/>
internal interface ITcpClientInvoker : IDisposable
{
    /// <inheritdoc cref="TcpClient.Connected"/>
    bool Connected { get; }

    /// <inheritdoc cref="TcpClient.Close"/>
    void Close();

    /// <inheritdoc cref="TcpClient.Connect(string, int)"/>
    void Connect(string hostname, int port);
}
