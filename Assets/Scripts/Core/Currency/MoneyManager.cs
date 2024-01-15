using System;

namespace Core.Currency
{
	public sealed class MoneyManager
	{
		public event Action<int> MoneyChanged;
		
		public int Money { get; private set; }

		public void AddMoney(int money)    => SetMoney(Money + money);

		public void RemoveMoney(int money) => SetMoney(Money - money);

		public bool HasEnoughMoney(int money) => 
			Money >= money;

		private void SetMoney(int newMoney)
		{
			Money = newMoney;
			
			MoneyChanged?.Invoke(Money);
		}
	}
}