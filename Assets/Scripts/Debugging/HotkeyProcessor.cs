using Core.Currency;
using SimpleInject;
using UnityEngine;

namespace Debugging
{
#if UNITY_EDITOR
	public class HotkeyProcessor : ITickable
	{
		private readonly MoneyManager _moneyManager;

		public HotkeyProcessor(MoneyManager moneyManager)
		{
			_moneyManager = moneyManager;
		}

		public void Tick()
		{
			if (Input.GetKeyDown(KeyCode.M))
				_moneyManager.AddMoney(1000);
		}
	}
#else
	public class HotkeyProcessor {}
#endif
}