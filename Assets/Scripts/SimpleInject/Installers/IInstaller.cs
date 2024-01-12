namespace SimpleInject
{
	public interface IInstaller
	{
		void Initialize(DiContainer container);
		void InstallBindings();
	}
}