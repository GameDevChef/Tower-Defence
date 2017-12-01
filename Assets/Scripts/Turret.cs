using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Enemy m_enemy;
    private EnemyMovement m_enemyMovement;

    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private float m_range;

    private EnemySpawner m_enemySpawner;

    [SerializeField]
    private Transform m_partToRotate;

    [SerializeField]
    private float m_turnSpeed;

    [SerializeField]
    private Transform m_bulletSpawnTransform;

    [Header("Projectile")]
    [SerializeField]
    private float m_fireRate;

    private float fireCountDown;

    [SerializeField]
    private Bullet m_bulletPrefab;

    [Header("Laser")]
    [SerializeField]
    private bool m_isLaser;

    [SerializeField]
    private float m_slowPct;

    [SerializeField]
    private LineRenderer m_lineRenderer;

    [SerializeField]
    private Transform m_laserEffectTransform;

    [SerializeField]
    private float m_laserDamagePerSecond;
 

    private void Awake()
    {
        m_enemySpawner = FindObjectOfType<EnemySpawner>();
        if (m_isLaser)
        {
            m_laserEffectTransform = GetComponentInChildren<ParticleSystem>().transform;
            m_laserEffectTransform.gameObject.SetActive(false);
        }
        
    }

    private void Start()
    {
        InvokeRepeating("LookForTarget", 0f, 0.5f);
    }

    private void LookForTarget()
    {
        
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        for (int i = 0; i < m_enemySpawner.EnemiesList.Count; i++)
        {
            
            float distance = (m_enemySpawner.EnemiesList[i].transform.position - transform.position).sqrMagnitude;
          
            if (distance < shortestDistance)
            {
                nearestTarget = m_enemySpawner.EnemiesList[i];
                shortestDistance = distance;
            }

           
        }
        if (shortestDistance <= m_range * m_range && nearestTarget != null)
        {
            m_target = nearestTarget.transform;
            m_enemy = nearestTarget.GetComponent<Enemy>();
            m_enemyMovement = nearestTarget.GetComponent<EnemyMovement>();
        }
        else
        {
            Debug.Log("no enemy");
            if(m_enemyMovement != null)
            {
                m_enemyMovement.SpeedUp();
            }
            
            m_target = null;
        }
    }

    private void Update()
    {
        if (m_target == null)
        {
            if (m_isLaser)
            {
                if(m_lineRenderer.enabled == true)
                {
                    m_lineRenderer.enabled = false;
                    m_laserEffectTransform.gameObject.SetActive(false);
                }
            }
            return;
        }
          

        LookAtTarget();

        if (m_isLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1f / m_fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        m_enemy.TakeDamage(m_laserDamagePerSecond * Time.deltaTime);
        LaserGraphics();
    }

    private void LaserGraphics()
    {
        if (m_lineRenderer.enabled == false)
        {
            m_lineRenderer.enabled = true;
            m_laserEffectTransform.gameObject.SetActive(true);
            m_enemyMovement.SlowDown(m_slowPct);
        }
        m_lineRenderer.SetPosition(0, m_bulletSpawnTransform.position);
        m_lineRenderer.SetPosition(1, m_target.position);

        Vector3 dir = m_bulletSpawnTransform.position - m_target.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        m_laserEffectTransform.position = m_target.position + dir.normalized * 1.5f;
        m_laserEffectTransform.rotation = rotation;
    }

    private void LookAtTarget()
    {
        Vector3 dir = m_target.position - transform.position;
        Quaternion lookTotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(m_partToRotate.rotation, lookTotation, Time.deltaTime * m_turnSpeed).eulerAngles;
        m_partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    private void Shoot()
    {
        Bullet bullet = Instantiate(m_bulletPrefab, m_bulletSpawnTransform.position, 
            (m_bulletSpawnTransform.rotation));

        if(bullet != null)
        {
            bullet.Target = m_target;
        }
        
    }
}

