using Core.AssetManagement;
using Core.Currency;
using Core.Gameplay;
using Core.InputLogic;
using Core.Integrations;
using Core.Levels.Storages;
using Core.Localization;
using Core.Sound;
using SimpleInject;
using UnityEngine;

namespace Core.Infrastructure.Installers
{
	public sealed class ConfigsInstaller : MonoInstaller
	{
		[SerializeField] private CoroutineRunner _coroutineRunner;
		[SerializeField] private AssetsStorage _assets;
		[SerializeField] private LevelsStorage _levels;
		[SerializeField] private GeneralSettings _generalSettings;
		[SerializeField] private SoundStorage _soundStorage;
		[SerializeField] private InputConfig _inputConfig;
		[SerializeField] private VehiclesStorage _vehiclesStorage;
		[SerializeField] private RoadsConfig _roadsConfig;
		[SerializeField] private RateUsConfig _rateUs;
		[SerializeField] private LocaleStorage _localeStorage;
		[SerializeField] private CurrencyConfig _currencyConfig;
		
		public override void InstallBindings()
		{
			Container.BindSelf<AssetsStorage>().FromInstance(_assets).AsSingle();
			Container.BindSelf<LevelsStorage>().FromInstance(_levels).AsSingle();
			Container.BindSelf<GeneralSettings>().FromInstance(_generalSettings).AsSingle();
			Container.BindSelf<SoundStorage>().FromInstance(_soundStorage).AsSingle();
			Container.BindSelf<CoroutineRunner>().FromComponentInNewPrefab(_coroutineRunner).AsSingle();
			Container.BindSelf<InputConfig>().FromInstance(_inputConfig).AsSingle();
			Container.BindSelf<VehiclesStorage>().FromInstance(_vehiclesStorage).AsSingle();
			Container.BindSelf<RoadsConfig>().FromInstance(_roadsConfig).AsSingle();
			Container.BindSelf<RateUsConfig>().FromInstance(_rateUs).AsSingle();
			Container.BindSelf<LocaleStorage>().FromInstance(_localeStorage).AsSingle();
			Container.BindSelf<CurrencyConfig>().FromInstance(_currencyConfig).AsSingle();
		}
	}
}