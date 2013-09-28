using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static class ItemEvents
	{

		public static IEnumerator<ScriptPauser> acquirePhotograph ()
		{
			GameState.Instance.PhotoTaken = true;

			yield return null;
		}

		public static IEnumerator<ScriptPauser> usePuzzlebox()
		{
			EventManager.Instance.OpenPuzzlebox();
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useStrangeNotes()
		{
			EventManager.Instance.ViewDocument("StrangeNotes");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useJournal()
		{
			EventManager.Instance.ViewDocument("Journal");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useBlueprints()
		{
			EventManager.Instance.ViewDocument("Blueprints");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useLetter()
		{
			EventManager.Instance.ViewDocument("Letter");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> usePhotograph()
		{
			EventManager.Instance.ViewDocument("Photograph");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useFoldedNote()
		{
			EventManager.Instance.ViewDocument("FoldedNote");
			yield return null;
		}

		public static IEnumerable<ScriptPauser> usePuzzleKey ()
		{
			EventManager.Instance.ShowMessage ("There's a notch cut out on the head of this thing.  It looks like it's supposed to screw into a hole somewhere.");
			yield return ScriptPauser.WaitForSignal ("Message closed");

			if (EventManager.Instance.PlayerHasItem ("Puzzlebox"))
			{
				EventManager.Instance.ShowMessage("Hey, it looks like this could fit into the center of the puzzlebox...");
				yield return ScriptPauser.WaitForSignal("Message closed");
				EventManager.Instance.InsertPuzzleboxKey();
				EventManager.Instance.DestroyItem("Puzzle Key");
			}
		}

	}
}

