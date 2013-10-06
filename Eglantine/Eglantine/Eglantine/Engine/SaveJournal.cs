/// <summary>
/// The save journal class is a small file serialized in the save directory that keeps a listing of all 
/// save files as they are created.  When it is loaded by the game it provides a snapshot of the player's 
/// progress.
/// </summary>


using System;
using System.IO;
using System.Collections.Generic;

namespace Eglantine
{
	[Serializable]
	public class SaveJournal
	{
		public const string SAVE_PATH = "Save/";
		public const string JOURNAL_FILE_PATH = ".savelog.bin";
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
			VerifyIntegrity();
			AlignIndexes ();

			return _instance;
		}

		public static void SerializeJournal()
		{
			// Serialize the save journal file.
			Serializer.Serialize<SaveJournal>(SAVE_PATH + JOURNAL_FILE_PATH, _instance);
		}

		// This returns the file name assigned to the save
		public static string AddSaveEntry (int id, float gameTime, int itemMask, string currentRoom)
		{
			string fileName = "";

			// First, check to see if the game ID exists in the dictionary
			SaveEntry thisEntry = null;
			foreach (KeyValuePair<int, SaveEntry> k in _instance.SaveData)
			{
				if (k.Value.ID == id)
				{
					thisEntry = k.Value;
					fileName = thisEntry.FileName;
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

				fileName = String.Concat ("save", SaveFiles, ".sav");
				_instance.SaveData.Add (SaveFiles, new SaveEntry(fileName, id, gameTime, itemMask, currentRoom));
				SerializeJournal ();

				SaveFiles++;
			}

			// Return the name of the saved file (in long form)
			return String.Concat(SAVE_PATH, fileName);
		}

		public static Dictionary<int, SaveEntry> GetSaveData()
		{
			return _instance.SaveData;
		}

		// Verify that the SaveJournal is properly built and refers to existing files
		public static void VerifyIntegrity ()
		{
			List<int> invalidIndexes = new List<int> ();
			foreach (KeyValuePair<int, SaveEntry> k in _instance.SaveData)
			{
				if (!File.Exists (SAVE_PATH + k.Value.FileName))
				{
					Console.WriteLine ("Could not find " + k.Value.FileName + ".  Removing from journal.");

					// If the save file no longer exists, flag it for removal
					k.Value.ClearEntry ();
					invalidIndexes.Add (k.Key);
				}
				else
				{
					Console.WriteLine (k.Value.FileName + " successfully read by journal.");
				}
			}

			// Now loop over all elements and remove the ones that are no longer needed.
			foreach (int i in invalidIndexes)
			{
				_instance.SaveData.Remove(i);
			}

			// Finally, update the save file count
			SaveFiles = _instance.SaveData.Count;
		}

		// Adjusts the indexes of all save files and aligns them neatly in the journal
		public static void AlignIndexes ()
		{
			Dictionary<int, SaveEntry> aligned = new Dictionary<int, SaveEntry>();

			int i = 0;
			SaveEntry temp;

			foreach (KeyValuePair<int, SaveEntry> k in _instance.SaveData)
			{
				temp = k.Value;
				aligned[i] = temp;
				i++;
			}

			_instance.SaveData = aligned;
		}
	}
}

