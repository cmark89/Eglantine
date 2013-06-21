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

		// Store the player character's position on screen.
		public Vector2 Position { get; private set; }

		// List of pathfinding nodes in the current path
		public List<NavNode> Path = new List<NavNode>();

		// The next waypoint the player is pathing to.
		private NavNode nextWaypoint;

		private bool forcedMovement = false;

		// The distance at which the player paths toward the next waypoint
		const float WAYPOINT_DISTANCE = 18f;

		// Movement speed per second
		const float MOVEMENT_SPEED = 100f;

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
			Texture = ContentLoader.Instance.Load<Texture2D>("MayStickfigure");
		}

		public void Update (GameTime gameTime)
		{
			// If the player is pathing
			if (nextWaypoint != null)
			{
				// Move toward the waypoint.
				Position += Vector2.Normalize(nextWaypoint.Position - Position) * MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

				// If the player is close enough to the waypoint, get the next one.
				if(Vector2.Distance(Position, nextWaypoint.Position) <= WAYPOINT_DISTANCE)
					NextWaypoint();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Temprorary until animatiosn exist:
			spriteBatch.Draw(Texture, new Vector2(Position.X - (Texture.Width / 2), Position.Y - (Texture.Height - 15)), Color.White);
		}

		#region Pathfinding

		public void NextWaypoint ()
		{
			// The next waypoint is the last element in the path, so pop it.
			if (Path != null && Path.Count > 0)
			{
				nextWaypoint = Path [Path.Count - 1];
				Path.RemoveAt (Path.Count - 1);
			}
			else
			{
				// Set the waypoint to null to stop pathfinding updates.
				nextWaypoint = null;
				if(forcedMovement)
				{
					EventManager.Instance.SendSignal("Player stopped");
					AdventureScreen.Instance.EnableInput();
				}
			}
		}

		public void SetPath(List<NavNode> path, bool forced)
		{
			forcedMovement = forced;

			nextWaypoint = null;
			Path = path;

			if(path != null)
				NextWaypoint();
		}

		public void StopMoving ()
		{
			Path.Clear();
			nextWaypoint = null;
		}

		#endregion

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
		}
	}
}

