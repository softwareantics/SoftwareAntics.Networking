// <copyright file="Client.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Clients;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoftwareAntics.Networking.Invocation;

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

    [ExcludeFromCodeCoverage(Justification = "Public API")]
    public Client(ILogger<Client> logger, IOptionsSnapshot<ClientOptions> options)
        : this(logger, options, new TcpClientFactory())
    {
    }

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

    public string Address { get; }

    public bool IsConnected
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.IsDisposed, this);
            return this.client!.Connected;
        }
    }

    public int Port { get; }

    /// <summary>
    ///   Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

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
