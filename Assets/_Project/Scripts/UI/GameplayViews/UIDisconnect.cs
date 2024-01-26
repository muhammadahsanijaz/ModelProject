using System.Collections;
using System.Collections.Generic;
using MoonKart;
using MoonKart.UI;
using UnityEngine;

public class UIDisconnect : UIView
{
    [SerializeField]
    private UIButton _leaveRaceButton;
    // Start is called before the first frame update
    void Start()
    {
        _leaveRaceButton.onClick.AddListener(OnLeaveRaceButton);
    }

    private void OnLeaveRaceButton()
    {
        (GameUI.Game as Gameplay).LoadMainMenu();
    }


}
