using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoonKart
{
	public class Loader : MonoBehaviour
	{
		// PUBLIC MEMBERS

		public event Action LoadingStarted;
		public event Action BeforeSceneLoad;
		public event Action UnloadResources;
		public event Action AfterSceneLoad;
		public event Action LoadingFinished;

		public bool IsLoading { get; private set; }

		public string LoadingSceneName { get; set; }
		public string SigneGameplaySceneName { get; set; }

		// PUBLIC METHODS

		public void LoadGameplayScene(string sceneName)
		{
			LoadScene(sceneName, LoadingSceneName, null);
		}

		public void LoadSingleGameplayScene(string sceneName)
		{
			LoadScene(sceneName,  SigneGameplaySceneName, null);
		}

		public void LoadScene(string sceneName)
		{
			LoadScene(sceneName, LoadingSceneName, null);
		}

		public void LoadScene(string sceneName, string loadingSceneName, string uiSceneName)
		{
			if (IsLoading == true)
				return;

			StartCoroutine(LoadSceneCoroutine(sceneName, loadingSceneName, uiSceneName));
		}

		// PRIVATE METHODS

		private IEnumerator LoadSceneCoroutine(string sceneName, string loadingSceneName, string uiSceneName)
		{
			IsLoading = true;
			LoadingStarted?.Invoke();

			var previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

			// Activating loading scene

			if (loadingSceneName.HasValue() == true)
			{
				AsyncOperation loadLoadingScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);
				while (loadLoadingScene.isDone == false)
					yield return null;
			}

			BeforeSceneLoad?.Invoke();

			// Deinitialize and unload old scene

			var previousGame = previousScene.GetComponentInScene<Game>();

			if (previousGame != null)
			{
				previousGame.Deinitialize();
			}

			yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(previousScene);

			UnloadResources?.Invoke();

			yield return Resources.UnloadUnusedAssets();

			// Load UI scene

			Scene uiScene = default;
			if (uiSceneName.HasValue() == true)
			{
				yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(uiSceneName, LoadSceneMode.Additive);
				uiScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(uiSceneName);
			}

			// Load new scene

			yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			Scene newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);

			if (uiScene.isLoaded == true)
			{
				UnityEngine.SceneManagement.SceneManager.MergeScenes(uiScene, newScene);
			}

			UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);

			AfterSceneLoad?.Invoke();

			// Deactivate and unload loading scene

			if (loadingSceneName.HasValue() == true)
			{
				Scene loadingScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(loadingSceneName);
				var loadingSceneGame = loadingScene.GetComponentInScene<LoadingScene>();

				if (loadingSceneGame != null)
				{
					yield return loadingSceneGame.Deactivate_Coroutine();
				}

				UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(loadingScene);
			}
			else
			{
				yield return null;
			}

			IsLoading = false;

			LoadingFinished?.Invoke();
		}
	}
}
