using Core.CameraLogic;
using Core.ECS;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Core.Levels;
using Core.Sound;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.UI.Menu
{
	public class MenuModel
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly World _world;

		private readonly Filter _currentLevelFilter;
		private readonly Filter _playerFilter;
		private readonly Filter _cameraFilter;
		
		public MenuModel(GlobalWorld globalWorld, GameStateMachine stateMachine, World world)
		{
			_stateMachine = stateMachine;
			_world = world;

			var global = globalWorld.World;
			_currentLevelFilter = global.Filter.With<CurrentLevel>().Build();
			_playerFilter = world.Filter.With<HumanPlayer>().With<CarController>().Build();
		}
		public int GetCurrentLevel()
		{
			var entity = _currentLevelFilter.First();
			return entity.GetComponent<CurrentLevel>().Value;
		}

		public void StartPlaying()
		{
			_stateMachine.ChangeState(GameStateType.Gameplay);
		}

		public async UniTask Initialize(float animationSpeed)
		{
			Entity player;
			do
			{
				await UniTask.Yield();
				player = _playerFilter.FirstOrDefault();
			}
			while (player == null);

			player.GetComponent<CarController>().Reference.SetMotor(animationSpeed);
			
			_world.ChangeActiveCamera("Menu");
		}

		public void SetMainCamera() => _world.ChangeActiveCamera("Main");
	}
}