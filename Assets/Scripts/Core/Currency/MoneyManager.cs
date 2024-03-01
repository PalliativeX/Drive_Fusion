using System;
using Core.Integrations.SaveSystem;

namespace Core.Currency
{
	public sealed class MoneyManager : ILoadable
	{
		private readonly SaveService _save;
		
		public int Money { get; private set; }
		
		public event Action<int> MoneyChanged;

		public MoneyManager(SaveService save) => _save = save;

		public void AddMoney(int money)    => SetMoney(Money + money);

		public void RemoveMoney(int money) => SetMoney(Money - money);

		public bool HasEnoughMoney(int money) => 
			Money >= money;

		private void SetMoney(int newMoney)
		{
			Money = newMoney;
			
			MoneyChanged?.Invoke(Money);

			_save.SaveData.Money = Money;
			_save.Save();
		}

		public void Load(SaveData data) => Money = data.Money;
	}
}