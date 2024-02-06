namespace Core.UI.PanelGeneration
{
	public sealed class PanelGenerationData
	{
		public readonly string Name;
		public readonly string Path;
		public readonly bool CreateModel;
		public readonly bool CreatePresenter;
		public readonly bool CreateView;

		public PanelGenerationData(string name, string path, bool createModel, bool createPresenter, bool createView)
		{
			Name = name;
			Path = path;
			CreateModel = createModel;
			CreatePresenter = createPresenter;
			CreateView = createView;
		}
	}
}