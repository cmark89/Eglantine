using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> enterFrontYard()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("naturalwind", .3f, 0f, 0f);
			if(EventManager.Instance.PlayerHasItem("Photograph") && !GameState.Instance.FrontYardFlowerPicked)
				EventManager.Instance.EnableInteractable("Eglantine");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickFrontYardEglantine ()
		{
			if (EventManager.Instance.UsingItem ("Scissors"))
			{
				pickup ("Eglantine");

				yield return waitUntil("Player stopped");
				GameState.Instance.FrontYardFlowerPicked = true;
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> leaveFrontYard()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("lowwind", .25f, 0, 0);

			yield return null;
		}


// --LOOK EVENTS-- //

		public static IEnumerator<ScriptPauser> lookAtFrontDoor()
		{
			EventManager.Instance.ShowMessage("What a creepy looking place...");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookAtFrontYardFlower()
		{
			EventManager.Instance.ShowMessage("Was that flower there before...?");
			
			yield return null;
		}

// --PICK UP EVENTS-- //

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useFrontYardDoor()
		{
			door("Door", "Foyer", "FrontDoor");
			yield return null;
		}

	}
}

