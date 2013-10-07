#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Eglantine
{
	static class Program
	{
		private static Eglantine game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
            try
            {
                game = new Eglantine();
                game.Run();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
		}
	}
}
