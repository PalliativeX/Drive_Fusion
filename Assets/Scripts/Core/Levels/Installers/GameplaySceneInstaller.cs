using Core.CameraLogic;
using Core.ECS;
using Core.Gameplay;
using Core.Gameplay.Behaviours;
using Core.Infrastructure;
using Core.InputLogic;
using Core.Sound;
using Core.UI;
using Core.UI.Systems;
using Scellecs.Morpeh;
using SimpleInject;
using UnityEngine;

namespace Core.Levels
{
	public sealed class GameplaySceneInstaller : MonoInstaller
	{
		[SerializeField] private LevelBehaviour _levelBehaviour; 
		[SerializeField] private GameParentBehaviour _gameParent;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<World>().FromInstance(World.Create("Gameplay")).AsSingle();
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetComponentsDisposableInitializer>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<CurrentLevelService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LevelBehaviour>().FromInstance(_levelBehaviour).AsSingle();
			Container.BindInterfacesAndSelf<GameParentBehaviour>().FromInstance(_gameParent).AsSingle();
			Container.BindInterfacesAndSelf<GameParentInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<RoadCreator>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InteractiveItemsCreator>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<PrefabInstantiateSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplaySceneInitializer>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<SetSteeringSensitivityListener>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetPlayerActiveSystem>().FromNew().AsSingle();
			// Container.BindInterfacesAndSelf<MovePlayerWheelColliderSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InputHelper>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<HandleKeyboardInputSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MovePlayerControllerSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetVehicleWreckedDurabilitySystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateVehicleFuelSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MoveInteractiveVehiclesSystem>().FromNew().AsSingle();
			// Container.BindInterfacesAndSelf<UpdateCameraTargetSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<SetPreviousPositionSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SynchronizePositionFromTransformSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SynchronizeTransformSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetCameraFollowSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateLookAtCameraSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateCurrentVirtualCameraSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<RoadBlockCreatedSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<UpdateTriggerEnterSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateTriggerExitSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<TriggerHandler>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<HandleCollisionEventsSystem>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<MainThemePlayInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<ShowReviveUiOnStopSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<GamePauser>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<UpdateScoreSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<DurabilityChangedUpdateUiSystem>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<RemoveDurabilityChangedSystem>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<PoolViewsOnDisposeHelper>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<DestroyEntitySystem>().FromNew().AsSingle();
		}
	}
}