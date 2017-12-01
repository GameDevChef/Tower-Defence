using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform Target;

    private Transform m_transform;

    [SerializeField]
    private float m_speed;

    [SerializeField]
    private float m_explosionRadius;

    [SerializeField]
    private GameObject m_effect;

    [SerializeField]
    private int m_damage;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
            

        Vector3 dir = Target.position - m_transform.position;
        m_transform.LookAt(Target);
        float distanceThisFrame = m_speed * Time.deltaTime;

        if(dir.sqrMagnitude <= distanceThisFrame * distanceThisFrame)
        {
            GameObject effect = Instantiate(m_effect, Target.position, Quaternion.identity);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);

            if (m_explosionRadius > 0)
            {
                Explode();
            }
            else
            {
                Damage(Target);
            }
            Destroy(gameObject);


        }
        else
        {
            m_transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(m_transform.position, m_explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                Damage(colliders[i].transform);
            }
            
        }
    }

    private void Damage(Transform e)
    {
        Enemy enemy = e.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
        }

    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_transform.position, m_explosionRadius);
    }
}
