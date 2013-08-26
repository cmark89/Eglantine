using System;

namespace Eglantine
{
	[Serializable]
	public class SaveEntry
	{
		public string FileName { get; private set; }
		public int ID { get; private set; }
		public float GameTime { get; private set; }
		public int ItemMask { get; private set; }
		public string CurrentRoom {get; private set; }

		private bool _flaggedForRemoval = false;
		public bool FalggedForRemoval {
			get { return _flaggedForRemoval; }
			private set { _flaggedForRemoval = value; }
		}

		public SaveEntry (string fileName, int id, float gameTime, int itemMask, string currentRoom)
		{
			FileName = fileName;
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

		public void ClearEntry()
		{
			_flaggedForRemoval = true;
		}
	}
}

