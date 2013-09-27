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

			yield return null;
		}


// --LOOK EVENTS-- //

		public static IEnumerator<ScriptPauser> lookAtCrowbar()
		{
			EventManager.Instance.ShowMessage("That looks like it may be useful.");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookAtBackYardFlower()
		{
			EventManager.Instance.ShowMessage("Sweet briar...never really cared for it.");
			
			yield return null;
		}

// --PICK UP EVENTS-- //
		public static IEnumerator<ScriptPauser> pickUpCrowbar()
		{
			pickup("Crowbar");
			yield return null;
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useBackYardDoor()
		{
			door("Door", "Kitchen", "BackYardDoor");
			yield return null;
		}

	}
}

