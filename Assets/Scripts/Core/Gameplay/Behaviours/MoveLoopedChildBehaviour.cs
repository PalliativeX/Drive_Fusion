using Core.ECS;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scellecs.Morpeh;
using UnityEngine;
using Utils;

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
			if (!entity.Has<Offset>())
				Play();
			else
				PlayMove(entity.GetComponent<Offset>().Value);
		}

		public override void Unlink(Entity entity) => 
			transform.DOKill();

		private async UniTaskVoid PlayMove(float offset)
		{
			await UniTask.Delay((1000 * offset).ToInt());
			Play();
		}

		private void Play()
		{
			_moveTransform.DOLocalMoveY(_endY, _duration)
				.From(_fromY)
				.SetEase(_ease)
				.SetLoops(-1, LoopType.Yoyo);
		}
	}
}