using Core.ECS;
using DG.Tweening;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class MoveLoopedChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Transform _moveTransform;
		[SerializeField] private float _endY;
		[SerializeField] private float _fromY;
		[SerializeField] private float _duration;
		[SerializeField] private Ease _ease;
		
		public override void Link(Entity entity)
		{
			_moveTransform.DOLocalMoveY(_endY, _duration)
				.From(_fromY)
				.SetEase(_ease)
				.SetLoops(-1, LoopType.Yoyo);
		}

		public override void Unlink(Entity entity) => 
			transform.DOKill();
	}
}