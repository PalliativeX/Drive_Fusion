using Scellecs.Morpeh;

namespace Core.ECS
{
	public class SetComponentsDisposableInitializer : IInitializer
	{
		public World World { get; set; }

		public void OnAwake()
		{
			World.GetStash<View>().AsDisposable();
		}
		
		public void Dispose() { }
	}
}