using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eglantine.Engine.Pathfinding;

namespace Eglantine.Engine
{
	public class Player
	{
		// Store the player character's position on screen.
		public Vector2 Position { get; private set; }

		// List of pathfinding nodes in the current path
		public List<NavNode> Path = new List<NavNode>();

		// The next waypoint the player is pathing to.
		private NavNode nextWaypoint;

		// The distance at which the player paths toward the next waypoint
		const float WAYPOINT_DISTANCE = 15f;

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
			// Temprorarily empty!
		}

		#region Pathfinding

		public void NextWaypoint ()
		{
			// The next waypoint is the last element in the path, so pop it.
			if (Path.Count > 0)
			{
				nextWaypoint = Path [Path.Count - 1];
				Path.RemoveAt (Path.Count - 1);
			}
			else
			{
				// Set the waypoint to null to stop pathfinding updates.
				nextWaypoint = null;
			}
		}

		public void SetPath(List<NavNode> path)
		{
			Path = path;
		}

		#endregion

		public void SetPosition(Vector2 pos)
		{
			Position = pos;
		}
	}
}

