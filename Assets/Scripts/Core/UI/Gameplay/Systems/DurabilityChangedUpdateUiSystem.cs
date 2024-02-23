using Core.ECS;
using Core.Gameplay;
using Scellecs.Morpeh;

namespace Core.UI
{
	public class DurabilityChangedUpdateUiSystem : ASystem<Durability, DurabilityChanged>
	{
		private readonly GamePresenter _presenter;
		
		public DurabilityChangedUpdateUiSystem(GamePresenter presenter) => 
			_presenter = presenter;

		protected override void OnUpdate(float deltaTime, Entity entity) => 
			_presenter.UpdateDurability();
	}
}