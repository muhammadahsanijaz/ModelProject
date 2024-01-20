namespace MoonKart
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public static class SceneExtensions
	{
		// PUBLIC METHODS

		public static T GetComponentInScene<T>(this Scene scene, bool includeInactive = false) where T : class
		{
			List<GameObject> roots = new List<GameObject>(16);
			scene.GetRootGameObjects(roots);

			T component = default;

			for (int i = 0, count = roots.Count; i < count; ++i)
			{
				component = roots[i].GetComponentInChildren<T>(includeInactive);
				if (component != null)
					break;
			}

			
			return component;
		}
	}
}
