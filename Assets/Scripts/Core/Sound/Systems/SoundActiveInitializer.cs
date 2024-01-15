using Scellecs.Morpeh;

namespace Core.Sound
{
	public class SoundActiveInitializer : IInitializer
	{
		public World World { get; set; }

		public void OnAwake()
		{
			var entity = World.CreateEntity();
			entity.SetComponent(new SoundActive { Value = true });
		}
		
		public void Dispose() { }
	}
}