using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay
{
	public class GamePauser
	{
		private readonly Filter _playerFilter;
		
		public GamePauser(World world)
		{
			_playerFilter = world.Filter.With<HumanPlayer>().Build();
		}

		public void SwitchPause(bool paused)
		{
			Time.timeScale = paused ? 0f : 1f;
			
			Entity player = _playerFilter.First();

			if (paused && player.Has<Active>())
				player.RemoveComponent<Active>();
			else if (!paused && !player.Has<Active>())
				player.SetComponent(new Active());
		}
	}
}