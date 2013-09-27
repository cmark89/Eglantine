using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useUnderground1Door_Up()
		{
			door("UpRope", "SecretRoom", "Trapdoor");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useUnderground1Door_Down()
		{
			door("DownRope", "Underground2", "RopeUp");
			yield return null;
		}

	}
}

