using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{

// --DOORS-- //
		public static IEnumerator<ScriptPauser> useFoyerDoor_FrontYard()
		{
			door("FrontDoor", "FrontYard", "FrontDoor");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_Kitchen()
		{
			door("KitchenDoor", "Kitchen", "FoyerDoor");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_Upstairs()
		{
			door("Stairs", "Upstairs", "Stairs");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("stairs", .9f, 0f, 0f);
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_LivingRoom()
		{
			door("LivingRoomDoor", "LivingRoom", "Door");
			yield return waitUntil("Player stopped");
			EventManager.Instance.PlaySound("door", .9f, 0f, 0f);
		}

	}
}

