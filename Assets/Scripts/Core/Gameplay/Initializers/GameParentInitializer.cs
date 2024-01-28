using System;
using Core.Gameplay.Behaviours;
using SimpleInject;
using UnityEngine;

namespace Core.Gameplay
{
	public class GameParentInitializer : IInitializable, IDisposable
	{
		private readonly GameParentProvider _parentProvider;
		private readonly GameParentBehaviour _parent;

		public GameParentInitializer(GameParentProvider parentProvider, GameParentBehaviour parent)
		{
			_parentProvider = parentProvider;
			_parent = parent;
		}

		public void Initialize()
		{
			_parentProvider.SetParent(_parent.Transform);
		}

		public void Dispose()    => _parentProvider.RemoveParent();
	}
}