using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [SerializeField]
    private Color m_enterColor,
        m_noMoneyColor;

    private Color m_startingColor;

    private Renderer m_renderer;

    public GameObject Turret;

    public TurretBlueprint UsedTurretBlueprint;

    public bool IsUpgraded;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        m_startingColor = m_renderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (!BuildManager.Instance.CanBuild)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.Instance.HasMoney)
        {
            m_renderer.material.color = m_noMoneyColor;
        }
        else
        {
            m_renderer.material.color = m_enterColor;
        }

      
    }

    private void OnMouseExit()
    {
        m_renderer.material.color = m_startingColor;
    }

    private void OnMouseDown()
    {
        

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Over UI");
            return;
        }
            

        if (Turret == null && BuildManager.Instance.CanBuild)
        {
            BuildManager.Instance.BuildTurret(this);
        }
        else if(Turret != null)
        {
            BuildManager.Instance.SetSelectedNode(this);
        }
        
    }

}
