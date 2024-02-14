// <copyright file="IClient.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Clients;

using System;

/// <summary>
///   Defines an interface that represents a TCP client.
/// </summary>
/// <seealso cref="IDisposable"/>
public interface IClient : IDisposable
{
    /// <summary>
    ///   Gets a <c>string</c> that represents the IP address.
    /// </summary>
    /// <value>
    ///   A <c>string</c> that represents the IP address.
    /// </value>
    string Address { get; }

    /// <summary>
    ///   Gets a value indicating whether this instance is connected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    /// </value>
    bool IsConnected { get; }

    /// <summary>
    ///   Gets a <c>int</c> that represents the port number.
    /// </summary>
    /// <value>
    ///   A <c>int</c> that represents the port number.
    /// </value>
    int Port { get; }

    /// <summary>
    ///   Connects this instance to the server (indicated by the <see cref="Address"/> and <see cref="Port"/> if not connected.
    /// </summary>
    void Connect();

    /// <summary>
    ///   Disconnects this instance from the server (if connected).
    /// </summary>
    void Disconnect();
}
