using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> openTopDrawer ()
		{
			bool usingCrowbar = false;
			if (EventManager.Instance.UsingItem ("Crowbar"))
			{
				usingCrowbar = true;
			}

			EventManager.Instance.MovePlayerTo ("TopDrawer_Closed");
			yield return waitUntil ("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");
						
			if (usingCrowbar && !GameState.Instance.TopDrawerFixed)
			{
				//Animation as player pries it open...
				GameState.Instance.TopDrawerFixed = true;
				EventManager.Instance.PlaySound ("safeopen");
				setTopDrawerOpen ();
			}
			else if (GameState.Instance.TopDrawerFixed)
			{
				setTopDrawerOpen ();
			}
			else
			{
				EventManager.Instance.ShowMessage("It's stuck shut.  I don't have the strength to open it.");
			}

			yield return null;
		}



		public static IEnumerator<ScriptPauser> openBottomDrawer ()
		{
			//Check if the player is trying to use the crowbar
			bool usingCrowbar = false;
			if (EventManager.Instance.UsingItem ("Crowbar")) 
				usingCrowbar = true;
							
			//Move the player to the filing cabinet
			EventManager.Instance.MovePlayerTo ("BottomDrawer_Closed");
			yield return waitUntil ("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");
							
			//Now, check to see what must happen.
			if (usingCrowbar && !GameState.Instance.BottomDrawerFixed)
			{
				//Animation as player pries it open...
				GameState.Instance.BottomDrawerFixed = true;
				EventManager.Instance.PlaySound ("safeopen");
				setBottomDrawerOpen ();
			}
			else if (GameState.Instance.BottomDrawerFixed)
			{
				setBottomDrawerOpen ();
			}
			else
			{
				EventManager.Instance.ShowMessage("It's completely jammed.  I wonder how long it's been since it's been opened...");
				yield return null;
			}
		}

		public static IEnumerator<ScriptPauser> closeTopDrawer()
		{
			EventManager.Instance.MovePlayerTo("TopDrawer_Open");
			yield return waitUntil("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");

			setTopDrawerClosed();
			yield return null;
		}

		public static IEnumerator<ScriptPauser> closeBottomDrawer()
		{
			EventManager.Instance.MovePlayerTo("BottomDrawer_Open");
			yield return waitUntil("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");

			setBottomDrawerClosed();
			yield return null;
		}

		public static void setTopDrawerOpen()
		{
			EventManager.Instance.PlaySound("filingcabinetopen");
				
			EventManager.Instance.DisableInteractable("TopDrawer_Closed");
			EventManager.Instance.EnableInteractable("TopDrawer_Open");
					
			//Enable all items that haven't been taken yet...
			if (!GameState.Instance.PlayerHasItem("Blueprints"))
				EventManager.Instance.EnableInteractable("Blueprints");

			if (!GameState.Instance.PlayerHasItem("Strange Notes"))
				EventManager.Instance.EnableInteractable("Strange Notes");


			checkCabinetBlocker();
		}

		public static void setTopDrawerClosed()
		{
			EventManager.Instance.PlaySound("filingcabinetclose");

			EventManager.Instance.DisableInteractable("TopDrawer_Open");
			EventManager.Instance.EnableInteractable("TopDrawer_Closed");

			//Disable all items that haven't been taken yet...
			EventManager.Instance.DisableInteractable("Strange Notes");
			EventManager.Instance.DisableInteractable("Blueprints");
					
			checkCabinetBlocker();
		}

		public static void setBottomDrawerOpen()
		{
			EventManager.Instance.PlaySound("filingcabinetopen");

			EventManager.Instance.DisableInteractable("BottomDrawer_Closed");
			EventManager.Instance.EnableInteractable("BottomDrawer_Open");

			//Enable all items that haven't been taken yet...
			if(!GameState.Instance.PlayerHasItem("Journal"))
				EventManager.Instance.EnableInteractable("Journal");

			if (!GameState.Instance.PhotoTaken)
				EventManager.Instance.EnableInteractable("Photograph");

			checkCabinetBlocker();
		}
		
		public static void setBottomDrawerClosed()
		{
			EventManager.Instance.PlaySound("filingcabinetclose");
				
			EventManager.Instance.DisableInteractable("BottomDrawer_Open");
			EventManager.Instance.EnableInteractable("BottomDrawer_Closed");
					
			//Enable all items that haven't been taken yet...
			EventManager.Instance.DisableInteractable("Photograph");
			EventManager.Instance.DisableInteractable("Journal");

			checkCabinetBlocker();
		}

		public static void checkCabinetBlocker ()
		{
			if (EventManager.Instance.InteractableIsActive ("BottomDrawer_Open") || EventManager.Instance.InteractableIsActive ("TopDrawer_Open"))
			{
				if(!EventManager.Instance.InteractableIsActive("CabinetMovementBlocker"))
					EventManager.Instance.EnableInteractable("CabinetMovementBlocker");
				else
					EventManager.Instance.DisableInteractable("CabinetMovementBlocker");
			}
		}


// //PICK UP EVENTS// //
		public static IEnumerator<ScriptPauser> pickUpBlueprints()
		{
			pickup("Blueprints");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickUpStrangeNotes()
		{
			pickup("Strange Notes");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickUpPhotograph()
		{
			pickup("Photograph");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> pickUpJournal()
		{
			pickup("Journal");
			yield return null;
		}



// //DOORS// //
		public static IEnumerator<ScriptPauser> useOfficeDoor()
		{
			door("Door", "Upstairs", "OfficeDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);

			EventManager.Instance.SetFacing(Facing.Down);
			EventManager.Instance.IdleAnimation();
		}

	}
}

