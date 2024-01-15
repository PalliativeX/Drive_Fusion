using UnityEngine;

namespace Core.UI
{
	public interface IPresenter
	{
		string Name { get; }
		bool IsActive { get; }
		
		void Bind(GameObject view);
		void Open();
		void Close();
		void SetAsLast();
	}
}