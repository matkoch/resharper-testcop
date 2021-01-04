// --
// -- TestCop http://github.com/testcop
// -- License http://github.com/testcop/license
// -- Copyright 2016
// --
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Util;
using TestCop.Plugin.Extensions;

namespace TestCop.Plugin.Helper.Mapper
{
    public class TestProjectsMapToSingleCodeProjectHelper : MappingBase
    {
        public override IList<TestCopProjectItem> GetAssociatedProject(IProject currentProject, string className, string currentTypeNamespace, IList<Tuple<string, bool>> subDirectoryElements)
        {
            const string warningMessage = "Not Supported: More than one code project has a default namespace of ";
            string subNameSpace = currentTypeNamespace.RemoveLeading(currentProject.GetDefaultNamespace());

            var filePatterns = AssociatedFileNames(Settings, className);

            if (currentProject.IsTestProject())
            {
                var nameSpaceOfAssociateProject = GetNameSpaceOfAssociatedCodeProject(currentProject);

                var matchedCodeProjects = currentProject.GetSolution().GetNonTestProjects().Where(
                    p => p.GetDefaultNamespace()  == nameSpaceOfAssociateProject).ToList();

                if (matchedCodeProjects.Count() > 1)
                {
                    ResharperHelper.AppendLineToOutputWindow(currentProject.Locks, warningMessage + nameSpaceOfAssociateProject);
                }

                return matchedCodeProjects.Select(p => new TestCopProjectItem(p, TestCopProjectItem.ProjectItemTypeEnum.Code, subNameSpace, subDirectoryElements, filePatterns)).ToList();
            }

            var matchedTestProjects = currentProject.GetSolution().GetTestProjects().Where(
                p => GetNameSpaceOfAssociatedCodeProject(p) == currentProject.GetDefaultNamespace()).ToList();

            return matchedTestProjects.Select(p => new TestCopProjectItem(p, TestCopProjectItem.ProjectItemTypeEnum.Tests, subNameSpace, subDirectoryElements, filePatterns)).ToList();                                        
        }
       
        private static string GetNameSpaceOfAssociatedCodeProject(IProject testProject)
        {
            var testNameSpacePattern = Settings.TestProjectToCodeProjectNameSpaceRegEx;
            string replaceText = Settings.TestProjectToCodeProjectNameSpaceRegExReplace;

            string currentProjectNamespace = testProject.GetDefaultNamespace();
            if (string.IsNullOrEmpty(currentProjectNamespace)) return "";

            string result;
            if (RegexReplace(testNameSpacePattern, replaceText, currentProjectNamespace, out result)) return result;

            ResharperHelper.AppendLineToOutputWindow(testProject.Locks, "ERROR: Regex pattern matching failed to extract group - check your regex replace string of " + replaceText);
            throw new ApplicationException("Unexpected internal error -regex error in testcop - {0} - {1}".FormatEx(testNameSpacePattern, replaceText));
        }      
    }
}