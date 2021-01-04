using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace TestCop.Plugin.Tests
{    
    [ZoneDefinition]
    public interface ITestCopTestZone : ITestsEnvZone, IRequire<ITestCopZone>
    {}

    [SetUpFixture]
    public class TestEnvironmentAssembly : ExtensionTestEnvironmentAssembly<ITestCopTestZone>
    {
    }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ITestCopTestZone>, IRequire<ISinceClr4HostZone> { }
}
