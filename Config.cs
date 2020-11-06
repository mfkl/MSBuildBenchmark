using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace MSBuildBenchmark
{
    class Config : ManualConfig
    {
        const string Build = "Microsoft.Build";
        const string BuildFramework = "Microsoft.Build.Framework";
        const string BuildTasksCore = "Microsoft.Build.Tasks.Core";

        const string Version1670 = "16.7.0";
        const string Version1660 = "16.6.0";
        const string Version1650 = "16.5.0";
        const string Version1640 = "16.4.0";

        public Config()
        {
            var baseJob = Job.MediumRun;

            AddMSBuildJob(Version1640, Version1650, Version1660, Version1670);

            void AddMSBuildJob(params string[] versions)
            {
                foreach (var version in versions)
                {
                    var dependencies = new NuGetReferenceList
                    {
                        new NuGetReference(Build, version),
                        new NuGetReference(BuildFramework, version),
                        new NuGetReference(BuildTasksCore, version),
                    };

                    AddJob(baseJob.WithNuGet(dependencies));
                }
            }
        }
    }
}
