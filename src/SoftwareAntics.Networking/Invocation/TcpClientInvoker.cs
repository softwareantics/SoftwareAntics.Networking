// <copyright file="TcpClientInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

[ExcludeFromCodeCoverage(Justification = "Wrapper")]
internal sealed class TcpClientInvoker : TcpClient, ITcpClientInvoker
{
}
