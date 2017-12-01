using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private PlayerStats m_playerStats;

    [SerializeField]
    private GameOverScreen m_gameOverScreen;

    public bool IsGameOver;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is already one game manager");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        m_playerStats = FindObjectOfType<PlayerStats>();
    }

    public void Takelife()
    {
        m_playerStats.CurrentLives--;
        EnemySpawner.NumOfEnemies--;
        if(m_playerStats.CurrentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        m_gameOverScreen.gameObject.SetActive(true);
        m_gameOverScreen.Initialize(EnemySpawner.Instance.WaveNumber);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameOver();
        }
    }
}
