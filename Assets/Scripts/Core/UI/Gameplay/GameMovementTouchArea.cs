using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
	public sealed class GameMovementTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private MovementTouchAreaType _type;
		
		public event Action<MovementTouchAreaType> PointerDown;
		public event Action PointerUp;
		
		public void OnPointerDown(PointerEventData eventData)
		{
			PointerDown?.Invoke(_type);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			PointerUp?.Invoke();
		}
	}
}