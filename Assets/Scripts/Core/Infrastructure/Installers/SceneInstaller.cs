using Core.CameraLogic;
using Core.ECS;
using Core.Gameplay;
using Core.InputLogic;
using Core.Levels;
using SimpleInject;
using UnityEngine;

namespace Core.Infrastructure.Installers
{
	public sealed class SceneInstaller : MonoInstaller
	{
		[SerializeField] private LevelBehaviour _levelBehaviour; 
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<LevelBehaviour>().FromInstance(_levelBehaviour).AsSingle();
			
			Container.BindInterfacesAndSelf<PrefabInstantiateSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplaySceneInitializer>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<MovePlayerSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateCameraTargetSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<SynchronizeTransformSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetCameraFollowSystem>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<DestroyEntitySystem>().FromNew().AsSingle();
		}
	}
}