﻿// --
// -- TestCop http://github.com/testcop
// -- License http://github.com/testcop/license
// -- Copyright 2014
// --

using System.IO;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace TestCop.Plugin.Tests.MultipleTestProjectToSingleCodeProjectViaProjectName
{
    [TestFixture]
    public class TestToClassFileNavigationTests : CSharpHighlightingWithinSolutionTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return highlighting.GetType().Namespace.Contains("TestCop");
        }

        protected override string RelativeTestDataPath
        {
            get { return @"MultipleTestProjectToSingleCodeProjectViaName\TestToClassNavigation"; }
        }

        protected override IExecutableAction GetShortcutAction(TextWriter textwriter)
        {
            var jumpToTestFileAction = TestCopJumpToTestFileAction.CreateWith(CreateJetPopMenuShowToWriterAction(textwriter));
            return jumpToTestFileAction;
        }
        protected override string SolutionName
        {
            get { return @"MyCorp.TestApplication4.sln"; }
        }

        [Test]
        [TestCase(@"<APITests>\ClassATests.cs")]
        [TestCase(@"<APITests>\NS1\ClassATests.cs")]
        [TestCase(@"<APITests>\Properties\AssemblyInfo.cs")]

        [TestCase(@"<APITests>\NS1\NonNamespaceFolder\NS2\ClassEInNonNamespaceTests.cs")]
        [TestCase(@"<APITests>\NonNamespaceFolder\ClassDInNonNamespaceTests.cs")]

        public void Test(string testName)
        {
            const string altRegEx = "^(.*?)\\.?(Integration)*Tests$";

            ExecuteWithinSettingsTransaction((settingsStore =>
            {
                RunGuarded(
                    () =>
                    {
                        ClearRegExSettingsPriorToRun(settingsStore);

                        settingsStore.SetValue<TestFileAnalysisSettings, TestProjectStrategy>(
                            s => s.TestCopProjectStrategy, TestProjectStrategy.TestProjectHasSameNamespaceAsCodeProject);
                        settingsStore.SetValue<TestFileAnalysisSettings, bool>(
                            s => s.FindOrphanedProjectFiles, true);
                        settingsStore.SetValue<TestFileAnalysisSettings, string>(
                            s => s.TestProjectNameToCodeProjectNameRegEx, altRegEx);
                        settingsStore.SetValue<TestFileAnalysisSettings, string>(
                            s => s.TestProjectNameToCodeProjectNameRegExReplace, "$1");
                    }

                    );
                DoTestFiles(testName);
            }));
        }
    }
}
