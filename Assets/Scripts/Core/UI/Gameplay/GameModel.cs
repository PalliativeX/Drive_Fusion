using Core.ECS;
using Core.Gameplay;
using Core.InputLogic;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.UI
{
	public sealed class GameModel : IInitializable, ITickable
	{
		private readonly InputHelper _inputHelper;
		private readonly Filter _playerFilter;

		private float _currentXInput = 0f;
		
		public GameModel(World world, InputHelper inputHelper)
		{
			_inputHelper = inputHelper;
			_playerFilter = world.Filter
				.With<View>()
				.With<HumanPlayer>()
				.Build();
		}

		public void Initialize() { }

		public void SetXTouchInput(float xInput) => 
			_currentXInput = xInput;

		public void Tick()
		{
			foreach (var entity in _playerFilter)
			{
				_inputHelper.Set(entity, _currentXInput, 1f);
			}
		}
	}
}