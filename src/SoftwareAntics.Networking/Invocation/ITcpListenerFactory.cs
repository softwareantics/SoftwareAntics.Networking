// <copyright file="ITcpListenerFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

internal interface ITcpListenerFactory
{
    ITcpListenerInvoker CreateTcpListener(string host, int port);
}
