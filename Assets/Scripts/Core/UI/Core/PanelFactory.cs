using Core.AssetManagement;
using UnityEngine;

namespace Core.UI
{
	public class PanelFactory
	{
		private readonly UiParent _parent;
		private readonly IAssetProvider _assetProvider;

		public PanelFactory(UiParent parent, AssetProvider assetProvider)
		{
			_parent = parent;
			_assetProvider = assetProvider;
		}

		public GameObject Create(string name)
		{
			(GameObject panel, bool isPooled) = _assetProvider.LoadAsset(name);
			panel.transform.SetParent(_parent.Transform, false);
			return panel;
		}
	}
}