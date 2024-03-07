using System;
using UniRx;
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
		
		public static IDisposable OnClickSubscribeDisposable(this Button self, Action callback) => 
			self.OnClickAsObservable().Subscribe(_ => callback());

		public static IDisposable OnClickSubscribeDisposable<T>(this Button self, Action<T> callback, T value) => 
			self.OnClickAsObservable().Subscribe(_ => callback(value));
		
		public static IDisposable OnValueChangedSubscribeDisposable(this Slider self, Action<float> callback) => 
			self.OnValueChangedAsObservable().Subscribe(callback);
	}
}