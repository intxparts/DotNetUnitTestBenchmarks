using System;
using System.Collections.Generic;
using Library;
using Moq;
using Xunit;

namespace XUnitTests
{
    public class IAppContainerFixture
    {
        public IAppContainer AppContainer { get; private set; }
        public IAppContainerFixture()
        {
            var licenseService = new Mock<ILicenseService>();
            var licenseModules = new HashSet<LicenseModules>() { LicenseModules.MODULE1 };
            licenseService.Setup(l => l.Modules).Returns(licenseModules);

            var appContainer = new Mock<IAppContainer>();
            appContainer.Setup(c => c.LicenseService).Returns(licenseService.Object);
            var authService = new AuthorizationService();
            appContainer.Setup(c => c.AuthorizationService).Returns(authService);

            authService.Initialize(appContainer.Object);
            authService.SetLicenseControlledFeatures();

            AppContainer = appContainer.Object;
        }
    }

    public class AuthorizationService_Module1Only_Tests : IClassFixture<IAppContainerFixture>
    {
        private IAppContainerFixture _fixture;
        public AuthorizationService_Module1Only_Tests(IAppContainerFixture fixture)
        {
            this._fixture = fixture;
        }

        [Theory]
        [InlineData(LicenseControlledFeatures.FEATURE1, true )]
        [InlineData(LicenseControlledFeatures.FEATURE2, true )]
        [InlineData(LicenseControlledFeatures.FEATURE3, true )]
        [InlineData(LicenseControlledFeatures.FEATURE4, true )]
        [InlineData(LicenseControlledFeatures.FEATURE5, true )]
        [InlineData(LicenseControlledFeatures.FEATURE6, true )]
        [InlineData(LicenseControlledFeatures.FEATURE7, true )]
        [InlineData(LicenseControlledFeatures.FEATURE8, false )]
        [InlineData(LicenseControlledFeatures.FEATURE9, false )]
        [InlineData(LicenseControlledFeatures.FEATURE10, false )]
        [InlineData(LicenseControlledFeatures.FEATURE11, false )]
        [InlineData(LicenseControlledFeatures.FEATURE12, false )]
        [InlineData(LicenseControlledFeatures.FEATURE13, false )]
        [InlineData(LicenseControlledFeatures.FEATURE14, false )]
        [InlineData(LicenseControlledFeatures.FEATURE15, false )]
        [InlineData(LicenseControlledFeatures.FEATURE16, false )]
        [InlineData(LicenseControlledFeatures.FEATURE17, false )]
        [InlineData(LicenseControlledFeatures.FEATURE18, false )]
        [InlineData(LicenseControlledFeatures.FEATURE19, false )]
        [InlineData(LicenseControlledFeatures.FEATURE20, false )]
        public void AuthorizationService_IsUserAuthorizedToAccessFeature_Module1Only(LicenseControlledFeatures feature, bool expected)
        {
            Assert.Equal(expected, _fixture.AppContainer.AuthorizationService.IsUserAuthorizedToAccessFeature(feature));
        }
    }
}
