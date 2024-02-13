// <copyright file="IServer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Servers;

public interface IServer : IDisposable
{
    string Address { get; }

    bool IsRunning { get; }

    int Port { get; }

    void Start();

    void Stop();
}
