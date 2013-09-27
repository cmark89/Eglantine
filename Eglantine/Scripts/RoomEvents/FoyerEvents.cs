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
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_Kitchen()
		{
			door("KitchenDoor", "Kitchen", "FoyerDoor");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_Upstairs()
		{
			door("Stairs", "Upstairs", "Stairs");
			yield return null;
		}

		public static IEnumerator<ScriptPauser> useFoyerDoor_LivingRoom()
		{
			door("LivingRoomDoor", "LivingRoom", "Door");
			yield return null;
		}

	}
}

