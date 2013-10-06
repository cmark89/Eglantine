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
			EventManager.Instance.IdleAnimation();

			Scheduler.Execute (undergroundSoundUpdate);
		}

		public static IEnumerator<ScriptPauser> useUnderground3Door_Down()
		{
			EventManager.Instance.MovePlayerTo("Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("dooropen", .9f, 0f, 0f);

			Player.Instance.PlayInteractAnimation(Facing.Right);
			yield return waitUntil("Interact frame 4");
			Scheduler.Execute (killUndergroundSounds);

			EventManager.Instance.ChangeRoom("Underground4", "Door");

			EventManager.Instance.SetFacing(Facing.Right);
			EventManager.Instance.IdleAnimation();
		}
		
	}
}

