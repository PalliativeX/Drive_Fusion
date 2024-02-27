using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Utils;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateScoreSystem : ISystem
	{
		private const float ScoreDistanceMultiplier = 1f;
		
		private readonly CurrentLevelService _currentLevelService;
		
		private Filter _filter;
		
		public World World { get; set; }

		public UpdateScoreSystem(CurrentLevelService currentLevelService) => 
			_currentLevelService = currentLevelService;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<Position>()
				.With<PreviousPosition>()
				.With<HumanPlayer>()
				.With<Active>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref Vector3 position = ref entity.GetComponent<Position>().Value;
				ref Vector3 previousPosition = ref entity.GetComponent<PreviousPosition>().Value;

				float difference = Vector3.Distance(position, previousPosition);
				if (!difference.IsZero()) 
					_currentLevelService.AddScore(difference * ScoreDistanceMultiplier);
			}
		}
		
		public void Dispose() { }
	}
}