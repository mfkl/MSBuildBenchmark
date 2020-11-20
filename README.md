# MSBuildBenchmark

Setup to run benchmarkdotnet-based benchmarks on MSBuild tasks.

## Running

To run the benchmarks, you need to do so in Release mode

```
dotnet run -c Release
```

## Results

The main idea of these benchmarks is to compare performance of specific msbuild tasks on various versions of msbuild. 

Currently the benchmark tests are ran on the following MSBuild versions:
- 16.4.0, 
- 16.5.0, 
- 16.6.0,
- 16.7.0.

```

Example output:

// * Summary *

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]    : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  MediumRun : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

Job=MediumRun  IterationCount=15  LaunchCount=2 WarmupCount=10
```

|                                                                          Method |                                                                           NuGetReferences |      Mean |     Error |     StdDev |    Median |
|-------------------------------------------------------------------------------- |------------------------------------------------------------------------------------------ |----------:|----------:|-----------:|----------:|
|                                  ConflictBetweenBackAndForeVersionsNotCopyLocal | Microsoft.Build 16.4.0,Microsoft.Build.Framework 16.4.0,Microsoft.Build.Tasks.Core 16.4.0 | 364.58 us |  6.472 us |   9.487 us | 365.93 us |
| DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally | Microsoft.Build 16.4.0,Microsoft.Build.Framework 16.4.0,Microsoft.Build.Tasks.Core 16.4.0 |  99.32 us |  2.106 us |   3.020 us |  99.56 us |
|                                  RedirectsAreSuggestedInExternallyResolvedGraph | Microsoft.Build 16.4.0,Microsoft.Build.Framework 16.4.0,Microsoft.Build.Tasks.Core 16.4.0 | 236.67 us | 11.147 us |  16.684 us | 232.51 us |
|                                  ConflictBetweenBackAndForeVersionsNotCopyLocal | Microsoft.Build 16.5.0,Microsoft.Build.Framework 16.5.0,Microsoft.Build.Tasks.Core 16.5.0 | 370.43 us | 14.911 us |  21.386 us | 359.71 us |
| DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally | Microsoft.Build 16.5.0,Microsoft.Build.Framework 16.5.0,Microsoft.Build.Tasks.Core 16.5.0 | 120.58 us | 17.986 us |  25.214 us | 105.98 us |
|                                  RedirectsAreSuggestedInExternallyResolvedGraph | Microsoft.Build 16.5.0,Microsoft.Build.Framework 16.5.0,Microsoft.Build.Tasks.Core 16.5.0 | 399.74 us | 23.847 us |  34.955 us | 405.61 us |
|                                  ConflictBetweenBackAndForeVersionsNotCopyLocal | Microsoft.Build 16.6.0,Microsoft.Build.Framework 16.6.0,Microsoft.Build.Tasks.Core 16.6.0 | 756.63 us | 95.698 us | 140.272 us | 764.58 us |
| DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally | Microsoft.Build 16.6.0,Microsoft.Build.Framework 16.6.0,Microsoft.Build.Tasks.Core 16.6.0 | 130.60 us | 17.364 us |  25.452 us | 129.14 us |
|                                  RedirectsAreSuggestedInExternallyResolvedGraph | Microsoft.Build 16.6.0,Microsoft.Build.Framework 16.6.0,Microsoft.Build.Tasks.Core 16.6.0 | 229.41 us |  3.622 us |   5.077 us | 229.96 us |
|                                  ConflictBetweenBackAndForeVersionsNotCopyLocal | Microsoft.Build 16.7.0,Microsoft.Build.Framework 16.7.0,Microsoft.Build.Tasks.Core 16.7.0 | 380.06 us | 30.363 us |  43.545 us | 358.44 us |
| DependeeDirectoryShouldNotBeProbedForDependencyWhenDependencyResolvedExternally | Microsoft.Build 16.7.0,Microsoft.Build.Framework 16.7.0,Microsoft.Build.Tasks.Core 16.7.0 |  94.96 us |  0.974 us |   1.397 us |  94.91 us |
|                                  RedirectsAreSuggestedInExternallyResolvedGraph | Microsoft.Build 16.7.0,Microsoft.Build.Framework 16.7.0,Microsoft.Build.Tasks.Core 16.7.0 | 219.80 us |  1.146 us |   1.679 us | 219.77 us |