using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> enterBackYard()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("naturalwind", .3f, 0f, 0f);

			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickBackYardEglantine()
		{
			if(EventManager.Instance.UsingItem("Scissors"))
				pickup("Eglantine");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> leaveBackYard()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("lowwind", .25f, 0, 0);
		}
	}
}

