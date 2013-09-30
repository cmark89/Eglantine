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

		public static void pickup(string itemName)
		{
			Scheduler.ExecuteWithArgs<string>(pickupScript, itemName);
		}

		public static IEnumerator<ScriptPauser> pickupScript(string itemName)
		{
			EventManager.Instance.MovePlayerTo(itemName);
			yield return waitUntil("Player stopped");

			EventManager.Instance.GainItem(itemName);
			EventManager.Instance.DisableInteractable(itemName);
			EventManager.Instance.PlaySound("Extend");
		}

		public static void door (string doorName, string targetRoom, string targetEntrance)
		{
			Scheduler.ExecuteWithArgs<string, string, string>(doorScript, doorName, targetRoom, targetEntrance);
		}

		public static IEnumerator<ScriptPauser> doorScript(string doorName, string targetRoom, string targetEntrance)
		{
			EventManager.Instance.MovePlayerTo(doorName);
			yield return waitUntil("Player stopped");
			EventManager.Instance.ChangeRoom(targetRoom, targetEntrance);
		}

		public static IEnumerator<ScriptPauser> startOutdoorSounds()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("naturalwind", .3f, 0f, 0f);
			
			yield return null;
		}

		public static IEnumerator<ScriptPauser> startIndoorSounds()
		{
			EventManager.Instance.StopLoopingSoundEffects();
			EventManager.Instance.PlaySoundLooping("lowwind", .25f, 0, 0);
			
			yield return null;
		}
	}
}

