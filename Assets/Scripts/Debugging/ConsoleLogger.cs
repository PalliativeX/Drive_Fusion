using System.Runtime.InteropServices;
using UnityEngine;
using Utils;

namespace Debugging
{
	public sealed class ConsoleLogger : Singleton<ConsoleLogger>
	{
		[DllImport("__Internal")]
		private static extern void LogExternal(string text);

		public void Log(string text)
		{
			// if (Platform.Instance.IsYandexGames())
				// LogExternal(text);
			// else
				Debug.Log(text);
		}
	}
}