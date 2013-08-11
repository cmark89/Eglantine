using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	[Serializable]
	public class SafeState
	{
		// Change this.
		public bool PuzzleSolved { get; private set; }
		private const string COMBINATION = "39549";
		public string CurrentGuess = "";

		public SafeState ()
		{
		}

		public void AddNumber (int num)
		{
			if (CurrentGuess.Length == 5)
				CheckIfSolved ();
			else
			{
				CurrentGuess = String.Concat(CurrentGuess, num.ToString());
			}
		}

		public void CheckIfSolved ()
		{
			if (CurrentGuess == COMBINATION)
			{
				// Play the happy beeping
				EventManager.Instance.PlaySound ("safeunlock");
				TriggerPuzzleSolve ();
			}
			else
			{
				// Play the error beeping
				EventManager.Instance.PlaySound ("errorbeep");
				PuzzleSolved = false;
				ClearInput();
			}
		}

		public void ClearInput()
		{
			CurrentGuess = "";
		}

		private void TriggerPuzzleSolve()
		{
			PuzzleSolved = true;
			// Do the stuff to make the thing happen
		}
	}
}

