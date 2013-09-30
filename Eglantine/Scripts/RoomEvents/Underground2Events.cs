using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useUnderground2Door_Up()
		{
			door("RopeUp", "Underground1", "DownRope");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useUnderground2Door_Down()
		{
			door("RopeDown", "Underground3", "RopeUp");
			yield return null;
		}

	}
}

