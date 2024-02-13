// <copyright file="ServerOptions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Servers;

using System.ComponentModel.DataAnnotations;
using System.Net;

#nullable disable warnings

public sealed class ServerOptions
{
    [Required]
    public string Address { get; set; }

    [Required]
    [Range(IPEndPoint.MinPort, IPEndPoint.MaxPort)]
    public int Port { get; set; }
}

#nullable enable warnings
