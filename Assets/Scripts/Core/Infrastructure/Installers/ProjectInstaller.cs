﻿using Core.AssetManagement;
using Core.SceneManagement.Services;
using Core.Sound;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Infrastructure.Installers
{
	public sealed class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindSelf<World>().FromInstance(World.Default).AsSingle();
			
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<AssetPool>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<AssetProvider>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SceneLoader>().FromNew().AsSingle();
			
			InstallSound();
		}

		private void InstallSound()
		{
			Container.BindInterfacesAndSelf<AudioSourceCreator>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MusicPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeClipSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MainThemePlayInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PlayInitializedClipSystem>().FromNew().AsSingle();
		}
	}
}