using BenchmarkDotNet.Attributes;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using MSBuildBenchmark;
using System.Collections.Generic;
using System.IO;

namespace MSBuild.Benchmarks
{
    /// <summary>
    /// from https://github.com/dotnet/msbuild/blob/master/src/Tasks.UnitTests/AssemblyDependency/Perf.cs
    /// https://github.com/dotnet/msbuild/blob/master/src/Tasks.UnitTests/AssemblyDependency/Miscellaneous.cs
    /// </summary>
    [Config(typeof(Config))]
    public class RarBenchmarks
    {
        // retrieved from ResolveAssemblyReferenceTestFixture
        static readonly string s_rootPathPrefix = "C:\\";
        static readonly string s_regress442570_RootPath = Path.Combine(s_rootPathPrefix, "Regress442570");
        static readonly string s_myLibrariesRootPath = Path.Combine(s_rootPathPrefix, "MyLibraries");
        static readonly string s_myLibraries_V1Path = Path.Combine(s_myLibrariesRootPath, "v1");
        static readonly string s_myLibraries_V2Path = Path.Combine(s_myLibrariesRootPath, "v2");
        ResolveAssemblyReference t;

        [GlobalSetup(Target = nameof(DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally))]
        public void DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternallySetup()
        {
            t = new ResolveAssemblyReference
            {
                BuildEngine = new MockEngine(),
                Assemblies = new ITaskItem[]
                {
                    new TaskItem(@"C:\DependsOnNuget\A.dll"), // depends on N, version 1.0.0.0
                    new TaskItem(@"C:\NugetCache\N\lib\N.dll", // version 2.0.0.0
                    new Dictionary<string, string>
                    {
                        {"ExternallyResolved", "true"}
                    })
                },
                SearchPaths = new[] { "{RawFileName}" },
                AutoUnify = true
            };
        }

        [GlobalSetup(Target = nameof(ConflictBetweenBackAndForeVersionsNotCopyLocal))]
        public void ConflictBetweenBackAndForeVersionsNotCopyLocalSetup()
        {
            t = new ResolveAssemblyReference
            {
                Assemblies = new ITaskItem[]
                {
                    new TaskItem("D, Version=2.0.0.0, Culture=neutral, PublicKeyToken=aaaaaaaaaaaaaaaa"),
                    new TaskItem("D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=aaaaaaaaaaaaaaaa")
                },
                BuildEngine = new MockEngine(),
                SearchPaths = new string[] {
                    s_myLibrariesRootPath, s_myLibraries_V2Path, s_myLibraries_V1Path
                },
            };
        }

        [GlobalSetup(Target = nameof(RedirectsAreSuggestedInExternallyResolvedGraph))]
        public void RedirectsAreSuggestedInExternallyResolvedGraphSetup()
        {
            t = new ResolveAssemblyReference
            {
                BuildEngine = new MockEngine(),
                AutoUnify = true,
                FindDependenciesOfExternallyResolvedReferences = true,
                Assemblies = new ITaskItem[]
                {
                    new TaskItem("A", new Dictionary<string, string> { ["ExternallyResolved"] = "true" } ),
                    new TaskItem("B", new Dictionary<string, string> { ["ExternallyResolved"] = "true" } )
                },
                SearchPaths = new string[] { s_regress442570_RootPath }
            };
        }

        [Benchmark]
        public void ConflictBetweenBackAndForeVersionsNotCopyLocal()
        {
            t.Execute();
        }

        [Benchmark]
        public void DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally()
        {
            t.Execute();
        }

        [Benchmark]
        public void RedirectsAreSuggestedInExternallyResolvedGraph()
        {
            t.Execute();
        }
    }
}
