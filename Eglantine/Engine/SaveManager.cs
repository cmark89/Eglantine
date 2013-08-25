using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine.Engine
{
	public class SaveManager
	{
		private static SaveJournal journal;
		public static bool Shown = false;

		static Dictionary<string, Texture2D> stampTextures;
		static Dictionary<string, Texture2D> itemIcons;
		// Content used for displaying save files
		static Texture2D saveFrameTexture;

		static SpriteFont timeFont;

		private static int startX;
		const int Y_PADDING = 15;


		public SaveManager ()
		{

		}

		public void ToggleLoadScreen()
		{
			EventManager.Instance.PlaySound ("pageturn");
			Shown = !Shown;
		}

		public static void Initialize ()
		{
			// If the Save folder doesn't exist, then we must create it.
			string dir = AppDomain.CurrentDomain.BaseDirectory.ToString ();
			if (!Directory.Exists (dir + SaveJournal.SAVE_PATH))
			{
				Directory.CreateDirectory (dir + SaveJournal.SAVE_PATH);
			}

			// If there is no save journal, then we need to create one
			if (!File.Exists (dir + SaveJournal.SAVE_PATH + SaveJournal.JOURNAL_FILE_PATH))
			{
				SaveJournal.CreateNewJournal();
			}

			// Now grab the save journal from file.
			journal = SaveJournal.DeserializeJournal ();
			LoadContent ();

			startX = (Eglantine.GAME_WIDTH - saveFrameTexture.Width) / 2;
		}

		public static void LoadContent()
		{
			if(saveFrameTexture == null || stampTextures == null)
			{
				saveFrameTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Save_slot");

				// Frames for the saved location
				stampTextures = new Dictionary<string, Texture2D>();
				stampTextures.Add ("BackYard", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Backyard_stamp"));
				stampTextures.Add ("Bathroom", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Bathroom_stamp"));
				stampTextures.Add ("Bedroom", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Bedroom_stamp"));
				stampTextures.Add ("EmptyRoom", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/EmptyRoom_stamp"));
				stampTextures.Add ("Foyer", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Foyer_stamp"));
				stampTextures.Add ("FrontYard", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/FrontYard_stamp"));
				stampTextures.Add ("Kitchen", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Kitchen_stamp"));
				stampTextures.Add ("LivingRoom", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/LivingRoom_stamp"));
				stampTextures.Add ("Office", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Office_stamp"));
				stampTextures.Add ("SecretRoom", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/SecretRoom_stamp"));
				stampTextures.Add ("Underground1", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Underground1_stamp"));
				stampTextures.Add ("Underground2", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Underground2_stamp"));
				stampTextures.Add ("Underground3", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Underground3_stamp"));
				stampTextures.Add ("Underground4", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Underground4_stamp"));
				stampTextures.Add ("Upstairs", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Upstairs_stamp"));

				// Item icons
				itemIcons = new Dictionary<string, Texture2D>();
				itemIcons.Add ("Blank", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Blank_icon"));
				itemIcons.Add ("Blueprints", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Blueprints_icon"));
				itemIcons.Add ("Coin", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Coin_icon"));
				itemIcons.Add ("Crowbar", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Crowbar_icon"));
				itemIcons.Add ("Eglantine", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Eglantine_icon"));
				itemIcons.Add ("FoldedNote", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/FoldedNote_icon"));
				itemIcons.Add ("Journal", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Journal_icon"));
				itemIcons.Add ("Key", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Key_icon"));
				itemIcons.Add ("Letter", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Letter_icon"));
				itemIcons.Add ("Notes", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Notes_icon"));
				itemIcons.Add ("Photograph", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Photograph_icon"));
				itemIcons.Add ("Puzzlebox", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Puzzlebox_icon"));
				itemIcons.Add ("Puzzlekey", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Puzzlekey_icon"));
				itemIcons.Add ("Scissors", ContentLoader.Instance.Load<Texture2D>("Graphics/Client/Scissors_icon"));

				timeFont = ContentLoader.Instance.Load<SpriteFont>("Fonts/Dustismo20");
			}
		}


		public static void Update (GameTime gameTime)
		{
			if(!Shown)
				return;

			for (int i = 0; i < journal.SaveData.Keys.Count; i++)
			{
				if(GetSaveRect (i).Contains ((int)MouseManager.Position.X, (int)MouseManager.Position.Y))
				{
					MouseManager.MouseMode = MouseInteractMode.Hot;
					if(MouseManager.LeftClickUp)
					{
						Eglantine.ChangeScene (new GameScene (GameState.LoadState (SaveJournal.SAVE_PATH + journal.SaveData[i].FileName)));
						Shown = false;
					}
				}
			}
		}

		public static Rectangle GetSaveRect (int i)
		{
			int x = 0;
			int y = 0 + (Y_PADDING + saveFrameTexture.Height) * i;
			int width = saveFrameTexture.Width;
			int height = saveFrameTexture.Height;

			return new Rectangle(x, y, width, height);
		}

		public static void Draw (SpriteBatch spriteBatch)
		{
			if(!Shown)
				return;

			Vector2 drawPoint = Vector2.Zero;
			for (int i = 0; i < journal.SaveData.Keys.Count; i++)
			{
				drawPoint = new Vector2(startX, (saveFrameTexture.Height + Y_PADDING) * i);
				SaveEntry thisEntry = journal.SaveData[i];

				// Draw the background
				spriteBatch.Draw (saveFrameTexture, position: drawPoint, color:Color.White);

				// Now write the time
				spriteBatch.DrawString (timeFont, ParseTime(thisEntry.GameTime), drawPoint + new Vector2(361,8), color: Color.DarkBlue);

				// Draw the stamp
				spriteBatch.Draw (stampTextures[thisEntry.CurrentRoom], position: drawPoint + new Vector2(10, 10), color: Color.White);

				// Finally, draw the items that the player currently has
				int mask = thisEntry.ItemMask;


				Texture2D thisTexture;

				Vector2 thisIcon = drawPoint + new Vector2(202, 92);
				if((mask & (int)ItemID.Scissors) != 0) thisTexture = itemIcons["Scissors"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(202, 114);
				if((mask & (int)ItemID.Crowbar) != 0) thisTexture = itemIcons["Crowbar"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(293, 92);
				if((mask & (int)ItemID.Journal) != 0) thisTexture = itemIcons["Journal"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(293, 114);
				if((mask & (int)ItemID.Notes) != 0) thisTexture = itemIcons["Notes"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(331, 92);
				if((mask & (int)ItemID.Blueprints) != 0) thisTexture = itemIcons["Blueprints"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(331, 114);
				if((mask & (int)ItemID.Letter) != 0) thisTexture = itemIcons["Letter"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(368, 92);
				if((mask & (int)ItemID.Photograph) != 0) thisTexture = itemIcons["Photograph"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(368, 114);
				if((mask & (int)ItemID.FoldedNote) != 0) thisTexture = itemIcons["FoldedNote"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(459, 92);
				if((mask & (int)ItemID.Puzzlebox) != 0) thisTexture = itemIcons["Puzzlebox"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(459, 114);
				if((mask & (int)ItemID.Puzzlekey) != 0) thisTexture = itemIcons["Puzzlekey"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(498, 92);
				if((mask & (int)ItemID.Coin) != 0) thisTexture = itemIcons["Coin"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);
				
				thisIcon = drawPoint + new Vector2(498, 114);
				if((mask & (int)ItemID.Key) != 0) thisTexture = itemIcons["Key"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(607, 86);
				if((mask & ((int)ItemID.Eglantine << 0)) != 0) thisTexture = itemIcons["Eglantine"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(607, 105);
				if((mask & ((int)ItemID.Eglantine << 1)) != 0) thisTexture = itemIcons["Eglantine"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

				thisIcon = drawPoint + new Vector2(607, 122);
				if((mask & ((int)ItemID.Eglantine << 2)) != 0) thisTexture = itemIcons["Eglantine"]; else thisTexture = itemIcons["Blank"];
				spriteBatch.Draw (thisTexture, position: thisIcon, color: Color.White);

			}
		}


		public static string ParseTime(float time)
		{
			int totalSeconds = (int)time;
			int totalMinutes = (int)totalSeconds / 60;

			int hours = (int)totalMinutes / 60;
			int minutes = totalMinutes - (hours * 60);
			int seconds = totalSeconds - (minutes * 60) - (hours * 3600);

			return String.Format ("{0:D}:{1:D2}:{2:D2}", hours, minutes, seconds);
		}

	}
}

