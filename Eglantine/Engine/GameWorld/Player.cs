using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;

namespace Eglantine.Engine
{
	public class Player
	{
		// The world's most temporary thing:
		public Texture2D Texture { get; private set; }

		public AnimatedSprite Sprite { get; private set; }

		// Store the player character's position on screen.
		public Vector2 Position { get; private set; }

		// This determines which direction the player is facing.
		public float FacingDirection { get; private set; }
		public Facing CurrentFacing { get; private set; }

		// List of pathfinding nodes in the current path
		public List<NavNode> Path = new List<NavNode>();

		// The next waypoint the player is pathing to.
		private NavNode nextWaypoint;

		private bool forcedMovement = false;

		// The distance at which the player paths toward the next waypoint
		const float WAYPOINT_DISTANCE = 24f;

		// Movement speed per second
		const float MOVEMENT_SPEED = 220f;
		float MovementSpeed;

		// Player implements the singleton pattern.
		private static Player _instance;
		public static Player Instance
		{
			get
			{
				if(_instance == null)
				_instance = new Player();

				return _instance;
			}
		}

		public Player ()
		{
			Texture = ContentLoader.Instance.Load<Texture2D>("Graphics/SpriteSheet");
			SetupAnimatedSprite();
		}

		private void SetupAnimatedSprite()
		{
			Sprite = new AnimatedSprite(Texture, 15);

			Sprite.AddAnimation("IdleDown", 400, 200, new int[] { 60 }, true);
			Sprite.AddAnimation("IdleLeft", 400, 200, new int[] { 61 }, true);
			Sprite.AddAnimation("IdleRight", 400, 200, new int[] { 63 }, true);
			Sprite.AddAnimation("IdleUp", 400, 200, new int[] { 62 }, true);

			Sprite.AddAnimation("WalkDown", 400, 200, new int[] { 0,1,2,3,4,5,6,7,8,9 }, true);
			Sprite.AddAnimation("WalkLeft", 400, 200, new int[] { 10,11,12,13,14,15,16,17,18,19 }, true);
			Sprite.AddAnimation("WalkRight", 400, 200, new int[] { 20,21,22,23,24,25,26,27,28,29 }, true);
			Sprite.AddAnimation("WalkUp", 400, 200, new int[] { 30,31,32,33,34,35,36,37,38,39 }, true);

			Sprite.AddAnimation("InteractLeft", 400, 200, new int[] { 40,41,42,43,44,45,46,47 }, false);
			Sprite.AddAnimation("InteractRight", 400, 200, new int[] { 50,51,52,53,54,55,56,57 }, false);

			Sprite.Origin = new Vector2(100, 365);
			Sprite.Color = Color.White;
			Sprite.Scale = new Vector2(1f,1f);

			// Start it off
			Sprite.PlayAnimation("IdleDown");
		}

		public void Update (GameTime gameTime)
		{
			// If the player is pathing
			if (nextWaypoint != null)
			{
				// Move toward the waypoint.
				Position += Vector2.Normalize(nextWaypoint.Position - Position) * MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

				// If the player is close enough to the waypoint, get the next one.
				if(Vector2.Distance(Position, nextWaypoint.Position) <= WAYPOINT_DISTANCE)
					NextWaypoint();
			}

			Sprite.Position = Position;
			UpdateScale();

			Sprite.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Sprite.Draw(spriteBatch);
		}

		#region Pathfinding

		public void NextWaypoint ()
		{
			// The next waypoint is not the last element in the path, so pop it.
			if (Path != null && Path.Count > 0)
			{
				nextWaypoint = Path [Path.Count - 1];
				Path.RemoveAt (Path.Count - 1);

				// Get the direction from here to the next waypoint
				float direction = (float)Math.Atan2(nextWaypoint.Position.Y - Position.Y, nextWaypoint.Position.X - Position.X);
				FacingDirection = direction;


				if(RadiansToFacing(FacingDirection) != CurrentFacing)
				{
					// The facing has changed, so change the player's 
					CurrentFacing = RadiansToFacing(FacingDirection);
				}

				// Handle the animation!
				if(Sprite.CurrentAnimationName.Contains("Idle"))
				{
					Sprite.PlayAnimation("Walk" + CurrentFacing);
				}
				else if(Sprite.CurrentAnimationName != "Walk" + CurrentFacing.ToString())
				{
					Sprite.ChangeAnimation("Walk" + CurrentFacing.ToString());
				}
			}
			else
			{
				StopMoving ();
			}
		}

		public void SetPath (List<NavNode> path, bool forced)
		{
			forcedMovement = forced;

			nextWaypoint = null;


			Path = path;

			if (path != null)
			{
				// Here, check to make sure that the target point is outside the waypoint stopping distance.
				// This stops a player from nudging around if they are forced to move to their current position
				if(Vector2.Distance (Position, path[0].Position) < WAYPOINT_DISTANCE)
					StopMoving ();
				else
				{
					// Start moving
					NextWaypoint ();
				}
			}


		}

		public void StopMoving ()
		{
			Path.Clear();
			nextWaypoint = null;
			Sprite.PlayAnimation("Idle" + GetFacing().ToString());
			if(forcedMovement)
			{
				EventManager.Instance.SendSignal("Player stopped");
				AdventureScreen.Instance.EnableInput();
			}
		}

		#endregion

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
		}

		public Facing GetFacing ()
		{
			FacingDirection = MathHelper.WrapAngle (FacingDirection);
			return RadiansToFacing(FacingDirection);
		}

		public Facing RadiansToFacing (float rad)
		{
			rad = MathHelper.WrapAngle (rad);
			if(rad < 0) rad += 2 * (float)Math.PI;
			Console.WriteLine(rad);
			
			if (rad >= (Math.PI / 4) * 1 && rad < (Math.PI / 4) * 3)
			{
				return Facing.Down;
			}
			else if (rad >= (Math.PI / 4) * 3 && rad < (Math.PI / 4) * 5)
			{
				return Facing.Left;
			}
			else if (rad >= (Math.PI / 4) * 5 && rad < (Math.PI / 4) * 7)
			{
				return Facing.Up;
			}
			else
			{
				return Facing.Right;
			}
		}

		public void SetFacing(Facing facing)
		{
			if(facing == Facing.Right)
				FacingDirection = 0;
			else if(facing == Facing.Down)
				FacingDirection = (float)Math.PI / 2;
			else if(facing == Facing.Left)
				FacingDirection = (float)Math.PI;
			else if(facing == Facing.Up)
				FacingDirection = ((float)Math.PI / 2f) * 3;
		}

		public void PlayInteractAnimation()
		{

		}

		public void UpdateScale ()
		{
			float scale;

			float minY = GameState.Instance.CurrentRoom.MinYValue;
			float maxY = GameState.Instance.CurrentRoom.MaxYValue;

			// Get the percent toward the bottom of the screen the player is
			float percent = (Position.Y - maxY) / (minY - maxY);
			Console.WriteLine("Min: " + minY + " -- Player: " + Position.Y + " -- Max: " + maxY);

			float minYScale = GameState.Instance.CurrentRoom.MinYScale;
			float maxYScale = GameState.Instance.CurrentRoom.MaxYScale;

			// Now lerp toward the minimum scale
			Console.WriteLine("Lerp between: " + maxYScale + " and " + minYScale + " with magnitude " + percent);
			scale = MathHelper.Lerp(maxYScale, minYScale, percent);

			Console.WriteLine("Scale: " + scale);
			Sprite.Scale = new Vector2(scale, scale);

			MovementSpeed = scale * MOVEMENT_SPEED;
		}
	}
}

