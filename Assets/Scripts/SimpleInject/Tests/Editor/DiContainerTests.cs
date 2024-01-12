using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace SimpleInject.Tests.Editor
{
	public sealed class DiContainerTests
	{
		[Test]
		public void Test()
		{
			var container = new DiContainer();
			
			container.BindSelf<A>().FromNew().AsSingle();
			container.BindSelf<B>().FromNew().AsSingle();
			container.BindInterfacesAndSelf<C>().FromNew().AsSingle();

			var aNew = new A();
			container.BindInterfaces<A>().FromInstance(aNew);

			container.Fill(null);
			
			container.ResolveBindings();

			var a = container.Resolve<A>();
			var b = container.Resolve<B>();
			var destroyers = container.Resolve<List<IDestroyer>>();
			var disposable = container.Resolve<List<IDisposable>>();

			Assert.NotNull(b.A);
			Assert.That(destroyers.Count != 0);
			Assert.That(disposable.Count != 0);
		}
	}
	
	public class A : IDisposable
	{
		public override string ToString() => "A is A";
		
		public void Dispose() { }
	}

	public class B
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

	public class C : IInitializer, IDestroyer
	{
		
	}

	public interface IInitializer { }
	
	public interface IDestroyer { }
}