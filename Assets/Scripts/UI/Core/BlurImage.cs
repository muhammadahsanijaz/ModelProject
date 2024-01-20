using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoonKart.UI
{
	public class BlurImage : Image 
	{
		// PUBLIC METHODS

		public void RenderBlur(System.Type descriptor, Action onFinished, int delayFrames = 0)
		{
			StopAllCoroutines();
			StartCoroutine(RenderBlur_Coroutine(descriptor, onFinished, delayFrames));
		}

		public void ClearBlur(System.Type descriptor)
		{
			KawaseBlur.Instance.ReleaseTarget(descriptor);
		}

		// PRIVATE METHODS

		private IEnumerator RenderBlur_Coroutine(System.Type descriptor, Action onFinished, int delayFrames)
		{
			for (int i = 0; i < delayFrames; i++)
				yield return null;

			var kawaseBlur = KawaseBlur.Instance;

			kawaseBlur.SetTarget(descriptor);

			yield return kawaseBlur.RenderBlur(Color.white);

			kawaseBlur.SetGlobalTexture(descriptor);

			onFinished?.Invoke();
		}
	}
}
