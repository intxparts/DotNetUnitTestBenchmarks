using System;
using System.Collections.Generic;
using Library;
using Moq;
using Testing;

namespace FastTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestConsole.RunTests(() => {
                AuthorizationService_Module1Only_Tests();
            });
        }

        static void AuthorizationService_Module1Only_Tests()
        {
            var licenseService = new Mock<ILicenseService>();
            var licenseModules = new HashSet<LicenseModules>() { LicenseModules.MODULE1 };
            licenseService.Setup(l => l.Modules).Returns(licenseModules);

            var appContainer = new Mock<IAppContainer>();
            appContainer.Setup(c => c.LicenseService).Returns(licenseService.Object);
            var authService = new AuthorizationService();
            authService.Initialize(appContainer.Object);
            authService.SetLicenseControlledFeatures();

            var testCases = new Dictionary<LicenseControlledFeatures, bool>()
            {
                { LicenseControlledFeatures.FEATURE1, true },
                { LicenseControlledFeatures.FEATURE2, true },
                { LicenseControlledFeatures.FEATURE3, true },
                { LicenseControlledFeatures.FEATURE4, true },
                { LicenseControlledFeatures.FEATURE5, true },
                { LicenseControlledFeatures.FEATURE6, true },
                { LicenseControlledFeatures.FEATURE7, true },
                { LicenseControlledFeatures.FEATURE8, false },
                { LicenseControlledFeatures.FEATURE9, false },
                { LicenseControlledFeatures.FEATURE10, false },
                { LicenseControlledFeatures.FEATURE11, false },
                { LicenseControlledFeatures.FEATURE12, false },
                { LicenseControlledFeatures.FEATURE13, false },
                { LicenseControlledFeatures.FEATURE14, false },
                { LicenseControlledFeatures.FEATURE15, false },
                { LicenseControlledFeatures.FEATURE16, false },
                { LicenseControlledFeatures.FEATURE17, false },
                { LicenseControlledFeatures.FEATURE18, false },
                { LicenseControlledFeatures.FEATURE19, false },
                { LicenseControlledFeatures.FEATURE20, false },
            };

            foreach (var t in testCases)
            {
                Test.Run($"AuthorizationService.IsUserAuthorizedToAccessFeature({Enum.GetName(typeof(LicenseControlledFeatures), t.Key)})", () => {
                    Assert.AreEqual(t.Value, authService.IsUserAuthorizedToAccessFeature( (LicenseControlledFeatures) t.Key));
                });
            }
        }
    }
}
