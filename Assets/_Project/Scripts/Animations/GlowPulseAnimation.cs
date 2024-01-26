using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MoonKart.UI
{

    public class GlowPulseAnimation : UIBehaviour
    {
        private bool playAnim;
        void OnEnable()
        {
            PlayAnimation();
        }

        public void PlayAnimation()
        {
            Image.DOFade(0.5f, 0f);
            Animation();
            playAnim = true;
        }

        public void StopAnimation()
        {
            playAnim = false;
        }

        private void Animation()
        {
            if (playAnim)
            {
                Image.DOFade(0.5f, 1f).OnComplete(() =>
                {
                    Image.DOFade(1f, 1f).OnComplete(() =>
                    {
                        Animation();
                    });
                });
            }
        }
    }   

      
    
}
