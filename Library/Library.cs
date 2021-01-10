using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
	public interface IAppContainer
	{
		ILicenseService LicenseService { get; }
		IAuthorizationService AuthorizationService { get; }
	}

	public class AppContainer : IAppContainer
	{
		private Lazy<AppContainer> _appContainerInstance = new Lazy<AppContainer>(
			() => {
				return new AppContainer();
			},
			isThreadSafe: true
		);

		public ILicenseService LicenseService { get { return new LicenseService(); } }
		public IAuthorizationService AuthorizationService
		{
			get
			{
				var authService = new AuthorizationService();
				authService.Initialize(_appContainerInstance.Value);
				return authService;
			}
		}
	}

	public interface IServiceWithDependencies
	{
		void Initialize(IAppContainer container);
	}

	public enum LicenseModules
	{
		MODULE1,
		MODULE2,
		MODULE3,
		MODULE4,
		MODULE5,
		MODULE6,
		MODULE7,
		MODULE8,
		MODULE9,
		MODULE10,
	}

	public interface ILicenseService
	{
		void LoadLicense();
		bool HasLicenseModule(LicenseModules module);
	}

	public class LicenseService : ILicenseService
	{
		private HashSet<LicenseModules> _licenseModules;
		
		public LicenseService()
		{
			_licenseModules = new HashSet<LicenseModules>();
		}

		public void LoadLicense()
		{
			string [] licenseFileContents = null;
			try
			{
				licenseFileContents = File.ReadAllLines("license.txt");
			}
			catch (System.Exception)
			{
			}
			if (licenseFileContents == null)
				return;

			foreach (var s in licenseFileContents)
			{
				switch (s)
				{
					case "MODULE1": _licenseModules.Add(LicenseModules.MODULE1); break;
					case "MODULE2": _licenseModules.Add(LicenseModules.MODULE2); break;
					case "MODULE3": _licenseModules.Add(LicenseModules.MODULE3); break;
					case "MODULE4": _licenseModules.Add(LicenseModules.MODULE4); break;
					case "MODULE5": _licenseModules.Add(LicenseModules.MODULE5); break;
					case "MODULE6": _licenseModules.Add(LicenseModules.MODULE6); break;
					case "MODULE7": _licenseModules.Add(LicenseModules.MODULE7); break;
					case "MODULE8": _licenseModules.Add(LicenseModules.MODULE8); break;
					case "MODULE9": _licenseModules.Add(LicenseModules.MODULE9); break;
					case "MODULE10": _licenseModules.Add(LicenseModules.MODULE10); break;
				}
			}
		}

		public bool HasLicenseModule(LicenseModules module)
		{
			return _licenseModules.Contains(module);
		}
	}

	public enum LicenseControlledFeatures
	{
		FEATURE1,
		FEATURE2,
		FEATURE3,
		FEATURE4,
		FEATURE5,
		FEATURE6,
		FEATURE7,
		FEATURE8,
		FEATURE9,
		FEATURE10,
		FEATURE11,
		FEATURE12,
		FEATURE13,
		FEATURE14,
		FEATURE15,
		FEATURE16,
		FEATURE17,
		FEATURE18,
		FEATURE19,
		FEATURE20
	}

	public interface IAuthorizationService : IServiceWithDependencies
	{
		bool IsUserAuthorizedToAccessFeature(LicenseControlledFeatures feature);
		void SetLicenseControlledFeatures();
	}

	public class AuthorizationService : IAuthorizationService
	{
		private HashSet<LicenseControlledFeatures> _authorizedFeatures;

		public AuthorizationService()
		{
			_authorizedFeatures = new HashSet<LicenseControlledFeatures>();
		}

		private void AddLicenseControlledFeature(LicenseControlledFeatures feature)
		{
			_authorizedFeatures.Add(feature);
		}

		public void SetLicenseControlledFeatures()
		{
			if (_licenseService.HasLicenseModule(LicenseModules.MODULE1))
			{
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE1);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE2);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE3);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE4);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE5);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE6);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE7);
			}

			if (_licenseService.HasLicenseModule(LicenseModules.MODULE2))
			{
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE8);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE9);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE10);
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE11);
			}

			if (_licenseService.HasLicenseModule(LicenseModules.MODULE3))
			{
				AddLicenseControlledFeature(LicenseControlledFeatures.FEATURE12);
			}
		}

		public bool IsUserAuthorizedToAccessFeature(LicenseControlledFeatures feature)
		{
			return _authorizedFeatures.Contains(feature);
		}

		private ILicenseService _licenseService;
		public void Initialize(IAppContainer container)
		{
			_licenseService = container.LicenseService;
		}
	}
}
