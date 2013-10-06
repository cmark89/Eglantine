using System;
using System.Collections.Generic;
using Eglantine.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eglantine
{
	public class AnimatedSprite
	{
		public readonly Texture2D Texture;
        // This is a stupid workaround for the Reach profile...
        public readonly Texture2D SecondTexture;

		public Vector2 Position;
		public Vector2 Origin;
		public Vector2 Scale;
		public Color Color;

		Dictionary<string, Animation> animations;
		Animation currentAnimation;
		int framesPerSecond;
		float frameRate;
		float nextFrameChangeTime;

		public string CurrentAnimationName { get; private set;}

		public AnimatedSprite (Texture2D tex, int fps, Texture2D secondTexture = null)
		{
			animations = new Dictionary<string, Animation>();
			framesPerSecond = fps;
			frameRate = 1f / fps;
			Texture = tex;

            if (secondTexture != null)
                SecondTexture = secondTexture;
		}

		public void AddAnimation (string name, int h, int w, int[] frames, bool loop)
		{
			animations.Add(name, new Animation(Texture, h ,w, frames, loop));
		}

		public void PlayAnimation(string name)
		{
			Console.WriteLine("Play animation " + name);
			if(currentAnimation != null && CurrentAnimationName.Contains("Walk") && name.Contains("Idle"))
			{
				// We are going from walking animation to an idle animation, so play the footstep sound
				EventManager.Instance.PlayFootprintSound();
			}

			currentAnimation = animations[name];
			CurrentAnimationName = name;

			// Reset the frame counter
			nextFrameChangeTime = frameRate;
			currentAnimation.SetFrame(0);
		}

		public void ChangeAnimation (string name)
		{


			if(CurrentAnimationName.Contains("Walk") && name.Contains("Idle"))
			{
				// We are going from walking animation to an idle animation, so play the footstep sound
				EventManager.Instance.PlayFootprintSound();
			}

			CurrentAnimationName = name;
			int currentIndex = currentAnimation.Index;
			currentAnimation = animations[name];
			currentAnimation.SetFrame(currentIndex);
		}

		public void Update (GameTime gameTime)
		{
			if (currentAnimation != null)
			{
				nextFrameChangeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				// Change the frame!
				if(nextFrameChangeTime <= 0f)
				{
					nextFrameChangeTime += frameRate;
					if(!currentAnimation.NextFrame())
					{
						Console.WriteLine(CurrentAnimationName + " finished");
						EventManager.Instance.SendSignal(CurrentAnimationName + " finished");

						if(CurrentAnimationName.Contains("Interact"))
						   PlayAnimation("Idle" + Player.Instance.CurrentFacing.ToString());
					}

					if(CurrentAnimationName.Contains("Walk") && (currentAnimation.Index % 10 == 2 || currentAnimation.Index % 10 == 7))
					{
						EventManager.Instance.PlayFootprintSound();
					}

					if(CurrentAnimationName.Contains("Interact"))
					{
						EventManager.Instance.SendSignal("Interact frame " + currentAnimation.Index);
					}
				}
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
            if (currentAnimation != null)
            {
                spriteBatch.Draw(currentAnimation.Texture, position: Position, sourceRectangle: currentAnimation.CurrentFrameRect(), origin: Origin, color: Color, scale: Scale);
                if (currentAnimation.CurrentFrame > 39)
                {
                    spriteBatch.Draw(SecondTexture, position: Position, sourceRectangle: currentAnimation.CurrentFrameRect(-40), origin: Origin, color: Color, scale: Scale);
                }
            }
		}
	}

	public class Animation
	{
		public int CurrentFrame 
		{ 
			get { return frameIndexes [index]; }
		}

		public Texture2D Texture { get; private set; }
		public int Index { get { return index; } }
		public int[] frameIndexes { get; private set; }
		private int index;

		readonly int frameHeight;
		readonly int frameWidth;
		readonly int columns;
		readonly int rows;
		readonly bool looping;

		public Animation(Texture2D tex, int h, int w, int[] frames, bool loop)
		{
			Texture = tex;
			frameIndexes = frames;
			frameHeight = h;
			frameWidth = w;
			looping = loop;

			columns = Texture.Width / frameWidth;
			rows = Texture.Height / frameHeight;

			index = 0;
		}

		public void SetFrame (int i)
		{
			if (i < frameIndexes.Length)
			{
				index = i;
			}
		}

		public bool NextFrame ()
		{
			index++;

			// If the animation is over...
			if (index >= frameIndexes.Length)
			{
				if(looping)
				{
					// Loop the animation.
					index = 0;
					return true;
				}
				else
				{
					// The animation has finished and it should not be looped.
					return false;
				}
			}
			else
			{
				// Nothing to report
				return true;
			}
		}

		public Rectangle CurrentFrameRect()
		{
			int column = CurrentFrame % columns;
			int row = CurrentFrame / columns;
			return new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
		}

        public Rectangle CurrentFrameRect(int mod)
        {
            int newFrame = CurrentFrame + mod;
            int column = newFrame % columns;
            int row = newFrame / columns;
            return new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
        }
	}
}

