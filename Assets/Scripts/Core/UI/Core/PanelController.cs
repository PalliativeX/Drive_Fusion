using System;
using System.Collections.Generic;
using SimpleInject;

namespace Core.UI
{
	public class PanelController
	{
		private readonly HashSet<IPresenter> _openedPanels = new HashSet<IPresenter>();
		
		private Dictionary<Type, IPresenter> _presenters;

		[Inject]
		public void Construct(List<IPresenter> presenters)
		{
			_presenters = new Dictionary<Type, IPresenter>();
			foreach (var presenter in presenters) 
				_presenters[presenter.GetType()] = presenter;
		}

		public void Open<T>() where T : IPresenter
		{
			var presenter = _presenters[typeof(T)];
			if (!presenter.IsActive)
				presenter.Open();

			_openedPanels.Add(presenter);
		}

		public void Close<T>() where T : IPresenter
		{
			var presenter = _presenters[typeof(T)];
			if (presenter.IsActive)
				presenter.Close();
			
			_openedPanels.Remove(presenter);
		}

		public void Dispose()
		{
			foreach (IPresenter openedPanel in _openedPanels) 
				openedPanel.Close();
			_openedPanels.Clear();
		}
	}
}