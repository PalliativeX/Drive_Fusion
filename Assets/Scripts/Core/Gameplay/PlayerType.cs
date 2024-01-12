using System;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	[Serializable]
	public struct PlayerType : IComponent
	{
		public EPlayerType Value;
	}
	
	public enum EPlayerType
	{
		Human,
		AI
	}
}