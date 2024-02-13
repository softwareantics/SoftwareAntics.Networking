// <copyright file="SharedAssemblyInfo.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using System.Reflection;

[assembly: AssemblyProduct("Project Name")]
[assembly: AssemblyCompany("Software Antics")]
[assembly: AssemblyCopyright("© 2024 Software Antics")]
[assembly: AssemblyTrademark("Software Antics™")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("2024.0.0.0")]
[assembly: AssemblyFileVersion("2024.0.0.0")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
