using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public float Rate;

    public int count;

    public GameObject enemyPrefab;
}

public class EnemySpawner : MonoBehaviour {

    public static int NumOfEnemies;

    public static EnemySpawner Instance;

    [SerializeField]
    private Wave[] m_wavesArray;

    [SerializeField]
    private Text m_timerText;

    [SerializeField]
    private float m_deleyBetweenWaves;

    private Transform m_spawnTransform;

    [HideInInspector]
    public int WaveNumber;

    public List<GameObject> EnemiesList;

    private float m_countDown;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Ther is already one build manager");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        EnemiesList = new List<GameObject>();
        WaveNumber = 0;
        m_spawnTransform = Waypoints.WaypointsArray[0];
        m_countDown = m_deleyBetweenWaves;
    }

    private IEnumerator SpawnEnemiesCO()
    {
        Wave wave = m_wavesArray[WaveNumber];
        NumOfEnemies = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f/wave.Rate);
        }
        WaveNumber++;
        if(WaveNumber == m_wavesArray.Length)
        {
            Debug.Log("WIN");
            this.enabled = false;
        }
      
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy =  Instantiate(enemyPrefab, m_spawnTransform.position, Quaternion.identity) as GameObject;
        EnemiesList.Add(enemy);
        
    }

    private void Update()
    {
        if(NumOfEnemies > 0)
        {
            return;
        }
        if(m_countDown <= 0f)
        {
            StartCoroutine(SpawnEnemiesCO());
            m_countDown = m_deleyBetweenWaves;
            return;
        }

        m_countDown -= Time.deltaTime;
        m_timerText.text = m_countDown.ToString("0.0");

    }
}
