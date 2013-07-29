using System;
using Microsoft.Xna.Framework;

namespace Eglantine.Engine
{
	public class PuzzleboxState
	{
		public const float RING_DRAG_MAGNITUDE = ((float)Math.PI * 2) / 36;

		// Change this.
		public bool RingsUnlocked = false;
		public bool PuzzleSolved { get; private set; }

		public float Ring1Rotation = 0f;
		public float Ring2Rotation = 0f;
		public float Ring3Rotation = 0f;
		public float Ring4Rotation = 0f;
		public float Ring5Rotation = 0f;
		public float Ring6Rotation = 0f;
		public float Ring7Rotation = 0f;

		public bool KeyInserted = false;

		public bool AriesPressed = false;
		public bool TaurusPressed = false;
		public bool GeminiPressed = false;
		public bool CancerPressed = false;
		public bool LeoPressed = false;
		public bool VirgoPressed = false;
		public bool LibraPressed = false;
		public bool ScorpioPressed = false;
		public bool SagittariusPressed = false;
		public bool CapricornPressed = false;
		public bool AquariusPressed = false;
		public bool PiscesPressed = false;

		public PuzzleboxState ()
		{
			// Setup
			RotateRing(1, -1, 7);
			RotateRing(2, 1, 15);
			RotateRing(3, -1, 22);
			RotateRing(4, 1, 33);
			RotateRing(5, 1, 4);
			RotateRing(6, 1, 28);
			RotateRing(7, -1, 19);

		}

		public void RotateRing (int ring, int direction, int times = 1)
		{
			switch (ring)
			{
			case(1):
				Ring1Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(2):
				Ring2Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(3):
				Ring3Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(4):
				Ring4Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(5):
				Ring5Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(6):
				Ring6Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			case(7):
				Ring7Rotation += RING_DRAG_MAGNITUDE * direction * times;
				break;
			default:
				break;
			}
		}

		public void CheckButtons()
		{
			if(ScorpioPressed && CancerPressed && PiscesPressed && LibraPressed && CapricornPressed && SagittariusPressed && VirgoPressed)
			{
				RingsUnlocked = true;
			}
			else
			{
				RingsUnlocked = false;
			}
		}

		public void CheckIfSolved ()
		{
			// This is an arbitrary limit to make sure that the rings are locked "close to 0"
			if (
				MathHelper.WrapAngle(Ring1Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring1Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring2Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring2Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring3Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring3Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring4Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring4Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring5Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring5Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring6Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring6Rotation) > RING_DRAG_MAGNITUDE / -3 &&
				MathHelper.WrapAngle(Ring7Rotation) < RING_DRAG_MAGNITUDE / 3 && MathHelper.WrapAngle(Ring7Rotation) > RING_DRAG_MAGNITUDE / -3
				)
			{
				TriggerPuzzleSolve ();
			}
			else
			{
				PuzzleSolved = false;
			}
		}

		public void TriggerPuzzleSolve()
		{
			PuzzleSolved = true;
			// Do the stuff to make the thing happen
		}
	}
}

