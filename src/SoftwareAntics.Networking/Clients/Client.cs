// <copyright file="Client.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Clients;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoftwareAntics.Networking.Invocation;

/// <summary>
///   Provides a standard implementation of an <see cref="IClient"/>.
/// </summary>
/// <seealso cref="IClient"/>
public class Client : IClient
{
    /// <summary>
    ///   The logger.
    /// </summary>
    private readonly ILogger<Client> logger;

    /// <summary>
    ///   The underlying TCP client.
    /// </summary>
    private ITcpClientInvoker? client;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Client"/> class.
    /// </summary>
    /// <param name="logger">
    ///   The logger.
    /// </param>
    /// <param name="options">
    ///   The client options used to configure the client connection properties.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   Thrown if the one of the following parameters are null:
    ///   <list type="bullet">
    ///     <item><paramref name="logger"/></item>
    ///     <item><paramref name="options"/></item>
    ///   </list>
    /// </exception>
    [ExcludeFromCodeCoverage(Justification = "Public API")]
    public Client(ILogger<Client> logger, IOptionsSnapshot<ClientOptions> options)
        : this(logger, options, new TcpClientFactory())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Client"/> class.
    /// </summary>
    /// <param name="logger">
    ///   The logger.
    /// </param>
    /// <param name="options">
    ///   The client options used to configure the client connection properties.
    /// </param>
    /// <param name="factory">
    ///   The factory used to handle instantiating an underlying <see cref="TcpClient"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   Thrown if the one of the following parameters are null:
    ///   <list type="bullet">
    ///     <item><paramref name="logger"/></item>
    ///     <item><paramref name="options"/></item>
    ///     <item><paramref name="factory"/></item>
    ///   </list>
    /// </exception>
    internal Client(ILogger<Client> logger, IOptionsSnapshot<ClientOptions> options, ITcpClientFactory factory)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.client = factory.CreateClient();

        this.Address = options.Value.Address;
        this.Port = options.Value.Port;
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="Client"/> class.
    /// </summary>
    ~Client()
    {
        this.Dispose(false);
    }

    /// <inheritdoc/>
    public string Address { get; }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown if this <see cref="Client"/> is disposed.
    /// </exception>
    public bool IsConnected
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.IsDisposed, this);
            return this.client!.Connected;
        }
    }

    /// <inheritdoc/>
    public int Port { get; }

    /// <summary>
    ///   Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown if this <see cref="Client"/> is disposed.
    /// </exception>
    public void Connect()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        if (this.IsConnected)
        {
            return;
        }

        this.client!.Connect(this.Address, this.Port);

        if (this.IsConnected)
        {
            this.logger.LogInformation($"Client Connected: '{this.Address}:{this.Port}'");
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown if this <see cref="Client"/> is disposed.
    /// </exception>
    public void Disconnect()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        if (!this.IsConnected)
        {
            return;
        }

        this.client!.Close();
        this.logger.LogInformation($"Client Disconnected: '{this.Address}:{this.Port}'");
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.client != null)
            {
                this.Disconnect();
                this.client = null;
            }
        }

        this.IsDisposed = true;
    }
}
