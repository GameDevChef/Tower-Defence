using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private int m_currentTargetIndex;

    private Transform m_target;

    private Transform m_transform;


    [SerializeField]
    private float m_currentSpeed;

    [SerializeField]
    private float m_startSpeed;

    
   

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Start()
    {
        m_currentTargetIndex = 1;
        m_target = Waypoints.WaypointsArray[m_currentTargetIndex];
        m_currentSpeed = m_startSpeed;
    }

    

    private void Update()
    {
        Vector3 direction = (m_target.position - m_transform.position).normalized;
        float distance = m_currentSpeed * Time.deltaTime;
        m_transform.Translate(direction * distance, Space.World);

        float distanceToNextWaypointSqr = (m_target.position - m_transform.position).sqrMagnitude;
        if(distanceToNextWaypointSqr <= 0.2f)
        {
            m_currentTargetIndex++;
            if(m_currentTargetIndex == Waypoints.WaypointsArray.Length)
            {
                OnEnd();
                return;
            }
            m_target = Waypoints.WaypointsArray[m_currentTargetIndex];
        }
    }

    private void OnEnd()
    {
        gameObject.SetActive(false);
        EnemySpawner.Instance.EnemiesList.Remove(gameObject);
        GameManager.Instance.Takelife();
        
    }

    internal void SpeedUp()
    {
        m_currentSpeed = m_startSpeed;
    }

    internal void SlowDown(float m_slowPct)
    {
        m_currentSpeed *= m_slowPct;
    }
}
