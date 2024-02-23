using Core.Gameplay;
using Core.UI.Result;
using Core.UI.Revive;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.UI.Systems
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class ShowReviveUiOnStopSystem : ISystem
	{
		private readonly PanelController _panelController;
		private Filter _filter;
		
		public World World { get; set; }

		public ShowReviveUiOnStopSystem(PanelController panelController) => 
			_panelController = panelController;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<StopRequested>()
				.Without<Stopped>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				entity.RemoveComponent<StopRequested>();
				entity.SetComponent(new Stopped());
				
				if (!entity.Has<ReviveUnavailable>())
					_panelController.Open<RevivePresenter>();
				else
					_panelController.Open<ResultPresenter>();
			}
		}
		
		public void Dispose() { }
	}
}