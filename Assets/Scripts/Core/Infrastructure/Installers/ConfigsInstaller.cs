﻿using Core.AssetManagement;
using Core.Gameplay;
using Core.InputLogic;
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
		[SerializeField] private LevelsStorage _levels;
		[SerializeField] private GeneralSettings _generalSettings;
		[SerializeField] private SoundStorage _soundStorage;
		[SerializeField] private InputConfig _inputConfig;
		[SerializeField] private VehiclesStorage _vehiclesStorage;
		
		public override void InstallBindings()
		{
			Container.BindSelf<AssetsStorage>().FromInstance(_assets).AsSingle();
			Container.BindSelf<LevelsStorage>().FromInstance(_levels).AsSingle();
			Container.BindSelf<GeneralSettings>().FromInstance(_generalSettings).AsSingle();
			Container.BindSelf<SoundStorage>().FromInstance(_soundStorage).AsSingle();
			Container.BindSelf<CoroutineRunner>().FromComponentInNewPrefab(_coroutineRunner).AsSingle();
			Container.BindSelf<InputConfig>().FromInstance(_inputConfig).AsSingle();
			Container.BindSelf<VehiclesStorage>().FromInstance(_vehiclesStorage).AsSingle();
		}
	}
}