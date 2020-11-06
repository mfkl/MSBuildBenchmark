using BenchmarkDotNet.Attributes;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using MSBuildBenchmark;
using System.IO;

namespace MSBuildBenchmarks
{
    [Config(typeof(Config))]
    public class PreprocessorBenchmarks
    {
        Project project;

        [GlobalSetup(Target = nameof(PreprocessorBenchmarkSingle))]
        public void PreprocessorBenchmarkSingleSetup()
        {
            project = new Project();
            project.SetProperty("p", "v1");
        }

        [GlobalSetup(Target = nameof(PreprocessorBenchmarkInitialTargetsOuterAndInner))]
        public void PreprocessorBenchmarkInitialTargetsOuterAndInnerSetup()
        {
            ProjectRootElement xml1 = ProjectRootElement.Create("p1");
            xml1.InitialTargets = "i1";
            xml1.AddImport("p2");
            ProjectRootElement xml2 = ProjectRootElement.Create("p2");
            xml2.InitialTargets = "i2";

            project = new Project(xml1);
        }

        [Benchmark]
        public void PreprocessorBenchmarkSingle()
        {
            project.SaveLogicalProject(new StringWriter());
        }

        [Benchmark]
        public void PreprocessorBenchmarkInitialTargetsOuterAndInner()
        {
            project.SaveLogicalProject(new StringWriter());
        }
    }
}