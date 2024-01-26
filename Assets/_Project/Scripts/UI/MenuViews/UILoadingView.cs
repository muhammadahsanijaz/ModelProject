using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MoonKart.UI
{
    public class UILoadingView : MonoBehaviour
    {
        public bool loadFromStart;
        public Slider slider;
        private bool allowSceneActivation = false;

        void OnEnable()
        {
            if(loadFromStart)
            {
                slider.DOValue(1.0f, 5.0f).SetEase(Ease.Linear).OnComplete((() => { allowSceneActivation = true; }));
                StartCoroutine(LoadScene());
            }
        }

        IEnumerator LoadScene()
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu");
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = allowSceneActivation;
                }

                yield return null;
            }
        }
    }
}