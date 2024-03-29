﻿using Core.ECS;
using Core.Gameplay;
using Core.InputLogic;
using Core.UI.Settings;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.UI
{
	public sealed class GameModel : IInitializable, ITickable
	{
		private readonly InputHelper _inputHelper;
		private readonly PanelController _panelController;
		private readonly Filter _playerFilter;

		private float _currentXInput = 0f;
		
		public GameModel(World world, InputHelper inputHelper, PanelController panelController)
		{
			_inputHelper = inputHelper;
			_panelController = panelController;
			_playerFilter = world.Filter
				.With<View>()
				.With<HumanPlayer>()
				.Build();
		}

		public void Initialize() { }

		public void SetXTouchInput(float xInput) => 
			_currentXInput = xInput;

		public void Tick()
		{
			foreach (var entity in _playerFilter)
			{
				_inputHelper.Set(entity, _currentXInput, 1f);
			}
		}

		public float GetCurrentFuel()
		{
			Entity player = _playerFilter.First();
			return player.GetComponent<Fuel>().Value;
		}
		
		public float GetCurrentDurability()
		{
			Entity player = _playerFilter.First();
			return player.GetComponent<Durability>().Value;
		}

		public void OnSettings()
		{
			_panelController.Open<SettingsPresenter>();
		}
	}
}