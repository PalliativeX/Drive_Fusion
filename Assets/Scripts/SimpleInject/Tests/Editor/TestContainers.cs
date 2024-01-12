using NUnit.Framework;

namespace SimpleInject.Tests.Editor
{
	public sealed class TestContainers
	{
		[Test]
		public void TestParentContainerResolveFromSubContainer()
		{
			var parentContainer = new DiContainer();
			parentContainer.BindSelf<A>().FromNew().AsSingle();
			
			var sceneContainer = new DiContainer();
			sceneContainer.BindSelf<Child>().FromNew().AsSingle();

			parentContainer.Fill(null);
			parentContainer.ResolveBindings();
			
			sceneContainer.AddParentContainer(parentContainer);
			
			sceneContainer.Fill(null);
			sceneContainer.ResolveBindings();

			var a = sceneContainer.Resolve<A>();
			Assert.NotNull(a);
		}
	}

	public class Child
	{
		[Inject]
		public void Inject(A a)
		{
			
		}
	}
}