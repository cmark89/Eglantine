using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> openCabinet()
		{
			EventManager.Instance.MovePlayerTo("CabinetClosed");
			yield return waitUntil("Player stopped");

			//Open the cabinet now
			EventManager.Instance.PlaySound("cabinetopen");
			EventManager.Instance.EnableInteractable("CabinetOpen");
			EventManager.Instance.EnableInteractable("DollHead");
			EventManager.Instance.DisableInteractable("CabinetClosed");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> closeCabinet()
		{
			EventManager.Instance.MovePlayerTo("CabinetOpen");
			yield return waitUntil("Player stopped");
			
			//Close the cabinet now
			EventManager.Instance.PlaySound("cabinetclose");
			EventManager.Instance.EnableInteractable("CabinetClosed");
			EventManager.Instance.DisableInteractable("CabinetOpen");
			EventManager.Instance.DisableInteractable("DollHead");
			
			yield return null;
		}

		public static IEnumerator<ScriptPauser> interactWithDollHead ()
		{
			EventManager.Instance.ShowMessage("Yeah, there's no way I'm touching that.  It's obviously cursed.");
			yield return null;
		}


// --LOOK EVENTS-- //

		
		
		public static IEnumerator<ScriptPauser> lookAtDollHead ()
		{
			EventManager.Instance.ShowMessage("What the hell is with this place...?");
			yield return null;
		}

// --PICK UP EVENTS-- //


// --DOORS-- //
		public static IEnumerator<ScriptPauser> useBathroomDoor()
		{
			door("Door", "Bedroom", "BathroomDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
			EventManager.Instance.SetFacing(Facing.Down);
		}

	}
}

