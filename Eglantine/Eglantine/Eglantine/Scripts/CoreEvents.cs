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

		public static IEnumerator<ScriptPauser> startUndergroundSounds ()
		{
			//EventManager.Instance.StopLoopingSoundEffects();
			if (!GameState.Instance.DrumsPlaying)
			{
				EventManager.Instance.PlaySoundLooping ("UndergroundDrums1", .0f, 0, 0);
				EventManager.Instance.PlaySoundLooping ("UndergroundDrums2", .0f, 0, 0);
				EventManager.Instance.PlaySoundLooping ("UndergroundDrums3", .0f, 0, 0);

				GameState.Instance.DrumsPlaying = true;
			}

			yield return null;
			Scheduler.Execute(undergroundSoundUpdate);
		}

		public static IEnumerator<ScriptPauser> undergroundSoundUpdate ()
		{
			AudioManager.Instance.SetLoopingSoundEffectVolume ("UndergroundDrums1", 0f);
			AudioManager.Instance.SetLoopingSoundEffectVolume ("UndergroundDrums2", 0f);
			AudioManager.Instance.SetLoopingSoundEffectVolume ("UndergroundDrums3", 0f);

			if (GameState.Instance.CurrentRoom.Name == "SecretRoom" && GameState.Instance.TrapdoorOpen)
			{
				AudioManager.Instance.SetLoopingSoundEffectVolume("UndergroundDrums1", .2f);
			}
			if (GameState.Instance.CurrentRoom.Name == "Underground1")
			{
				AudioManager.Instance.SetLoopingSoundEffectVolume("UndergroundDrums1", .4f);
			} 
			else if (GameState.Instance.CurrentRoom.Name == "Underground2")
			{
				AudioManager.Instance.SetLoopingSoundEffectVolume("UndergroundDrums2", .6f);
			} 
			else if (GameState.Instance.CurrentRoom.Name == "Underground3")
			{
				AudioManager.Instance.SetLoopingSoundEffectVolume("UndergroundDrums3", .6f);
			}

			yield return null;
		}

		public static IEnumerator<ScriptPauser> killUndergroundSounds()
		{
			AudioManager.Instance.StopSoundEffect("UndergroundDrums1");
			AudioManager.Instance.StopSoundEffect("UndergroundDrums2");
			AudioManager.Instance.StopSoundEffect("UndergroundDrums3");

			GameState.Instance.DrumsPlaying = false;

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

