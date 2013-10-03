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
			yield return waitUntil("Player stopped");
			EventManager.Instance.SetFacing(Facing.Up);
		}
		
		public static IEnumerator<ScriptPauser> useUnderground3Door_Down()
		{
			door("Door", "Underground4", "Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("dooropen", .9f, 0f, 0f);
			EventManager.Instance.SetFacing(Facing.Right);
		}
		
	}
}
