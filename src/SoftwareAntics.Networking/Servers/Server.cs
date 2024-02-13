// <copyright file="Server.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Servers;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoftwareAntics.Networking.Invocation;

public class Server : IServer
{
    private readonly ILogger<Server> logger;

    private ITcpListenerInvoker? listener;

    [ExcludeFromCodeCoverage(Justification = "Public API")]
    public Server(ILogger<Server> logger, IOptionsSnapshot<ServerOptions> options)
        : this(logger, options, new TcpListenerFactory())
    {
    }

    internal Server(ILogger<Server> logger, IOptionsSnapshot<ServerOptions> options, ITcpListenerFactory factory)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.listener = factory.CreateTcpListener(options.Value.Address, options.Value.Port);

        this.Address = options.Value.Address;
        this.Port = options.Value.Port;
    }

    ~Server()
    {
        this.Dispose(false);
    }

    public string Address { get; }

    public bool IsRunning { get; private set; }

    public int Port { get; }

    protected bool IsDisposed { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        if (this.IsRunning)
        {
            return;
        }

        this.Run();
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);

        if (!this.IsRunning)
        {
            return;
        }

        this.listener!.Stop();
        this.IsRunning = false;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.listener != null)
            {
                this.listener.Dispose();
                this.listener = null;
            }
        }

        this.IsDisposed = true;
    }

    private void Run()
    {
        this.listener!.Start();

        this.logger.LogInformation($"Server listening on port: '{this.Address}:{this.Port}'...");

        this.IsRunning = true;
    }
}
