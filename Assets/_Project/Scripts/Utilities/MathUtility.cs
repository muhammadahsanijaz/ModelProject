using UnityEngine;

namespace MoonKart
{
	public static partial class MathUtility
	{
		// CONSTANTS

		public const float Epsilon = 0.01f;

		// PUBLIC METHODS

		public static float Map(float inMin, float inMax, float outMin, float outMax, float value)
		{
			if (value <= inMin)
				return outMin;

			if (value >= inMax)
				return outMax;

			return (outMax - outMin) * ((value - inMin) / (inMax - inMin)) + outMin;
		}

		public static float Map(Vector2 inRange, Vector2 outRange, float value)
		{
			return Map(inRange.x, inRange.y, outRange.x, outRange.y, value);
		}

		public static float ClampAngleTo180(float angle)
		{
			angle = angle % 360.0f;

			if (angle > 180.0f)
				return angle - 360.0f;

			if (angle < -180.0f)
				return angle + 360.0f;

			return angle;
		}
	}
}
