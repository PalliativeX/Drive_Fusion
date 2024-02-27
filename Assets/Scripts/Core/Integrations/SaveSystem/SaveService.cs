using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Debugging;
using Utils;

namespace Core.Integrations.SaveSystem
{
	public class SaveService
	{
		private readonly List<ILoadable> _loadable;
		
		public bool IsFirstSession { get; private set; } 
		
		public bool IsInitialized { get; private set; }
		
		public event Action Initialized;
		
		public SaveService(List<ILoadable> loadable) => 
			_loadable = loadable;

		public void Initialize()
		{
			if (Platform.IsYandexGames())
			{
				LoadPlayerProgressDataExternal();
			}
		}

		public void Load()
		{
			
		}
		
		[DllImport("__Internal")]
		private static extern void SavePlayerProgressDataExternal(string data);
		
		[DllImport("__Internal")]
		private static extern void LoadPlayerProgressDataExternal();
	}
}