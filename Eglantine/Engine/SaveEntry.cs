using System;

namespace Eglantine
{
	public class SaveEntry
	{
		public int ID { get; private set; }
		public float GameTime { get; private set; }
		public int ItemMask { get; private set; }
		public string CurrentRoom {get; private set; }

		public SaveEntry (int id, float gameTime, int itemMask, string currentRoom)
		{
			ID = id;
			GameTime = gameTime;
			ItemMask = itemMask;
			CurrentRoom = currentRoom;
		}

		public void UpdateData(float gameTime, int itemMask, string currentRoom)
		{
			GameTime = gameTime;
			ItemMask = itemMask;
			CurrentRoom = currentRoom;
		}
	}
}

