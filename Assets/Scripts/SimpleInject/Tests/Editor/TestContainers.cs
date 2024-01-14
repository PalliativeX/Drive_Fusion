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

			parentContainer.ResolveBindings(null);
			
			sceneContainer.AddParentContainer(parentContainer);
			
			sceneContainer.ResolveBindings(null);

			var a = sceneContainer.Resolve<A>();
			Assert.NotNull(a);
		}
	}

	internal class Child
	{
		[Inject]
		public void Inject(A a)
		{
			
		}
	}
}