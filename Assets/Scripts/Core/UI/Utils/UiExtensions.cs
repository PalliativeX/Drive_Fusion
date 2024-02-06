using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI
{
	public static class UiExtensions
	{
		public static void Subscribe(this Button button, UnityAction action) => 
			button.onClick.AddListener(action);
		
		public static void Unsubscribe(this Button button, UnityAction action) => 
			button.onClick.RemoveListener(action);
	}
}