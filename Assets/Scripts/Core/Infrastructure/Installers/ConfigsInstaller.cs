using Core.AssetManagement;
using Core.Levels.Storages;
using Core.Sound;
using SimpleInject;
using UnityEngine;

namespace Core.Infrastructure.Installers
{
	public sealed class ConfigsInstaller : MonoInstaller
	{
		[SerializeField] private CoroutineRunner _coroutineRunner;
		[SerializeField] private AssetsStorage _assets;
		[SerializeField] private LevelsStorage _levelses;
		[SerializeField] private GeneralSettings _generalSettings;
		[SerializeField] private SoundStorage _soundStorage;
		
		public override void InstallBindings()
		{
			Container.BindSelf<AssetsStorage>().FromInstance(_assets).AsSingle();
			Container.BindSelf<LevelsStorage>().FromInstance(_levelses).AsSingle();
			Container.BindSelf<GeneralSettings>().FromInstance(_generalSettings).AsSingle();
			Container.BindSelf<SoundStorage>().FromInstance(_soundStorage).AsSingle();
			Container.BindSelf<CoroutineRunner>().FromComponentInNewPrefab(_coroutineRunner).AsSingle();
		}
	}
}