using System;
using Microsoft.Xna.Framework;
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

			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, GetBestFacing());
			yield return waitUntil("Interact frame 4");
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

		public static IEnumerator<ScriptPauser> PlayInteractAnimation()
		{
			Scheduler.ExecuteWithArgs<Facing>(PlayInteractAnimation, GetBestFacing());
			yield return null;
		}
		
		public static IEnumerator<ScriptPauser> PlayInteractAnimation(Facing dir)
		{
			Facing oldFacing = Player.Instance.CurrentFacing;
			Player.Instance.PlayInteractAnimation(dir);
			yield return waitUntil("Interact" + dir.ToString() + " finished");

			Player.Instance.Sprite.PlayAnimation("Idle" + oldFacing.ToString());
		}

		public static Facing GetBestFacing ()
		{
			double rad = MathHelper.WrapAngle (Player.Instance.FacingDirection);

			if (rad < 0)
				rad += 2 * (float)Math.PI;

			if (rad >= Math.PI / 2 && rad < (Math.PI / 2) * 3)
			{
				return Facing.Left;
			}
			else
			{
				return Facing.Right;
			}
		}
	}
}

