using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private Image m_healthImage;

    [SerializeField]
    public int m_value;

    [SerializeField]
    public float m_startingHealth,
        m_health;

    private void Start()
    {
        m_health = m_startingHealth;
    }

    internal void TakeDamage(float damage)
    {
        m_health -= damage;
        m_healthImage.fillAmount = m_health / m_startingHealth;
        if (m_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        BuildManager.Instance.AddMoney(m_value);
        EnemySpawner.Instance.EnemiesList.Remove(gameObject);
        Destroy(gameObject);
        EnemySpawner.NumOfEnemies--;
    }
}
