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
			pickup("Puzzle Key");
			yield return null;
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useEmptyRoomDoor()
		{
			door("Door", "Upstairs", "EmptyRoomDoor");
			yield return null;
		}

	}
}

