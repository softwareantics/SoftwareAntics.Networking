// <copyright file="ITcpClientFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

internal interface ITcpClientFactory
{
    ITcpClientInvoker CreateClient();
}
