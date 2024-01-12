using UnityEngine;

namespace Utils
{
	public static class UiExtensions
	{
		public static Vector3 WorldToCanvas(Camera camera, RectTransform canvasRect, Vector3 worldPosition)
		{
			var viewportPosition = camera.WorldToViewportPoint(worldPosition);
 
			return new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
				(viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
		}
	}
}