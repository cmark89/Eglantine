using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useUpstairsDoor_Bedroom()
		{
			door("BedroomDoor", "Bedroom", "HallDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);

			EventManager.Instance.SetFacing(Facing.Left);
			EventManager.Instance.IdleAnimation();
		}

		public static IEnumerator<ScriptPauser> useUpstairsDoor_Office()
		{
			door("OfficeDoor", "Office", "Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);

			EventManager.Instance.SetFacing(Facing.Right);
			EventManager.Instance.IdleAnimation();
		}

		public static IEnumerator<ScriptPauser> useUpstairsDoor_EmptyRoom()
		{
			door("EmptyRoomDoor", "EmptyRoom", "Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);

			EventManager.Instance.SetFacing(Facing.Right);
			EventManager.Instance.IdleAnimation();
		}

		public static IEnumerator<ScriptPauser> useUpstairsDoor_Foyer()
		{
			door("Stairs", "Foyer", "Stairs");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("stairs", .9f, 0f, 0f);

			EventManager.Instance.SetFacing(Facing.Left);
			EventManager.Instance.IdleAnimation();
		}

		public static IEnumerator<ScriptPauser> lookAtSafe ()
		{
			EventManager.Instance.ShowMessage("That safe looks pretty sturdy...");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> interactWithSafe ()
		{
			EventManager.Instance.MovePlayerTo ("Safe");
			yield return waitUntil ("Player stopped");
					
			//Bring up the safe screen here
			EventManager.Instance.OpenSafe ();
			yield return waitUntil ("Safe closed");
			if (EventManager.Instance.PlayerHasItem ("Key") && !GameState.Instance.KitchenWindowBroken)
			{
				GameState.Instance.KitchenWindowBroken = true;
				EventManager.Instance.DisableSaving();
				yield return waitSeconds(3);
				breakGlassInKitchen();
				EventManager.Instance.EnableSaving();
			}
						
		}

		public static void breakGlassInKitchen ()
		{
			EventManager.Instance.PlaySound("windowbreak");
		}

	}
}

