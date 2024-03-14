using Core.Integrations.SaveSystem;
using UnityEngine;

namespace Core.Tutorial
{
	public class TutorialService
	{
		private readonly TutorialConfig _tutorialConfig;
		private readonly SaveService _save;
		
		public bool IsTutorialComplete { get; private set; }

		public TutorialService(TutorialConfig tutorialConfig, SaveService save)
		{
			_tutorialConfig = tutorialConfig;
			_save = save;
		}

		public bool HasTutorial()
		{
#if UNITY_EDITOR
			if (!_tutorialConfig.IsTutorialActive)
				return false;
#endif

			Debug.Log("IsFirstSession: " + _save.IsFirstSession + " , IsTutorialComplete: " + IsTutorialComplete);
			return _save.IsFirstSession && !IsTutorialComplete;
		}

		public void SetTutorialComplete() => 
			IsTutorialComplete = true;
	}
}