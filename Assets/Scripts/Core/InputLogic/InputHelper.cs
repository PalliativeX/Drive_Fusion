using Scellecs.Morpeh;
using UnityEngine;
using Utils;

namespace Core.InputLogic
{
	public class InputHelper
	{
		private readonly InputConfig _inputConfig;
		
		public InputHelper(InputConfig inputConfig)
		{
			_inputConfig = inputConfig;
		}

		public void Set(Entity entity, float xInput, float zInput)
		{
			ref var input = ref entity.GetComponent<MovementInput>();
			if (!input.Value.x.IsZero())
				return;
			
			input.Value = Vector3.zero;
			
			if (entity.Has<MovementInputBlocked>())
				return;

			input.Value.x = xInput;
				
#if DEBUG
			input.Value.z = _inputConfig.SetZManually 
				? zInput 
				: 1f;
#endif
		}
	}
}