using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace SimpleInject.Tests.Editor
{
	public sealed class TestContainerResolving
	{
		[Test]
		public void TestContainerResolve()
		{
			var container = new DiContainer();
			
			container.BindSelf<A>().FromNew().AsSingle();
			
			container.ResolveBindings(null);

			var a = container.Resolve<A>();
			Assert.That(a != null);
		}
		
		[Test]
		public void TestContainerInterfaceResolve()
		{
			var container = new DiContainer();
			
			container.BindInterfacesAndSelf<C>().FromNew().AsSingle();

			container.ResolveBindings(null);

			var destroyers = container.Resolve<List<IDestroyer>>();
			var initializers = container.Resolve<List<IInitializer>>();

			Assert.That(destroyers.Count != 0);
			Assert.That(initializers.Count != 0);
		}
		
		[Test]
		public void Test()
		{
			var container = new DiContainer();
			
			container.BindInterfacesAndSelf<A>().FromNew().AsSingle();
			container.BindSelf<B>().FromNew().AsSingle();
			container.BindInterfacesAndSelf<C>().FromNew().AsSingle();

			container.ResolveBindings(null);

			var a = container.Resolve<A>();
			var b = container.Resolve<B>();
			var destroyers = container.Resolve<List<IDestroyer>>();
			var disposable = container.Resolve<List<IDisposable>>();

			Assert.NotNull(b.A);
			Assert.That(destroyers.Count != 0);
			Assert.That(disposable.Count != 0);
		}
	}
	
	internal class A : IDisposable
	{
		public override string ToString() => "A is A";
		
		public void Dispose() { }
	}

	internal class B
	{
		public A A;

		[Inject]
		public void Inject(A a, List<IInitializer> initializers)
		{
			A = a;
			foreach (var initializer in initializers) {
				Debug.Log(initializer.GetType());
			}
		}
	}

	internal class C : IInitializer, IDestroyer
	{
		
	}

	internal interface IInitializer { }
	
	public interface IDestroyer { }
}