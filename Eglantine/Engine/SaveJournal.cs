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
		public const string SAVE_PATH = "Save/";
		public const string JOURNAL_FILE_PATH = "savelog.bin";
		private static SaveJournal _instance;
		private static int SaveFiles = 0;
		public Dictionary<int, SaveEntry> SaveData = new Dictionary<int, SaveEntry>();

		public static void CreateNewJournal()
		{
			_instance = new SaveJournal();
			_instance.SaveData = new Dictionary<int, SaveEntry>();
			SerializeJournal();
		}

		public static SaveJournal DeserializeJournal()
		{
			// Deserialize the save journal file.
			_instance = Serializer.Deserialize<SaveJournal> (SAVE_PATH + JOURNAL_FILE_PATH);
			SaveFiles = _instance.SaveData.Keys.Count;

			return _instance;
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
			foreach (KeyValuePair<int, SaveEntry> k in _instance.SaveData)
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
				string fileName = String.Concat (SAVE_PATH, "save", SaveFiles);
				_instance.SaveData.Add (SaveFiles, new SaveEntry(fileName, id, gameTime, itemMask, currentRoom));
				SerializeJournal ();
				SaveFiles++;
			}
		}

		public static Dictionary<int, SaveEntry> GetSaveData()
		{
			return _instance.SaveData;
		}
	}
}

