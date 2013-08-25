/// <summary>
/// The save journal class is a small file serialized in the save directory that keeps a listing of all 
/// save files as they are created.  When it is loaded by the game it provides a snapshot of the player's 
/// progress.
/// </summary>


using System;
using System.Collections.Generic;

namespace Eglantine
{
	[Serializable]
	public class SaveJournal
	{
		private const string SAVE_PATH = "Save/";
		private const string JOURNAL_FILE_PATH = "savedata.sj";
		private static SaveJournal _instance;
		private static int SaveFiles = 0;
		public Dictionary<string, SaveEntry> SaveData = new Dictionary<string, SaveEntry>();


		public static void DeserializeJournal()
		{
			// Deserialize the save journal file.
			_instance = Serializer.Deserialize<SaveJournal> (SAVE_PATH + JOURNAL_FILE_PATH);
			SaveFiles = _instance.SaveData.Keys.Count;
		}

		public static void SerializeJournal()
		{
			// Serialize the save journal file.
			Serializer.Serialize<SaveJournal>(SAVE_PATH + JOURNAL_FILE_PATH, _instance);
		}

		public static void AddSaveEntry (int id, float gameTime, int itemMask, string currentRoom)
		{
			// First, check to see if the game ID exists in the dictionary
			SaveEntry thisEntry = null;
			foreach (KeyValuePair<string, SaveEntry> k in SaveData)
			{
				if (k.Value.ID == id)
				{
					thisEntry = k.Value;
					break;
				}
			}

			// The ID exists in the journal, so update the save data.
			if (thisEntry != null)
			{
				thisEntry.UpdateData (gameTime, itemMask, currentRoom);
				SerializeJournal ();
			}
			// The ID does not exist, so create a new entry
			else
			{
				SaveFiles++;
				string fileName = String.Concat (SAVE_PATH, "save_", SaveFiles);
				SaveData.Add (fileName, new SaveEntry(fileName, gameTime, itemMask, currentRoom));
				SerializeJournal ();
			}
		}

		public static Dictionary<string, SaveEntry> GetSaveData()
		{
			return _instance.SaveData;
		}
	}
}

