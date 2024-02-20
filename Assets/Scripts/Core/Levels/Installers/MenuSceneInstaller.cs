using Core.CameraLogic;
using Core.ECS;
using Core.Gameplay;
using Core.Gameplay.Behaviours;
using Core.Infrastructure;
using Core.Menu;
using Core.Sound;
using Scellecs.Morpeh;
using SimpleInject;
using UnityEngine;

namespace Core.Levels
{
	public sealed class MenuSceneInstaller : MonoInstaller
	{
		[SerializeField] private MenuLevelBehaviour _levelBehaviour; 
		[SerializeField] private GameParentBehaviour _gameParent; 
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<World>().FromInstance(World.Create("Menu")).AsSingle();
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetComponentsDisposableInitializer>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<MenuLevelBehaviour>().FromInstance(_levelBehaviour).AsSingle();
			Container.BindInterfacesAndSelf<GameParentBehaviour>().FromInstance(_gameParent).AsSingle();
			Container.BindInterfacesAndSelf<GameParentInitializer>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<VehicleSelectionSpawnSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PrefabInstantiateSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<SynchronizePositionFromTransformSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SynchronizeTransformSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetCameraFollowSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateLookAtCameraSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<MenuThemePlayInitializer>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<PoolViewsOnDisposeHelper>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<DestroyEntitySystem>().FromNew().AsSingle();
		}
	}
}