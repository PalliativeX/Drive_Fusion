using Core.Currency;
using Core.Levels;
using SimpleInject;
using UnityEngine;

namespace Debugging
{
#if UNITY_EDITOR
	public class HotkeyProcessor : ITickable
	{
		private readonly MoneyManager _moneyManager;
		private readonly LevelsHelper _levelHelper;

		public HotkeyProcessor(MoneyManager moneyManager, LevelsHelper levelHelper)
		{
			_moneyManager = moneyManager;
			_levelHelper = levelHelper;
		}

		public void Tick()
		{
			if (Input.GetKeyDown(KeyCode.M))
				_moneyManager.AddMoney(1000);

			if (Input.GetKeyDown(KeyCode.N)) 
				_levelHelper.NextLevel();
			if (Input.GetKeyDown(KeyCode.R)) 
				_levelHelper.RestartLevel();
		}
	}
#else
	public class HotkeyProcessor {}
#endif
}