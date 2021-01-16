using System.Collections.Generic;
using Library;
using Moq;
using NUnit.Framework;

namespace NUnitTests
{
    public class AuthorizationService_Module1Only_Tests
    {
        private AuthorizationService _authService;

        [SetUp]
        public void Setup()
        {
            var licenseService = new Mock<ILicenseService>();
            var licenseModules = new HashSet<LicenseModules>() { LicenseModules.MODULE1 };
            licenseService.Setup(l => l.Modules).Returns(licenseModules);

            var appContainer = new Mock<IAppContainer>();
            appContainer.Setup(c => c.LicenseService).Returns(licenseService.Object);
            _authService = new AuthorizationService();
            _authService.Initialize(appContainer.Object);
            _authService.SetLicenseControlledFeatures();
        }

        [TestCase(LicenseControlledFeatures.FEATURE1, true )]
        [TestCase(LicenseControlledFeatures.FEATURE2, true )]
        [TestCase(LicenseControlledFeatures.FEATURE3, true )]
        [TestCase(LicenseControlledFeatures.FEATURE4, true )]
        [TestCase(LicenseControlledFeatures.FEATURE5, true )]
        [TestCase(LicenseControlledFeatures.FEATURE6, true )]
        [TestCase(LicenseControlledFeatures.FEATURE7, true )]
        [TestCase(LicenseControlledFeatures.FEATURE8, false )]
        [TestCase(LicenseControlledFeatures.FEATURE9, false )]
        [TestCase(LicenseControlledFeatures.FEATURE10, false )]
        [TestCase(LicenseControlledFeatures.FEATURE11, false )]
        [TestCase(LicenseControlledFeatures.FEATURE12, false )]
        [TestCase(LicenseControlledFeatures.FEATURE13, false )]
        [TestCase(LicenseControlledFeatures.FEATURE14, false )]
        [TestCase(LicenseControlledFeatures.FEATURE15, false )]
        [TestCase(LicenseControlledFeatures.FEATURE16, false )]
        [TestCase(LicenseControlledFeatures.FEATURE17, false )]
        [TestCase(LicenseControlledFeatures.FEATURE18, false )]
        [TestCase(LicenseControlledFeatures.FEATURE19, false )]
        [TestCase(LicenseControlledFeatures.FEATURE20, false )]
        public void AuthorizationService_IsUserAuthorizedToAccessFeature_Module1Only(LicenseControlledFeatures feature, bool expected)
        {
            Assert.AreEqual(expected, _authService.IsUserAuthorizedToAccessFeature(feature));
        }
    }
}
