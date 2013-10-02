using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{


// --LOOK EVENTS-- //

		public static IEnumerator<ScriptPauser> lookAtPuzzleKey()
		{
			EventManager.Instance.ShowMessage("Hey, what's that thing?");

			yield return null;
		}

// --PICK UP EVENTS-- //
		public static IEnumerator<ScriptPauser> pickUpPuzzleKey()
		{
			pickup("PuzzleKey");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useEmptyRoomDoor()
		{
			door("Door", "Upstairs", "EmptyRoomDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

	}
}

