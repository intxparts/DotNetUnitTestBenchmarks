using System.Collections.Generic;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MSTests
{
    [TestClass]
    public class AuthorizationService_Module1Only_Tests
    {
        private AuthorizationService _authService;
        public AuthorizationService_Module1Only_Tests()
        {
            var licenseService = new Mock<ILicenseService>();
            var licenseModules = new HashSet<LicenseModules>() { LicenseModules.MODULE1 };
            licenseService.Setup(l => l.Modules).Returns(licenseModules);

            var appContainer = new Mock<IAppContainer>();
            appContainer.Setup(c => c.LicenseService).Returns(licenseService.Object);
            _authService = new AuthorizationService();
            appContainer.Setup(c => c.AuthorizationService).Returns(_authService);
            
            _authService.Initialize(appContainer.Object);
            _authService.SetLicenseControlledFeatures();
        }

        [TestMethod]
        [DataRow(LicenseControlledFeatures.FEATURE1, true )]
        [DataRow(LicenseControlledFeatures.FEATURE2, true )]
        [DataRow(LicenseControlledFeatures.FEATURE3, true )]
        [DataRow(LicenseControlledFeatures.FEATURE4, true )]
        [DataRow(LicenseControlledFeatures.FEATURE5, true )]
        [DataRow(LicenseControlledFeatures.FEATURE6, true )]
        [DataRow(LicenseControlledFeatures.FEATURE7, true )]
        [DataRow(LicenseControlledFeatures.FEATURE8, false )]
        [DataRow(LicenseControlledFeatures.FEATURE9, false )]
        [DataRow(LicenseControlledFeatures.FEATURE10, false )]
        [DataRow(LicenseControlledFeatures.FEATURE11, false )]
        [DataRow(LicenseControlledFeatures.FEATURE12, false )]
        [DataRow(LicenseControlledFeatures.FEATURE13, false )]
        [DataRow(LicenseControlledFeatures.FEATURE14, false )]
        [DataRow(LicenseControlledFeatures.FEATURE15, false )]
        [DataRow(LicenseControlledFeatures.FEATURE16, false )]
        [DataRow(LicenseControlledFeatures.FEATURE17, false )]
        [DataRow(LicenseControlledFeatures.FEATURE18, false )]
        [DataRow(LicenseControlledFeatures.FEATURE19, false )]
        [DataRow(LicenseControlledFeatures.FEATURE20, false )]
        public void AuthorizationService_IsUserAuthorizedToAccessFeature(LicenseControlledFeatures feature, bool expected)
        {
            Assert.AreEqual(expected, _authService.IsUserAuthorizedToAccessFeature(feature));
        }
    }
}
