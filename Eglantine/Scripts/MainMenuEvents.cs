using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		static int menuPhase = 0;

		public static IEnumerable<ScriptPauser> showSplashScreen()
		{
			EventManager.Instance.PlaySong("Toxic Night", .7f);
			yield return waitSeconds(2);
			EventManager.Instance.MainMenuFadeIn("splash", 3);
			yield return waitSeconds(7);
			EventManager.Instance.MainMenuFadeOut("splash", 3);
			yield return waitSeconds(3);
			EventManager.Instance.NextMenuPhase();
		}

		public static IEnumerable<ScriptPauser> titleFadeIn()
		{
			EventManager.Instance.MainMenuHideElement("splash");
			EventManager.Instance.MainMenuFadeIn("background", 5);
			yield return waitSeconds(6);
			EventManager.Instance.MainMenuFadeIn("title", 3);
			yield return waitSeconds(3);
			EventManager.Instance.NextMenuPhase();
		}

		public static IEnumerable<ScriptPauser> mainMenu()
		{
			EventManager.Instance.MainMenuFadeIn("background", 0);
			EventManager.Instance.MainMenuFadeIn("title", 0);
			EventManager.Instance.ShowMainMenu();
		}

		public static IEnumerable<ScriptPauser> onStartNewGame()
		{
			EventManager.Instance.PlaySound("windowbreak");
			EventManager.Instance.StopMusic();
			EventManager.Instance.LockMenuInput();
			yield return waitSeconds(2.5f);

			EventManager.Instance.MainMenuFadeOut("background", 3);
			EventManager.Instance.MainMenuFadeOut("title", 3);
			yield return waitSeconds(3.5f);

			EventManager.Instance.PlayStorySequence("openingCutscene");
		}

		Script[] menuEvents = new Script[] {
			null,
			showSplashScreen,
			titleFadeIn,
			mainMenu
		};
	}
}

