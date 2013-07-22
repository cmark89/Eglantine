using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
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
				Console.WriteLine("Current guess: " + CurrentGuess);
			}
		}

		public void CheckIfSolved ()
		{
			if (CurrentGuess == COMBINATION)
			{
				// Play the happy beeping
				TriggerPuzzleSolve ();
			}
			else
			{
				// Play the error beeping
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

