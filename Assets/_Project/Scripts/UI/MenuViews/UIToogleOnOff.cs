using System.Collections;
using System.Collections.Generic;
using MoonKart.UI;
using UnityEngine;

public class UIToogleOnOff : MonoBehaviour
{
    [SerializeField] private UIToggle _toggle;
    [SerializeField] private GameObject[] _toggleOn;

    [SerializeField] private GameObject[] _toggleOff;

    // Start is called before the first frame update
    void Start()
    {
        if (_toggle == null)
            _toggle = GetComponent<UIToggle>();

        _toggle.onValueChanged.AddListener((arg0 => { Enable(_toggleOn, _toggleOff, arg0); }));
        Enable(_toggleOn, _toggleOff, _toggle.isOn);
    }

    private void Enable(GameObject[] toggleOn, GameObject[] toggleOff, bool value)
    {
        if (value)
        {
            foreach (var t in _toggleOn)
            {
                t.SetActive(true);
            }

            foreach (var t in _toggleOff)
            {
                t.SetActive(false);
            }
        }
        else
        {
            foreach (var t in _toggleOn)
            {
                t.SetActive(false);
            }

            foreach (var t in _toggleOff)
            {
                t.SetActive(true);
            }
        }
    }
}