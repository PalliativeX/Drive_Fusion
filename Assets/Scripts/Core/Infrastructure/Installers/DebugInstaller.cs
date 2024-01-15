using Debugging;
using SimpleInject;

namespace Core.Infrastructure.Installers
{
	public sealed class DebugInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
#if DEBUG
			Container.BindInterfacesAndSelf<HotkeyProcessor>().FromNew().AsSingle();
#endif
		}
	}
}