using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Debugging;
using SimpleInject;
using UnityEngine;
using Utils;

namespace Core.Integrations.SaveSystem
{
	public class SaveService
	{
		private const string SaveDataKey = "SaveData";

		private List<ILoadable> _loadable;
		
		public SaveData SaveData { get; private set; }
		
		public bool IsFirstSession { get; private set; } 
		
		public bool IsInitialized { get; private set; }
		
		public event Action Initialized;
		
		[Inject]
		public void Construct(List<ILoadable> loadable) => 
			_loadable = loadable;

		public void Initialize()
		{
			if (Platform.Instance.IsYandexGames())
			{
				LoadPlayerProgressDataExternal();
			}
			else
			{
				SetPlayerProgressData(PlayerPrefs.GetString(SaveDataKey));
			}
		}

		// TODO: Save leaderboard!
		public void Save()
		{
			if (!IsInitialized)
				return;
			
			string data = JsonUtility.ToJson(SaveData);
			if (Platform.Instance.IsYandexGames())
			{
				SavePlayerProgressDataExternal(data);
			}
			else
			{
				PlayerPrefs.SetString(SaveDataKey, data);
				PlayerPrefs.Save();
			}
		}

		public void InitializeLoadables()
		{
			foreach (var loadable in _loadable) 
				loadable.Load(SaveData);
		}

		public void Clear()
		{
			SaveData = new SaveData();
			Save();
		}

		public void SetPlayerProgressData(string dataJson)
		{
			ConsoleLogger.Instance.Log("Setting player progress data");
			
			if (string.IsNullOrEmpty(dataJson))
			{
				SaveData = new SaveData { IsFirstSession = true };
                IsFirstSession = true;
			}
			else
			{
#if DEBUG
				ConsoleLogger.Instance.Log("Has Player Progress: " + dataJson);
#endif
				SaveData = JsonUtility.FromJson<SaveData>(dataJson);
				SaveData.IsFirstSession = false;
				IsFirstSession = false; 
			}

			Debug.Log("IsFirstSession: " + IsFirstSession);
			
			IsInitialized = true;
			Initialized?.Invoke();
		}
		
		[DllImport("__Internal")]
		private static extern void SavePlayerProgressDataExternal(string data);
		
		[DllImport("__Internal")]
		private static extern void LoadPlayerProgressDataExternal();
	}
}