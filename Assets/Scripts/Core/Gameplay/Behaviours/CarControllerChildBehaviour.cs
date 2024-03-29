﻿using Core.ECS;
using DavidJalbert;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class CarControllerChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private TinyCarController _controller;
		[SerializeField] private TinyCarVisuals _visuals;
		[SerializeField] private VehicleConfig _config;

		public override void Link(Entity entity)
		{
			_visuals.BlockEffects = false;
			
			entity.SetComponent(new CarController { Reference = _controller });
			entity.SetComponent(new VehicleConfigComponent { Reference = _config });
			entity.SetComponent(new Fuel { Value = 1f });
			entity.SetComponent(new Durability { Value = 1f });
			entity.SetComponent(new CurrentSteeringFactor { Value = 0f, Direction = SteeringDirection.None });

			_controller.Clear(Vector3.zero, Quaternion.identity);
		}

		public override void Unlink(Entity entity)
		{
			_visuals.BlockEffects = true;
			
			_controller.Clear(Vector3.zero, Quaternion.identity);
			_visuals.StopEffects();
		}
	}
}