using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure.GameFsm;
using Core.Integrations.SaveSystem;
using Core.Levels.Storages;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Levels
{
	public class LevelsHelper
	{
		private readonly World _world;
		private readonly IGameStateMachine _stateMachine;
		private readonly LevelsStorage _levels;
		private readonly SaveService _save;

		private readonly Filter _filter;

		public LevelsHelper(World world, GameStateMachine stateMachine, LevelsStorage levels, SaveService save)
		{
			_world = world;
			_stateMachine = stateMachine;
			_levels = levels;
			_save = save;

			_filter = world.Filter.With<CurrentLevel>().Build();
		}

		public Entity Load(SaveData data)
		{
			var entity = _world.CreateEntity();
			entity.SetComponent(new CurrentLevel { Value = 0 });
			entity.SetComponent(new LevelScoreRecords
			{
				Dictionary = data.HighestScores.ToDictionary(d => d.Level, d => d.Score)
			});
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

		public float GetCurrentLevelScoreRecord()
		{
			var entity = _filter.First();
			var currentLevel = entity.GetComponent<CurrentLevel>();
			ref var scoreRecords = ref entity.GetComponent<LevelScoreRecords>();
			if (scoreRecords.Dictionary.TryGetValue(currentLevel.Index, out float score))
				return score;
            return 0;
		}

		public void SetCurrentLevelScoreRecord(float score)
		{
			var entity = _filter.First();
			var currentLevel = entity.GetComponent<CurrentLevel>();
			ref var scoreRecords = ref entity.GetComponent<LevelScoreRecords>();
			scoreRecords.Dictionary[currentLevel.Value] = score;

			SaveScores(scoreRecords.Dictionary);
		}

		private void SaveScores(Dictionary<int, float> scores)
		{
			var scoresSave = _save.SaveData.HighestScores;
			scoresSave.Clear();
			
			foreach ((int level, float value) in scores) 
				scoresSave.Add(new LevelScoreData(level, value));

			_save.Save();
		}
	}
}