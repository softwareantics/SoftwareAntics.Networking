// <copyright file="IClient.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Clients;

public interface IClient : IDisposable
{
    string Address { get; }

    bool IsConnected { get; }

    int Port { get; }

    void Connect();

    void Disconnect();
}
