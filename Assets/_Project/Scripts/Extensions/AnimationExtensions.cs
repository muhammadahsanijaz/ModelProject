using UnityEngine;

namespace MoonKart
{
	public static class AnimationExtensions
	{
		public static void Sample(this Animation @this, float normalizedTime)
		{
			@this.Sample(@this.clip.name, normalizedTime);
		}

		public static void Sample(this Animation @this, string clipName, float normalizedTime)
		{
			AnimationState currentState = @this[clipName];

			float previousNormalizedTime = currentState.normalizedTime;
			float previousWeight         = currentState.weight;
			bool  previousEnabled        = currentState.enabled;

			currentState.normalizedTime  = normalizedTime;
			currentState.weight          = 1f;
			currentState.enabled         = true;

			@this.Sample();

			currentState.enabled         = previousEnabled;
			currentState.weight          = previousWeight;
			currentState.normalizedTime  = previousNormalizedTime;
		}
	}
}
