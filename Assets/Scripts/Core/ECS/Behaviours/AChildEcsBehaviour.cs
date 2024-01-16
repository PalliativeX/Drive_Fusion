using Scellecs.Morpeh;
using UnityEngine;

namespace Core.ECS
{
	public abstract class AChildEcsBehaviour : MonoBehaviour
	{
		public abstract void Link(Entity entity);
		public abstract void Unlink(Entity entity);
	}
}