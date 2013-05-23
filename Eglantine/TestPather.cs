using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Eglantine.Engine.Pathfinding;

namespace Eglantine
{
	public class TestPather
	{
		Texture2D texture = null;
		public Vector2 Position;

		public List<NavNode> Waypoints = new List<NavNode>();
		public NavNode nextWaypoint;
		const float WAYPOINT_DISTANCE = 9f;
		const float MOVEMENT_SPEED = 60f;
		bool textureloaded = false;

		public TestPather (Vector2 pos)
		{
			Position = pos;
		}

		public void LoadContent (ContentManager content)
		{
			texture = content.Load<Texture2D> ("MayStickfigure");
			textureloaded = true;
		}

		public void Update (GameTime gameTime)
		{
			if (Waypoints.Count > 0 && nextWaypoint ==  null)
			{
				nextWaypoint = Waypoints[Waypoints.Count - 1];
				Waypoints.RemoveAt(Waypoints.Count - 1);
					Console.WriteLine("Move towards waypoint at: " + nextWaypoint.Position.X + ":" + nextWaypoint.Position.Y);
			}
			if (nextWaypoint != null)
			{
				Vector2 tempVector = new Vector2(nextWaypoint.Position.X, nextWaypoint.Position.Y);
				Vector2 moveVector = Vector2.Normalize(tempVector - Position) * MOVEMENT_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
				Position += moveVector;

				if(Vector2.Distance(tempVector, Position) <= WAYPOINT_DISTANCE)
				{
					// Pop the waypoint from the list, as it is no longer needed
					nextWaypoint = null;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if(textureloaded)
				spriteBatch.Draw(texture, new Vector2(Position.X - (texture.Width/2), Position.Y - (texture.Height - 25)), Color.White);
		}
	}
}

