// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace MSBuildBenchmark
{
    internal sealed class MockEngine : IBuildEngine
    {
        private readonly ProjectCollection _projectCollection = new ProjectCollection();

        public bool ContinueOnError => false;
        public int LineNumberOfTaskNode => 0;
        public int ColumnNumberOfTaskNode => 0;
        public string ProjectFileOfTaskNode => string.Empty;

        public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalPropertiesPassedIntoTask, IDictionary targetOutputs)
        {
            return BuildProjectFile(projectFileName, targetNames, globalPropertiesPassedIntoTask, targetOutputs, null);
        }

        public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalPropertiesPassedIntoTask, IDictionary targetOutputs, string toolsVersion)
        {
            var finalGlobalProperties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Finally, whatever global properties were passed into the task ... those are the final winners.
            if (globalPropertiesPassedIntoTask != null)
            {
                foreach (DictionaryEntry newGlobalProperty in globalPropertiesPassedIntoTask)
                {
                    finalGlobalProperties[(string)newGlobalProperty.Key] = (string)newGlobalProperty.Value;
                }
            }

            Project project = _projectCollection.LoadProject(projectFileName, finalGlobalProperties, toolsVersion);

            ILogger[] loggers = { new ConsoleLogger() };

            return project.Build(targetNames, loggers);
        }

        public void LogCustomEvent(CustomBuildEventArgs e)
        {
        }

        public void LogErrorEvent(BuildErrorEventArgs e)
        {
        }

        public void LogMessageEvent(BuildMessageEventArgs e)
        {
        }

        public void LogWarningEvent(BuildWarningEventArgs e)
        {
        }
    }
}