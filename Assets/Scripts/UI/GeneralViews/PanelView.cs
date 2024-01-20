using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonKart.UI {

    public class PanelView : UIBehaviour
    {
        public GameObject circleObject;
        public GameObject position;

        public void EnablePanel()
        {
            circleObject.transform.DOMove(position.transform.position, 1);
        }
    }
}
