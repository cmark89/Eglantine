using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> checkWindow ()
		{
			if (GameState.Instance.KitchenWindowBroken)
			{
				EventManager.Instance.PlaySoundLooping ("naturalwind", .2f, 0, 0);
				EventManager.Instance.DisableInteractable ("WindowLayer");
				EventManager.Instance.EnableInteractable ("BrokenWindowLayer");

				if (!GameState.Instance.KitchenFlowerPicked)
				{
					EventManager.Instance.EnableInteractable ("Eglantine");
				}
			}
			else
			{
				EventManager.Instance.EnableInteractable("WindowLayer");
				EventManager.Instance.DisableInteractable("BrokenWindowLayer");
				EventManager.Instance.DisableInteractable("Eglantine");
			}

			yield return null;
		}


		public static IEnumerator<ScriptPauser> pickKitchenEglantine ()
		{
			if (EventManager.Instance.UsingItem ("Scissors"))
			{
				pickup ("Eglantine");
				GameState.Instance.KitchenFlowerPicked = true;
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> leaveKitchen()
		{
			if(GameState.Instance.KitchenWindowBroken)
				EventManager.Instance.StopSoundEffect("naturalwind");

			yield return null;
		}


// --LOOK EVENTS-- //


		public static IEnumerator<ScriptPauser> lookAtKitchenFlower()
		{
			EventManager.Instance.ShowMessage("Okay, this is getting pretty weird...");
			
			yield return null;
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useKitchenDoor_BackYard()
		{
			door("BackYardDoor", "BackYard", "Door");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useKitchenDoor_Foyer()
		{
			door("BackYardDoor", "BackYard", "Door");
			yield return null;
		}

	}
}

