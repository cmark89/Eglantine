using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		
		// --DOORS-- //
		public static IEnumerator<ScriptPauser> useUnderground3Door_Up()
		{
			door("RopeUp", "Underground2", "RopeDown");
			yield return null;
		}
		
		public static IEnumerator<ScriptPauser> useUnderground3Door_Down()
		{
			door("Door", "Underground4", "Door");
			yield return null;
		}
		
	}
}

