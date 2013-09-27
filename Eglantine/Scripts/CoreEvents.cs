using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		public static ScriptPauser waitSeconds(float time)
		{
			return ScriptPauser.WaitSeconds(time);
		}

		public static ScriptPauser waitUntil (string signal)
		{
			return ScriptPauser.WaitForSignal(signal);
		}

		public static IEnumerator<ScriptPauser> pickup(string itemName)
		{
			EventManager.Instance.MovePlayerTo(itemName);
			yield return waitUntil("Player stopped");

			EventManager.Instance.GainItem(itemName);
			EventManager.Instance.DisableInteractable(itemName);
			EventManager.Instance.PlaySound("Extend");
		}

		public static IEnumerator<ScriptPauser> door(string doorName, string targetRoom, string targetEntrance)
		{
			EventManager.Instance.MovePlayerTo(doorName);
			yield return waitUntil("Player stopped");
			EventManager.Instance.ChangeRoom(targetRoom, targetEntrance);
		}
	}
}

