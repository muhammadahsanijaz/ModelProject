using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateObjectYoyo : MonoBehaviour
{
    public RectTransform thisObj;
    void Start()
    {
        float yPos = thisObj.anchoredPosition.y;
        thisObj.DOAnchorPosY((yPos + 5f), 2f).SetEase(Ease.InSine).SetLoops(-1,LoopType.Yoyo);
    }

}
