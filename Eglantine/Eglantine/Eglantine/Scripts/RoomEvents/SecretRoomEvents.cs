using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		public static IEnumerator<ScriptPauser> interactWithTrapdoor ()
		{
			if (EventManager.Instance.UsingItem ("Key") || GameState.Instance.TrapdoorUnlocked)
			{
				Scheduler.Execute(openTrapdoor);
			}
			else
			{
				EventManager.Instance.MovePlayerTo("TrapdoorClosed");
				yield return waitUntil("Player stopped");

				Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
				yield return waitUntil("Interact frame 4");
							
				EventManager.Instance.ShowMessage("It's locked up nice and tight.");
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> openTrapdoor ()
		{
			EventManager.Instance.MovePlayerTo ("TrapdoorClosed");
			yield return waitUntil ("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");
					
			if (!GameState.Instance.TrapdoorUnlocked)
			{
				EventManager.Instance.DestroyItem ("Key");
				GameState.Instance.TrapdoorUnlocked = true;
			}
							
			EventManager.Instance.PlaySound("dooropen", 1.5f, 0f, 0f);
			EventManager.Instance.DisableInteractable("TrapdoorClosed");
			EventManager.Instance.EnableInteractable("TrapdoorOpen");
			EventManager.Instance.EnableInteractable("TrapdoorOpenGraphic");
			EventManager.Instance.EnableInteractable("TrapdoorHatch_Open");

			GameState.Instance.TrapdoorOpen = true;
			Scheduler.Execute(undergroundSoundUpdate);

			yield return null;
		}

		public static IEnumerator<ScriptPauser> closeTrapdoor ()
		{
			EventManager.Instance.MovePlayerTo("TrapdoorClosed");
			yield return waitUntil("Player stopped");

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, Facing.Left);
			yield return waitUntil("Interact frame 4");
						
			EventManager.Instance.PlaySound("safeclose");
			EventManager.Instance.EnableInteractable("TrapdoorClosed");
			EventManager.Instance.DisableInteractable("TrapdoorOpen");
			EventManager.Instance.DisableInteractable("TrapdoorOpenGraphic");
			EventManager.Instance.DisableInteractable("TrapdoorHatch_Open");

			GameState.Instance.TrapdoorOpen = false;
			Scheduler.Execute(undergroundSoundUpdate);

			yield return null;
		}

		public static IEnumerator<ScriptPauser> checkTVStatic ()
		{
			Scheduler.Execute(startUndergroundSounds);
			if (GameState.Instance.TVOn)
			{
				EventManager.Instance.PlaySoundLooping ("static", .15f, 0, 0);
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> leaveSecretRoom ()
		{
			EventManager.Instance.StopSoundEffect("static");

			yield return null;
		}


// --LOOK EVENTS-- //

		public static IEnumerator<ScriptPauser> lookAtPuzzlebox()
		{
			EventManager.Instance.ShowMessage("A puzzlebox sits on the table.");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookAtTrapdoor ()
		{
			EventManager.Instance.ShowMessage("I wonder where that goes...");

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookDownTrapdoor()
		{
			EventManager.Instance.ShowMessage("Looks pretty dark down there...");
			
			yield return null;
		}

		public static IEnumerator<ScriptPauser> loadSecretRoom ()
		{
			GameState.Instance.DrumsPlaying = false;
			Console.WriteLine("SECRET ROOM LOADED!");
			Scheduler.Execute(startIndoorSounds);
			Scheduler.Execute(checkTVStatic);

			yield return null;
		}

// --PICK UP EVENTS-- //
		public static IEnumerator<ScriptPauser> pickUpPuzzlebox()
		{
			pickup("Puzzlebox");
			yield return null;
		}

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useSecretRoomDoor_Painting()
		{
			door("Door", "LivingRoom", "Painting");
			yield return waitUntil("Player stopped");
		
			EventManager.Instance.SetFacing(Facing.Down);
			EventManager.Instance.IdleAnimation();

			Scheduler.Execute(killUndergroundSounds);
		}

		public static IEnumerator<ScriptPauser> useSecretRoomDoor_TrapDoor()
		{
			door("TrapdoorOpen", "Underground1", "UpRope");
			yield return waitUntil("Player stopped");

			EventManager.Instance.SetFacing(Facing.Down);
			EventManager.Instance.IdleAnimation();
			AudioManager.Instance.StopSoundEffect("lowwind");
			Scheduler.Execute (undergroundSoundUpdate);
		}
	}
}

