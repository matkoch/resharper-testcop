using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.UnitTestFramework;

namespace TestCop.Plugin
{
    [ZoneDefinition]
    [ZoneDefinitionConfigurableFeature("Title", "Description", IsInProductSection: false)]
    public interface ITestCopZone : IPsiLanguageZone,
        IRequire<ILanguageCSharpZone>,
        IRequire<DaemonZone>,
        IRequire<IUnitTestingZone>,
        IRequire<IUIInteractiveEnvZone>
    {
    }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ITestCopZone>
    {
    }
}
