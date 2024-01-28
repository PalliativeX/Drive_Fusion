using Core.Currency;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Core.InputLogic;
using Core.Levels;
using Scellecs.Morpeh;
using SimpleInject;
using UnityEngine;

namespace Debugging
{
#if UNITY_EDITOR
	public class HotkeyProcessor : ITickable
	{
		private readonly MoneyManager _moneyManager;
		private readonly LevelsHelper _levelHelper;
		private readonly World _world;
		private readonly GameStateMachine _gameFsm;

		private Filter _playerFilter;

		public HotkeyProcessor(MoneyManager moneyManager, LevelsHelper levelHelper, World world, GameStateMachine gameFsm)
		{
			_moneyManager = moneyManager;
			_levelHelper = levelHelper;
			_world = world;
			_gameFsm = gameFsm;

			_playerFilter = _world.Filter.With<HumanPlayer>().Build();
		}

		public void Tick()
		{
			if (Input.GetKeyDown(KeyCode.M))
				_moneyManager.AddMoney(1000);

			if (Input.GetKeyDown(KeyCode.N)) 
				_levelHelper.NextLevel();
			if (Input.GetKeyDown(KeyCode.R)) 
				_levelHelper.RestartLevel();

			if (_gameFsm.ActiveState != GameStateType.Gameplay)
				return;
			
			Entity player = _playerFilter.First();
			HandlePlayerInput(player);
		}

		private void HandlePlayerInput(Entity player)
		{
			Vector3 movementInput = Vector3.zero;
			movementInput.x = Input.GetAxis("Horizontal");
			movementInput.z = Input.GetAxis("Vertical");
			if (movementInput != Vector3.zero)
			{
				ref MovementInput input = ref player.GetComponent<MovementInput>();
				input.Value = movementInput;
			}
		}
	}
#else
	public class HotkeyProcessor {}
#endif
}