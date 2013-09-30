using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{


// --LOOK EVENTS-- //

		public static IEnumerator<ScriptPauser> lookAtScissors()
		{
			EventManager.Instance.ShowMessage("Good God, what the hell is that...?");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookAtTrashcan()
		{
			EventManager.Instance.ShowMessage("What a mess...");
			
			yield return null;
		}

// --PICK UP EVENTS-- //
		public static IEnumerator<ScriptPauser> pickUpScissors()
		{
			pickup("Scissors");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickUpLetter()
		{
			pickup("Letter");
			yield return null;
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useBedroomDoor_HallDoor()
		{
			door("HallDoor", "Upstairs", "BedroomDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

		public static IEnumerator<ScriptPauser> useBedroomDoor_BathroomDoor()
		{
			door("BathroomDoor", "Bathroom", "Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

	}
}

