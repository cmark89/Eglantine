using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

		public static IEnumerator<ScriptPauser> lookAtPainting ()
		{
			if (GameState.Instance.PaintingOpened)
			{
				EventManager.Instance.ShowMessage("What a waste of a painting...");
			}
			else
			{
				EventManager.Instance.ShowMessage("What a magnificent creature!");
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> interactWithPainting ()
		{
			Console.WriteLine ("Painting time");
			if (EventManager.Instance.UsingItem ("Scissors"))
			{
				EventManager.Instance.MovePlayerTo ("Painting");
				yield return waitUntil ("Player stopped");
					             
				EventManager.Instance.PlaySound ("Extend");
				GameState.Instance.PaintingOpened = true;
				EventManager.Instance.EnableInteractable ("Tear");
				EventManager.Instance.EnableInteractable ("PaintingDoor");
				EventManager.Instance.DisableInteractable ("Painting");
			}
			else
			{
				Console.WriteLine("no hasami");
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> interactWithTV ()
		{
			if (!GameState.Instance.TVOn)
			{
				EventManager.Instance.MovePlayerTo("TV");
				yield return waitUntil("Player stopped");
				
				yield return waitSeconds(1);
				EventManager.Instance.ShowMessage("No good.  It won't turn on.");
			}
			else
			{
				EventManager.Instance.MovePlayerTo("TV");
				yield return waitUntil("Player stopped");

				yield return waitSeconds(1);
				EventManager.Instance.ShowMessage("It won't turn off.  The dials don't do anything.");
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> lookAtTV ()
		{
			if (!GameState.Instance.TVOn)
			{
				EventManager.Instance.ShowMessage("That TV looks pretty old.");
			}
			else
			{
				EventManager.Instance.OpenTV();
			}

			yield return null;
		}


		public static IEnumerator<ScriptPauser> turnOnTV ()
		{
			if(EventManager.Instance.PlayerHasItem("Photograph"))
			{
				GameState.Instance.TVOn = true;
				checkTV();
				EventManager.Instance.DisableTrigger("TVActivate");
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> enterLivingRoom()
		{
			checkTV();

			yield return null;
		}

		public static IEnumerator<ScriptPauser> loadLivingRoom()
		{
			Scheduler.Execute(GameEvents.startIndoorSounds);
			checkTV();
			
			yield return null;
		}

		public static void checkTV ()
		{
			if (GameState.Instance.TVOn)
			{
				setTVImage();
				Scheduler.Execute(staticLoop);
				EventManager.Instance.PlaySoundLooping("static", .5f, 0, 0);
			}
		}

		// Put all logic for changing the image on the screen here
		public static void setTVImage ()
		{
			if (GameState.Instance.KitchenWindowBroken && !GameState.Instance.KitchenFlowerPicked)
			{
				EventManager.Instance.SetTVImage ("Graphics/TV/tv_kitchen");
			}
			else
			{
				EventManager.Instance.SetTVImage ("Graphics/TV/tv_eglantine");
			}
		}


		public static IEnumerator<ScriptPauser> staticLoop ()
		{
			int staticIndex = 1;
			while(EventManager.Instance.PlayerInRoom("LivingRoom"))
			{
				if(EventManager.Instance.PlayerInRoom("LivingRoom")) 
				{
					EventManager.Instance.EnableInteractable("Static" + staticIndex);
				}

				yield return waitSeconds(.05f);

				if(EventManager.Instance.PlayerInRoom("LivingRoom"))
				{
					EventManager.Instance.DisableInteractable("Static" + staticIndex);
				}

				staticIndex++;
				if(staticIndex > 6) staticIndex = 1;
			}
		}


		public static IEnumerator<ScriptPauser> leaveLivingRoom ()
		{
			EventManager.Instance.StopSoundEffect("static");
			yield return null;
		}


// --DOORS-- //
		public static IEnumerator<ScriptPauser> useLivingRoomDoor_Painting()
		{
			door("PaintingDoor", "SecretRoom", "Door");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useLivingRoomDoor_Foyer()
		{
			door("Door", "Foyer", "LivingRoomDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

	}
}

