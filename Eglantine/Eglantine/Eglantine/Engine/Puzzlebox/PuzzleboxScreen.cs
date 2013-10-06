using System;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class PuzzleboxScreen : Screen
	{
		// Singletonize me, cap'n!
		private static PuzzleboxScreen _instance;
		public static PuzzleboxScreen Instance
		{
			get
			{
				if(_instance == null)
					_instance = new PuzzleboxScreen(null);

				return _instance;
			}
		}

		#region Puzzle textures and constant values
		private const float RING_DRAG_THRESHOLD = 35;


		private static Texture2D PuzzleBGTexture;
		private static Texture2D Ring1Texture;
		private static Texture2D Ring2Texture;
		private static Texture2D Ring3Texture;
		private static Texture2D Ring4Texture;
		private static Texture2D Ring5Texture;
		private static Texture2D Ring6Texture;
		private static Texture2D Ring7Texture;
		private static Texture2D KeyTexture;
		private static Texture2D ButtonsTexture;
		private static Texture2D InsideBoxTexture;
		
		private static Texture2D FoldedNoteTexture;
		private static Texture2D StrangeCoinTexture;

		private static Texture2D OpenButtonTexture;
		private static Texture2D CloseButtonTexture;

		private static Vector2 Ring1Pos;
		private static Vector2 Ring2Pos;
		private static Vector2 Ring3Pos;
		private static Vector2 Ring4Pos;
		private static Vector2 Ring5Pos;
		private static Vector2 Ring6Pos;
		private static Vector2 Ring7Pos;
		private static Vector2 KeyPos;

		private static Rectangle AriesRect;
		private static Rectangle TaurusRect;
		private static Rectangle GeminiRect;
		private static Rectangle CancerRect;
		private static Rectangle LeoRect;
		private static Rectangle VirgoRect;
		private static Rectangle LibraRect;
		private static Rectangle ScorpioRect;
		private static Rectangle SagittariusRect;
		private static Rectangle CapricornRect;
		private static Rectangle AquariusRect;
		private static Rectangle PiscesRect;


		// Source rectangles
		private static Rectangle AriesSrcRect;
		private static Rectangle TaurusSrcRect;
		private static Rectangle GeminiSrcRect;
		private static Rectangle CancerSrcRect;
		private static Rectangle LeoSrcRect;
		private static Rectangle VirgoSrcRect;
		private static Rectangle LibraSrcRect;
		private static Rectangle ScorpioSrcRect;
		private static Rectangle SagittariusSrcRect;
		private static Rectangle CapricornSrcRect;
		private static Rectangle AquariusSrcRect;
		private static Rectangle PiscesSrcRect;


		#endregion

		public PuzzleboxState PuzzleboxState { get; protected set; }

		private Vector2 puzzleStart;
		private Vector2 center;

		private PuzzleboxScreenGUI window;
		private PuzzleboxScreenState screenState;

		private bool isDraggingRing;
		int draggingRing;
		Vector2 ringDragAnchor;
		float ringDragAnchorAngle;

		private Rectangle openButtonRect;
		private Color openButtonColor;

		private Rectangle noteRect;
		private Vector2 coinCenter;

		public PuzzleboxScreen (PuzzleboxState puzzleState)
		{
			_instance = this;
			PuzzleboxState = puzzleState;
			screenState = PuzzleboxScreenState.Closed;
			Initialize();
		}

		public override void Initialize ()
		{
			// Textures haven't been set yet...initialize!
			if (PuzzleBGTexture == null)
			{
				// Set all this stuff up, dingus
				PuzzleBGTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxBG");
				KeyTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleKey");
				Ring1Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing1");
				Ring2Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing2");
				Ring3Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing3");
				Ring4Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing4");
				Ring5Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing5");
				Ring6Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing6");
				Ring7Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleRing7");

				InsideBoxTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxInside");

				FoldedNoteTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/noteinbox");
				StrangeCoinTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/coininbox");

				OpenButtonTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxOpenButton");
				CloseButtonTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/PuzzleBoxCloseButton");

				ButtonsTexture = ContentLoader.Instance.Load<Texture2D>("Graphics/ZodiacButtons");



				// And the constant rectangles and draw points
				Ring1Pos = new Vector2(100, 3);
				Ring2Pos = new Vector2(130, 33);
				Ring3Pos = new Vector2(160, 63);
				Ring4Pos = new Vector2(200, 103);
				Ring5Pos = new Vector2(230, 133);
				Ring6Pos = new Vector2(270, 173);
				Ring7Pos = new Vector2(320, 223);
				KeyPos = new Vector2(358, 242);

				GeminiRect = new Rectangle(87, 41, 91, 40);
				PiscesRect = new Rectangle(38, 110, 91, 40);
				AquariusRect = new Rectangle(3, 199, 91, 40);
				LibraRect = new Rectangle(3, 361, 91, 40);
				TaurusRect = new Rectangle(38, 453, 91, 40);
				ScorpioRect = new Rectangle(87, 519, 91, 40);
				VirgoRect = new Rectangle(619, 41, 91, 40);
				CancerRect = new Rectangle(668, 110, 91, 40);
				AriesRect = new Rectangle(702, 199, 91, 40);
				CapricornRect = new Rectangle(702, 361, 91, 40);
				LeoRect = new Rectangle(668, 453, 91, 40);
				SagittariusRect = new Rectangle(619, 519, 91, 40);

				SagittariusSrcRect = new Rectangle(0, 0, 91, 40);
				LeoSrcRect = new Rectangle(0, 40, 91, 40);
				CapricornSrcRect = new Rectangle(0, 80, 91, 40);
				AriesSrcRect = new Rectangle(0, 120, 91, 40);
				CancerSrcRect = new Rectangle(0, 160, 91, 40);
				VirgoSrcRect = new Rectangle(0, 200, 91, 40);

				ScorpioSrcRect = new Rectangle(182, 0, 91, 40);
				TaurusSrcRect = new Rectangle(182, 40, 91, 40);
				LibraSrcRect = new Rectangle(182, 80, 91, 40);
				AquariusSrcRect = new Rectangle(182, 120, 91, 40);
				PiscesSrcRect = new Rectangle(182, 160, 91, 40);
				GeminiSrcRect = new Rectangle(182, 200, 91, 40);
			}

			window = new PuzzleboxScreenGUI(PuzzleBGTexture);
			openButtonRect = window.GetOpenButtonRect();
			puzzleStart = window.PuzzleStartPosition();
			center = KeyPos + new Vector2(KeyTexture.Width/2, KeyTexture.Height/2);

			coinCenter = new Vector2(545, 337) + puzzleStart;
			noteRect = OffsetRect(new Rectangle(156, 152, 177, 197), puzzleStart);
		}

		public override void Update (GameTime gameTime)
		{
			if (window != null)
			{
				window.Update (gameTime);
			}

			if (screenState == PuzzleboxScreenState.Open || PuzzleboxState.PuzzleSolved)
			{
				openButtonColor = Color.DimGray;
			}

			HandleInput();
		}

		public override void Draw (SpriteBatch spriteBatch)
		{
			if (window != null)
			{
				window.DrawBackground (spriteBatch);

				if(screenState == PuzzleboxScreenState.Closed)
					DrawPuzzle(spriteBatch);
				else
					DrawInsideBox(spriteBatch);

				window.DrawFrame(spriteBatch);

				DrawOpenButton(spriteBatch);
			}
		}

		public void DrawPuzzle (SpriteBatch spriteBatch)
		{
			// Draw the actual puzzle components here...

			// Draw the rings
			spriteBatch.Draw(Ring1Texture, position: puzzleStart + Ring1Pos + new Vector2(Ring1Texture.Width/2, Ring1Texture.Height/2), rotation: PuzzleboxState.Ring1Rotation, origin: new Vector2(Ring1Texture.Width/2, Ring1Texture.Height/2));
			spriteBatch.Draw(Ring2Texture, position: puzzleStart + Ring2Pos + new Vector2(Ring2Texture.Width/2, Ring2Texture.Height/2), rotation: PuzzleboxState.Ring2Rotation, origin: new Vector2(Ring2Texture.Width/2, Ring2Texture.Height/2));
			spriteBatch.Draw(Ring3Texture, position: puzzleStart + Ring3Pos + new Vector2(Ring3Texture.Width/2, Ring3Texture.Height/2), rotation: PuzzleboxState.Ring3Rotation, origin: new Vector2(Ring3Texture.Width/2, Ring3Texture.Height/2));
			spriteBatch.Draw(Ring4Texture, position: puzzleStart + Ring4Pos + new Vector2(Ring4Texture.Width/2, Ring4Texture.Height/2), rotation: PuzzleboxState.Ring4Rotation, origin: new Vector2(Ring4Texture.Width/2, Ring4Texture.Height/2));
			spriteBatch.Draw(Ring5Texture, position: puzzleStart + Ring5Pos + new Vector2(Ring5Texture.Width/2, Ring5Texture.Height/2), rotation: PuzzleboxState.Ring5Rotation, origin: new Vector2(Ring5Texture.Width/2, Ring5Texture.Height/2));
			spriteBatch.Draw(Ring6Texture, position: puzzleStart + Ring6Pos + new Vector2(Ring6Texture.Width/2, Ring6Texture.Height/2), rotation: PuzzleboxState.Ring6Rotation, origin: new Vector2(Ring6Texture.Width/2, Ring6Texture.Height/2));
			spriteBatch.Draw(Ring7Texture, position: puzzleStart + Ring7Pos + new Vector2(Ring7Texture.Width/2, Ring7Texture.Height/2), rotation: PuzzleboxState.Ring7Rotation, origin: new Vector2(Ring7Texture.Width/2, Ring7Texture.Height/2));

			// Draw the key if it is inserted
			if(PuzzleboxState.KeyInserted)
				spriteBatch.Draw(KeyTexture, position: puzzleStart + KeyPos);

			// Now draw all the zodiac buttons
			Rectangle buttonRect = new Rectangle();

			buttonRect = AriesSrcRect;
			if(PuzzleboxState.AriesPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(AriesRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = TaurusSrcRect;
			if(PuzzleboxState.TaurusPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(TaurusRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = GeminiSrcRect;
			if(PuzzleboxState.GeminiPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(GeminiRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = CancerSrcRect;
			if(PuzzleboxState.CancerPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(CancerRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = LeoSrcRect;
			if(PuzzleboxState.LeoPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(LeoRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = VirgoSrcRect;
			if(PuzzleboxState.VirgoPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(VirgoRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = LibraSrcRect;
			if(PuzzleboxState.LibraPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(LibraRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = ScorpioSrcRect;
			if(PuzzleboxState.ScorpioPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(ScorpioRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = SagittariusSrcRect;
			if(PuzzleboxState.SagittariusPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(SagittariusRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = CapricornSrcRect;
			if(PuzzleboxState.CapricornPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(CapricornRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = AquariusSrcRect;
			if(PuzzleboxState.AquariusPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(AquariusRect, puzzleStart), sourceRectangle: buttonRect);

			buttonRect = PiscesSrcRect;
			if(PuzzleboxState.PiscesPressed)
				buttonRect.X += 91;
			spriteBatch.Draw(ButtonsTexture, position: OffsetRectPosition(PiscesRect, puzzleStart), sourceRectangle: buttonRect);
		}


		public void DrawOpenButton (SpriteBatch spriteBatch)
		{
			// Draw the button to open the box if the puzzle has been solved
			if (screenState == PuzzleboxScreenState.Closed && PuzzleboxState.PuzzleSolved)
			{
				spriteBatch.Draw(OpenButtonTexture, drawRectangle: OffsetRect(openButtonRect, puzzleStart), color: openButtonColor);
			}
			else if (screenState == PuzzleboxScreenState.Open)
			{
				spriteBatch.Draw(CloseButtonTexture, drawRectangle: OffsetRect(openButtonRect, puzzleStart), color: openButtonColor);
			}
		}


		public void DrawInsideBox (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (InsideBoxTexture, position: puzzleStart);

			if (!GameState.Instance.PlayerHasItem ("Folded Note"))
			{
				spriteBatch.Draw (FoldedNoteTexture, position: puzzleStart + new Vector2(156, 152));
			}
			if (!GameState.Instance.PlayerHasItem ("Strange Coin"))
			{
				spriteBatch.Draw (StrangeCoinTexture, position: puzzleStart + new Vector2(464, 257));
			}
		}


		public void HandleInput ()
		{
			bool openButtonDrawn = false;
			if (PuzzleboxState.PuzzleSolved || screenState == PuzzleboxScreenState.Open)
				openButtonDrawn = true;

			if (openButtonDrawn && MouseManager.MouseInRect (OffsetRect(openButtonRect, puzzleStart)))
			{
				openButtonColor = Color.White;
				if(MouseManager.LeftClickUp)
					OpenBox();
			}

			if (screenState == PuzzleboxScreenState.Closed)
			{
				// Handle button presses
				if (MouseManager.LeftClickUp)
				{
					if (isDraggingRing)
					{
						isDraggingRing = false;
						PuzzleboxState.CheckIfSolved ();
					}
					else
					{
						bool pressed = false;

						if (MouseManager.MouseInRect (OffsetRect (AriesRect, puzzleStart)))
						{
							PuzzleboxState.AriesPressed = !PuzzleboxState.AriesPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (TaurusRect, puzzleStart)))
						{
							PuzzleboxState.TaurusPressed = !PuzzleboxState.TaurusPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (GeminiRect, puzzleStart)))
						{
							PuzzleboxState.GeminiPressed = !PuzzleboxState.GeminiPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (CancerRect, puzzleStart)))
						{
							PuzzleboxState.CancerPressed = !PuzzleboxState.CancerPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (LeoRect, puzzleStart)))
						{
							PuzzleboxState.LeoPressed = !PuzzleboxState.LeoPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (VirgoRect, puzzleStart)))
						{
							PuzzleboxState.VirgoPressed = !PuzzleboxState.VirgoPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (LibraRect, puzzleStart)))
						{
							PuzzleboxState.LibraPressed = !PuzzleboxState.LibraPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (ScorpioRect, puzzleStart)))
						{
							PuzzleboxState.ScorpioPressed = !PuzzleboxState.ScorpioPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (SagittariusRect, puzzleStart)))
						{
							PuzzleboxState.SagittariusPressed = !PuzzleboxState.SagittariusPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (CapricornRect, puzzleStart)))
						{
							PuzzleboxState.CapricornPressed = !PuzzleboxState.CapricornPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (AquariusRect, puzzleStart)))
						{
							PuzzleboxState.AquariusPressed = !PuzzleboxState.AquariusPressed;
							pressed = true;
						}
						else if (MouseManager.MouseInRect (OffsetRect (PiscesRect, puzzleStart)))
						{
							PuzzleboxState.PiscesPressed = !PuzzleboxState.PiscesPressed;
							pressed = true;
						}

						if(pressed)
						{
							EventManager.Instance.PlaySound ("switch");
							PuzzleboxState.CheckButtons ();
						}
					}
				}
				else if (PuzzleboxState.RingsUnlocked && !isDraggingRing && MouseManager.LeftButtonIsDown)
				{
					// Now handle player input on the rings.
					float mouseDist = Vector2.Distance (MouseManager.Position, puzzleStart + center);

					if (mouseDist > 22 && mouseDist <= 77)
					{
						// Mouse in Ring 7
						isDraggingRing = true;
						draggingRing = 7;
					}
					else if (mouseDist > 77 && mouseDist <= 127)
					{
						// Mouse in Ring 6
						isDraggingRing = true;
						draggingRing = 6;
					}
					else if (mouseDist > 127 && mouseDist <= 167)
					{
						// Mouse in Ring 5
						isDraggingRing = true;
						draggingRing = 5;
					}
					else if (mouseDist > 167 && mouseDist <= 197)
					{
						// Mouse in Ring 4
						isDraggingRing = true;
						draggingRing = 4;
					}
					else if (mouseDist > 197 && mouseDist <= 237)
					{
						// Mouse in Ring 3
						isDraggingRing = true;
						draggingRing = 3;
					}
					else if (mouseDist > 237 && mouseDist <= 267)
					{
						// Mouse in Ring 2
						isDraggingRing = true;
						draggingRing = 2;
					}
					else if (mouseDist > 267 && mouseDist <= 297)
					{
						// Mouse in Ring 1
						isDraggingRing = true;
						draggingRing = 1;
					}

					if(isDraggingRing)
					{
						ringDragAnchor = MouseManager.Position;
						ringDragAnchorAngle = GetAngleFromCenter(MouseManager.Position);
					}
				}


				if (isDraggingRing)
				{
					// Check if the difference in angle is sufficient to snap. Measure the change in mouse ANGLE instead of just position.
					float dragAngle = GetAngleFromCenter(MouseManager.Position) - ringDragAnchorAngle;
					if(Math.Abs(dragAngle) > PuzzleboxState.RING_DRAG_MAGNITUDE)
					{
						// Time to snap!
						if(dragAngle > 0)
							PuzzleboxState.RotateRing(draggingRing, 1);
						else
							PuzzleboxState.RotateRing(draggingRing, -1);

						ringDragAnchor = MouseManager.Position;
						ringDragAnchorAngle = GetAngleFromCenter(MouseManager.Position);

						// Play the sound
						EventManager.Instance.PlaySound ("boxring");
					}
				}
			}
			else
			{
				// Handle input for the --inside-- of the box
				if(!GameState.Instance.PlayerHasItem("Folded Note") && MouseManager.MouseInRect(noteRect))
				{
					MouseManager.MouseMode = MouseInteractMode.Grab;
					if(MouseManager.LeftClickUp)
					{
						EventManager.Instance.PlaySound("Extend");
						EventManager.Instance.GainItem("Folded Note");
					}
				}
				if(!GameState.Instance.PlayerHasItem("Strange Coin") && Vector2.Distance(MouseManager.Position, coinCenter) < 78)
				{

					if(MouseManager.LeftClickUp)
					{
						MouseManager.MouseMode = MouseInteractMode.Grab;
						EventManager.Instance.PlaySound("Extend");
						EventManager.Instance.GainItem("Strange Coin");
					}
				}
			}
		}

		public float GetAngleFromCenter (Vector2 mousePos)
		{
			Vector2 cent = puzzleStart + center;
			float radians = (float)Math.Atan2(mousePos.Y - cent.Y, mousePos.X - cent.X);

			return radians;
		}

		public void Close()
		{
			FlaggedForRemoval = true;
		}

		public Vector2 OffsetRectPosition(Rectangle rect, Vector2 offset)
		{
			return new Vector2(rect.X + offset.X, rect.Y + offset.Y);
		}

		public Rectangle OffsetRect(Rectangle rect, Vector2 offset)
		{
			return new Rectangle((int)(rect.X + offset.X), (int)(rect.Y + offset.Y), rect.Width, rect.Height);
		}

		public void OpenBox ()
		{
			// Toggle the state.
			if (screenState == PuzzleboxScreenState.Closed)
			{
				// Play open sound.
				EventManager.Instance.PlaySound ("boxopen");

				screenState = PuzzleboxScreenState.Open;
			}
			else
			{
				// Play close sound.
				EventManager.Instance.PlaySound ("boxclose");

				screenState = PuzzleboxScreenState.Closed;

			}
		}

		private enum PuzzleboxScreenState
		{
			Closed,
			Open
		}
	}
}

