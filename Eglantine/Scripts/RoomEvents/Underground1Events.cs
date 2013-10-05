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
			yield return waitUntil("Player stopped");

			EventManager.Instance.SetFacing(Facing.Down);
			EventManager.Instance.IdleAnimation();

			EventManager.Instance.PlaySoundLooping("lowwind", .25f, 0, 0);
			Scheduler.Execute(undergroundSoundUpdate);
		}

		public static IEnumerator<ScriptPauser> useUnderground1Door_Down ()
		{
			door ("DownRope", "Underground2", "RopeUp");
			yield return waitUntil ("Player stopped");

			EventManager.Instance.SetFacing (Facing.Down);
			EventManager.Instance.IdleAnimation ();

			Scheduler.Execute (undergroundSoundUpdate);
		}
	}
}

