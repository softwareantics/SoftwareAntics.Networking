// <copyright file="TcpClientFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Wrapper")]
internal sealed class TcpClientFactory : ITcpClientFactory
{
    public ITcpClientInvoker CreateClient()
    {
        return new TcpClientInvoker();
    }
}
