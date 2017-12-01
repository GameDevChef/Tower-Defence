using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    [SerializeField]
    private Text m_numOfRounds;

    [SerializeField]
    private SceneFader m_sceneFader;

    private void Awake()
    {
        m_sceneFader = FindObjectOfType<SceneFader>();
    }

    public void Initialize(int num)
    {
        m_numOfRounds.text = num.ToString();
    }

    public void Retry()
    {
        m_sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        m_sceneFader.FadeTo("Main Menu");
    }


}
