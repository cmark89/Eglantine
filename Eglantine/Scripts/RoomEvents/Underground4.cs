using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		public static IEnumerator<ScriptPauser> beginEnding()
		{
			EventManager.Instance.DisableSaving();
			EventManager.Instance.SetItemType("Eglantine", "Active");

			//Right now, only the default is available, so enable it
			GameState.Instance.Ending = "normal";
			EventManager.Instance.EnableInteractable("Grave");
			EventManager.Instance.DisableInteractable("Artifact");
			Scheduler.Execute(normalEndingScenario);

			yield return null;
		}

		public static IEnumerator<ScriptPauser> normalEndingScenario ()
		{
			//Loop the pulsing
			float strobeTime = 6;
			float strobeFade = .15f;

			while (strobeTime > 0 && GameState.Instance.FlowersOnGrave < 3)
			{
				yield return waitSeconds(strobeTime);
				Scheduler.ExecuteWithArgs<float, float>(artifactPulse, .4f, strobeFade);
				strobeTime = strobeTime -1;
				strobeFade = .15f + ((6 - strobeTime) * 1f/6f);
			}

			//If the player failed to complete the puzzle
			if(GameState.Instance.FlowersOnGrave < 3)
			{
				yield return waitSeconds(1);
				EventManager.Instance.FadeOutInteractable("Grave", 3);
				EventManager.Instance.EnableInteractable("Artifact");
				EventManager.Instance.FadeInInteractable("Artifact", 3);
				yield return waitSeconds(3);
				EventManager.Instance.DisableInteractable("Grave");
				yield return waitSeconds(4);
				EventManager.Instance.PlayStorySequence("badEnding");
			}
			else if(GameState.Instance.FlowersOnGrave == 3)
			{
				yield return waitSeconds(4);
				Scheduler.ExecuteWithArgs<float, float>(fadeOutGrave, 6, 1);
				yield return waitSeconds(6);
				EventManager.Instance.PlayStorySequence("goodEnding");
			}
		}
		public static IEnumerator<ScriptPauser> artifactPulse (float time, float amount)
		{
			EventManager.Instance.PlaySound("heartbeat");
			fadeOutGrave(time, amount);
			EventManager.Instance.EnableInteractable("Artifact");
			EventManager.Instance.FadeInteractable("Artifact", time, amount);
			yield return waitSeconds(.5f);
			EventManager.Instance.FadeOutInteractable("Artifact", time);
			fadeInGrave(time);
			yield return waitSeconds(.5f);
			EventManager.Instance.DisableInteractable("Artifact");
		}




		public static IEnumerator<ScriptPauser> interactWithGrave ()
		{
			if (EventManager.Instance.UsingItem ("Eglantine"))
			{
				EventManager.Instance.MovePlayerTo("Grave");
				yield return waitUntil("Player stopped");
				placeFlowerOnGrave();
			}
		}

		public static void placeFlowerOnGrave ()
		{
			EventManager.Instance.DestroyItem("Eglantine");
			GameState.Instance.FlowersOnGrave = GameState.Instance.FlowersOnGrave + 1;
			EventManager.Instance.EnableInteractable("Flower" + GameState.Instance.FlowersOnGrave);
		}

		public static IEnumerator<ScriptPauser> fadeOutGrave (float time, float amount)
		{
			EventManager.Instance.FadeInteractable("Grave", time, 1.0 - amount);
			EventManager.Instance.FadeInteractable("Flower1", time, 1.0 - amount);
			EventManager.Instance.FadeInteractable("Flower2", time, 1.0 - amount);
			EventManager.Instance.FadeInteractable("Flower3", time, 1.0 - amount);
		}

		public static IEnumerator<ScriptPauser> fadeInGrave (float time)
		{
			EventManager.Instance.FadeInInteractable("Grave", time);
			EventManager.Instance.FadeInInteractable("Flower1", time);
			EventManager.Instance.FadeInInteractable("Flower2", time);
			EventManager.Instance.FadeInInteractable("Flower3", time);
		}

		public static IEnumerator<ScriptPauser> readHeadstone ()
		{
			if(GameState.Instance.FlowersOnGrave == 0)
				EventManager.Instance.ShowMessage("A gravestone... the name has been worn away.");

					
			if(GameState.Instance.FlowersOnGrave == 1)
				EventManager.Instance.ShowMessage("The writing is very faded, but its partially legible.  It reads: ..R. .I.S FC.A-II.C W.AIV.R.");

			if(GameState.Instance.FlowersOnGrave == 2)
				EventManager.Instance.ShowMessage("The text is becoming clearer.  It reads: \nHER. LICS FCLAHIIVE WCATNERS");
									
			if(GameState.Instance.FlowersOnGrave == 3)
				EventManager.Instance.ShowMessage("The headstone reads: \nHERE LIES EGLANTINE WEATHERS");

			yield return null;
		}



// //DOORS// //
		public static IEnumerator<ScriptPauser> useUnderground4Door()
		{
			EventManager.Instance.ShowMessage("...");
			yield return null;
		}



	}
}

