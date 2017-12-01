using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private SceneFader m_sceneFader;

    [SerializeField]
    private string m_gameplaySceneName = "Level Select";

    public void Play()
    {
        m_sceneFader.FadeTo(m_gameplaySceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
