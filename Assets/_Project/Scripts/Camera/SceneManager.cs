using System.Collections;
using System.Collections.Generic;
using MoonKart;
using UnityEngine;

/// <summary>
/// Scene manager that contains current player vehicle, current player camera, current player UI, current player character, recording/playing mechanim, and other vehicles as well.
/// 
/// </summary>
public class SceneManager : Singleton<SceneManager>
{
    // Default time scale of the game.
    private float orgTimeScale = 1f;

    public static AsyncOperation LoadSceneAsync(string scene2)
    {
        throw new System.NotImplementedException();
    }
}