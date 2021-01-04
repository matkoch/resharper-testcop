// --
// -- TestCop http://github.com/testcop
// -- License http://github.com/testcop/license
// -- Copyright 2020
// --
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;

namespace TestCop.Plugin.Highlighting
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
    [RegisterConfigurableSeverity(
        SeverityId,
        null, Highlighter.HighlightingGroup,
        "Test method should be public",
        "TestCop : Method with testing attributes should be public",
        Severity.ERROR)]
    public class MethodShouldBePublicWarning : AbstractShouldBePublicWarning
    {
        internal const string SeverityId = "MethodShouldBePublic";

        public MethodShouldBePublicWarning(string attributeName, IAccessRightsOwnerDeclaration declaration)
            : base(SeverityId, string.Format("Methods with [{0}] must be public.", attributeName), declaration)
        {
        }       
    }
}