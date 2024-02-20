using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.ECS
{
	public class BaseEcsBehaviour : MonoBehaviour
	{
		[SerializeField] private AChildEcsBehaviour[] _children;
		
		protected Entity Entity;

		public AChildEcsBehaviour[] Children => _children;

		public void Link(Entity entity)
		{
			Entity = entity;

			entity.SetComponent(new InstanceId { Value = gameObject.GetInstanceID() });
			
			OnLink(entity);

			foreach (AChildEcsBehaviour child in _children) 
				child.Link(entity);
		}

		public void Unlink()
		{
			OnUnlink();
			
			foreach (AChildEcsBehaviour child in _children) 
				child.Unlink(Entity);
			
			Entity = null;
		}

		protected virtual void OnLink(Entity entity) { }

		protected virtual void OnUnlink() { }

		[Button, GUIColor(0.3f, 1f, 0.1f), PropertySpace(6f)]
		private void CollectChildren()
		{
			_children = GetComponentsInChildren<AChildEcsBehaviour>();
		}
	}
}