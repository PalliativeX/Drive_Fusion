﻿using Core.Infrastructure.GameFsm;
using Core.Levels.Storages;
using Core.Sound;
using Scellecs.Morpeh;

namespace Core.Levels
{
	public class LevelsHelper
	{
		private readonly World _world;
		private readonly IGameStateMachine _stateMachine;
		private readonly LevelsStorage _levels;

		private readonly Filter _filter;

		public LevelsHelper(World world, GameStateMachine stateMachine, LevelsStorage levels)
		{
			_world = world;
			_stateMachine = stateMachine;
			_levels = levels;

			_filter = world.Filter.With<CurrentLevel>().Build();
		}

		public Entity Initialize()
		{
			var entity = _world.CreateEntity();
			entity.SetComponent(new CurrentLevel { Value = 0 });
			return entity;
		}
		
		public void Play(int level)
		{
			var entity = _filter.First();
			ref var currentLevel = ref entity.GetComponent<CurrentLevel>();
			currentLevel.Value = level;
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}

		public void RestartLevel()
		{
			var entity = _filter.First();
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}
		
		public void LoadMenu()
		{
			var entity = _filter.First();
			ref var currentLevel = ref entity.GetComponent<CurrentLevel>();
			currentLevel.Value = 0;

			entity.SetComponent(new RequestMenuLoad());
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}

		public LevelEntry GetCurrentEntry()
		{
			var entity = _filter.First();
			var currentLevel = entity.GetComponent<CurrentLevel>();
			return _levels.LevelEntries[currentLevel.Index];
		}
	}
}