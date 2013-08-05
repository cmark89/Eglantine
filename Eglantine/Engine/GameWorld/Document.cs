using System;
using System.Collections.Generic;
using Eglantine.Engine;
using LuaInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	[Serializable]
	public class Document
	{
		public string Name { get; protected set; }
		public List<DocumentPage> Pages { get; protected set; }

		public Document(string docName)
		{
			BuildDocument(docName);
		}

		void BuildDocument (string docName)
		{
			Name = docName;
			Console.WriteLine("Build document " + docName);
			Pages = new List<DocumentPage> ();

			LuaTable docTable = Eglantine.Lua.GetTable ("documents");
			if(docTable[docName] != null)
			{
				docTable = (LuaTable)docTable[docName];
				for(int i = 0; i < docTable.Keys.Count; i++)
				{
					AddPage((string)docTable[i+1]);
				}
			}
			else
			{
				Console.WriteLine("Could not find LuaTable documents["+docName+"]");
			}
		}

		// Add the named page to the end of the list of pages
		public void AddPage (string newPage)
		{
			Pages.Add(CreatePage(newPage));
		}

		// Create a new page given the name of the texture
		public DocumentPage CreatePage (string newPage)
		{
			return new DocumentPage(ContentLoader.Instance.Load<Texture2D>(newPage));
		}

		// Replaces the page at the given index with the named texture
		public void ChangePage (int pageNum, string newPage)
		{
			DocumentPage oldPage = Pages[pageNum];

			Pages.Insert(pageNum, CreatePage(newPage));
			Pages.Remove(oldPage);
		}

		public void PrepareForSerialization ()
		{
			foreach (DocumentPage d in Pages)
			{
				d.PrepareForSerialization();
			}
		}

		// Wrapper for the page texture and its dimensions
		[Serializable]
		public class DocumentPage
		{
			[NonSerialized]
			private Texture2D _texture;
			public Texture2D Texture 
			{
				get { return _texture; }
				private set { _texture = value; }
			}
			private string _TextureName;

			public float Width
			{
				get { return Texture.Width; }
			}

			public float Height
			{
				get { return Texture.Height; }
			}

			public float X
			{
				get { return (Eglantine.GAME_WIDTH - Width) / 2f; }
			}

			public float Y
			{
				get { return (Eglantine.GAME_HEIGHT - Height) / 2f; }
			}

			public DocumentPage(Texture2D pageTexture)
			{
				Texture = pageTexture;
			}

			public void PrepareForSerialization()
			{
				_TextureName = Texture.Name;
			}
		}
	}
}

