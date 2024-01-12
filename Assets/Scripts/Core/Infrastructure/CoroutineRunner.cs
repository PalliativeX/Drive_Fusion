using System.Collections;
using UnityEngine;

namespace Core.Infrastructure
{
	public sealed class CoroutineRunner : MonoBehaviour
	{
		public new Coroutine StartCoroutine(IEnumerator coroutine) => 
			base.StartCoroutine(coroutine);

		public new void StopCoroutine(Coroutine coroutine) => 
			base.StopCoroutine(coroutine);
	}
}