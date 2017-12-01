using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    [SerializeField]
    private SceneFader m_sceneFader;

    public void SelectLevel(string sceneName)
    {
        m_sceneFader.FadeTo(sceneName);
    }
}
