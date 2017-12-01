using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {


    [SerializeField]
    private SceneFader m_sceneFader;

    [SerializeField]
    private GameObject m_pauseUI;

    private void Awake()
    {
        m_sceneFader = FindObjectOfType<SceneFader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        Debug.Log("Toggle");
        m_pauseUI.SetActive(!m_pauseUI.activeSelf);

        if (m_pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void Retry()
    {
        Toggle();
        m_sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        m_sceneFader.FadeTo("Main Menu");
    }
}
