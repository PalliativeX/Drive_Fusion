using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Levels
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct CurrentLevel : IComponent
	{
		public int Value;

		public int Index => Value - 1;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct LoadedLevel : IComponent
	{
		public int Value;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct LevelsPassed : IComponent
	{
		public int Value;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct RequestLevelLoad : IComponent { }
}