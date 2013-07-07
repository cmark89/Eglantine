using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class DocumentScreen : Screen
	{
		// Singletonize me, cap'n!
		private static DocumentScreen _instance;
		public static DocumentScreen Instance
		{
			get
			{
				if(_instance == null)
					_instance = new DocumentScreen(null);

				return _instance;
			}
		}

		private Document thisDocument;
		private int currentPage;
		private DocumentScreenGUI window;

		public DocumentScreen (Document doc)
		{
			Initialize();
			SetDocument(doc);
		}

		public override void Initialize()
		{
		}

		public void SetDocument (Document doc)
		{
			thisDocument = doc;
			currentPage = 0;
			SetupWindow();
		}

		private void SetupWindow()
		{
			window = new DocumentScreenGUI();
			window.SetPage(thisDocument.Pages[currentPage].Texture, currentPage + 1, thisDocument.Pages.Count);
		}

		public void GoToPreviousPage()
		{
			currentPage--;
			window.SetPage(thisDocument.Pages[currentPage].Texture, currentPage + 1, thisDocument.Pages.Count);
		}

		public void GoToNextPage()
		{
			currentPage++;
			window.SetPage(thisDocument.Pages[currentPage].Texture, currentPage + 1, thisDocument.Pages.Count);
		}

		public void Close()
		{
			// Close the window...
			window = null;
			this.FlaggedForRemoval = true;
		}

		public override void Update(GameTime gameTime)
		{
			if(window != null)
				window.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if(window != null)
				window.Draw(spriteBatch);
		}
	}
}

