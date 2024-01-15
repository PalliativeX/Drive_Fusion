using UnityEngine;

namespace Core.UI
{
	public abstract class APresenter<T> : IPresenter where T : BaseView
	{
		protected T View;
		
		public abstract string Name { get; }
		
		public bool IsActive { get; private set; }

		public void Bind(GameObject view)
		{
			View = view.GetComponent<T>();
		}

		public void Open()
		{
			IsActive = true;
			
			View.gameObject.SetActive(true);
			OnShow();
		}

		public void Close()
		{
			IsActive = false;
			
			View.gameObject.SetActive(false);
			OnClose();
		}

		public void SetAsLast() => View.transform.SetAsLastSibling();
		
		protected virtual void OnShow() { }
		
		protected virtual void OnClose() { }
	}
}