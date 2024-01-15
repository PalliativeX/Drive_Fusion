using System.Collections.Generic;
using SimpleInject;
using UnityEngine;

namespace Core.UI
{
	public class PanelInitializer : IInitializable
	{
		private PanelFactory _panelFactory;
		private List<IPresenter> _presenters;

		[Inject]
		public void Construct(PanelFactory panelFactory, List<IPresenter> presenters)
		{
			_panelFactory = panelFactory;
			_presenters = presenters;
		}
		
		public void Initialize()
		{
			foreach (IPresenter presenter in _presenters)
			{
				var view = _panelFactory.Create(presenter.Name);
				view.SetActive(false);
				presenter.Bind(view);
			}
		}
	}
}