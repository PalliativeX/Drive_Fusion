using NUnit.Framework;

namespace SimpleInject.Tests.Editor
{
	public sealed class TestConstructorInjection
	{
		[Test]
		public void TestContainerResolveNormalOrder()
		{
			var container = new DiContainer();

			container.BindSelf<A>().FromNew().AsSingle();
			container.BindSelf<C>().FromNew().AsSingle();
			container.BindSelf<ConstructorInjected>().FromNew().AsSingle();

			container.ResolveBindings(null);

			var constructorInjected = container.Resolve<ConstructorInjected>();
			Assert.That(constructorInjected.A != null);
			Assert.That(constructorInjected.C != null);
		}

		[Test]
		public void TestContainerResolveReversedOrder()
		{
			var container = new DiContainer();

			container.BindSelf<ConstructorInjected>().FromNew().AsSingle();
			container.BindSelf<A>().FromNew().AsSingle();
			container.BindSelf<C>().FromNew().AsSingle();

			container.ResolveBindings(null);

			var a = container.Resolve<A>();
			Assert.That(a != null);
		}
	}

	internal class ConstructorInjected
	{
		public A A;
		public C C;

		public ConstructorInjected(A a, C c)
		{
			A = a;
			C = c;
		}
	}
}