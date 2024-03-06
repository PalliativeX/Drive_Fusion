using System;
using Core.Currency;
using Core.ECS;
using Core.Sound;
using JetBrains.Annotations;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay
{
	public class TriggerHandler
	{
		private readonly World _globalWorld;
		private readonly RoadCreator _roadCreator;
		private readonly CurrentLevelService _currentLevel;
		private readonly Filter _instanceFilter;

		public TriggerHandler(
			GlobalWorld globalWorld,
			World world,
			RoadCreator roadCreator,
			CurrentLevelService currentLevel
		)
		{
			_globalWorld = globalWorld;
			_roadCreator = roadCreator;
			_currentLevel = currentLevel;
			_instanceFilter = world.Filter.With<InstanceId>().Build();
		}

		public void OnEnter(Entity entity, int triggerId)
		{
			Entity triggerEntity = GetTriggerEntity(triggerId);
			// Debug.Log("Enter: " + triggerEntity);
			if (triggerEntity == null)
				return;

			if (triggerEntity.Has<Interactive>())
			{
				var interactiveType = triggerEntity.GetComponent<Interactive>().Type;
				switch (interactiveType)
				{
					case InteractiveType.Coins:
						var interactiveCoins = triggerEntity.GetComponent<InteractiveCoins>();
						_currentLevel.AddEarnings(interactiveCoins.Value);
						break;
					case InteractiveType.Fuel:
						var fuelAmount = triggerEntity.GetComponent<InteractiveValue>();
						ref var fuel = ref entity.GetComponent<Fuel>();
						fuel.Value = Mathf.Clamp01(fuel.Value + fuelAmount.Value);
						break;
					case InteractiveType.Repair:
						var durabilityAmount = triggerEntity.GetComponent<InteractiveValue>();
						ref var durability = ref entity.GetComponent<Durability>();
						durability.Value = Mathf.Clamp01(durability.Value + durabilityAmount.Value);
						break;
					case InteractiveType.Boost:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				_globalWorld.CreateSound(SoundId.CollectInteractive, SoundType.Sound);

				triggerEntity.SetComponent(new Destroyed());
				return;
			}

			if (triggerEntity.Has<RoadBlock>())
			{
				if (entity.Has<CurrentRoadBlock>())
				{
					ref var currentBlock = ref entity.GetComponent<CurrentRoadBlock>();
					currentBlock.Value = triggerEntity.ID;
				}
				else
					entity.SetComponent(new CurrentRoadBlock { Value = triggerEntity.ID });
			}
		}

		public void OnExit(Entity entity, int triggerId)
		{
			Entity triggerEntity = GetTriggerEntity(triggerId);
			if (triggerEntity == null)
				return;

			if (!triggerEntity.Has<RoadBlock>())
				return;

			_roadCreator.HandleRoadCreation(triggerEntity);
		}

		[CanBeNull]
		private Entity GetTriggerEntity(int triggerId)
		{
			foreach (var entity in _instanceFilter)
				if (entity.GetComponent<InstanceId>().Value == triggerId)
					return entity;

			return null;
		}
	}
}