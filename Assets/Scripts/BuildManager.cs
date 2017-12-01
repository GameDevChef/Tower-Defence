using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager Instance;

    private TurretBlueprint m_turretBlueprint;

    [SerializeField]
    private NodeUI m_nodeUI;

    private PlayerStats m_playerStats;

    public bool CanBuild { get { return m_turretBlueprint != null; } }
    public bool HasMoney { get { return m_playerStats.CurrentMoney >= m_turretBlueprint.Cost; } }

    [SerializeField]
    private Vector3 m_buildOffset;

    [SerializeField]
    private GameObject m_buildEffect;

    [SerializeField]
    private Node m_selectedNode;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Ther is already one build manager");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        m_playerStats = FindObjectOfType<PlayerStats>();
    }

    

    public TurretBlueprint GetTurretBlueprint()
    {
        return m_turretBlueprint;
    }

    public void SetTurretBluePrint(TurretBlueprint turretBlueprint)
    {
        m_turretBlueprint = turretBlueprint;
        DeselectNode();
    }

    public void SetSelectedNode(Node node)
    {
        Debug.Log("Select");
        if(m_selectedNode == node)
        {
            DeselectNode();
            Debug.Log("Hide");
            return;
        }
        m_selectedNode = node;
       // m_turretBlueprint = null;
        m_nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        m_selectedNode = null;
        m_nodeUI.Hide();
    }

    internal void BuildTurret(Node node)
    {
        if(m_playerStats.CurrentMoney >= m_turretBlueprint.Cost)
        {
            GameObject turret = Instantiate(m_turretBlueprint.Turret, node.transform.position + m_buildOffset, Quaternion.identity);
            node.Turret = turret;
            node.UsedTurretBlueprint = m_turretBlueprint;
            AddMoney(-m_turretBlueprint.Cost);
            GameObject buildEFfect = Instantiate(m_buildEffect, turret.transform.position, Quaternion.identity);
            Destroy(buildEFfect.gameObject, 2f);
        }
        else
        {
            Debug.Log("Not enough money");
        }
        
    }

    public void UpgradeTurret()
    {
        
        if (m_playerStats.CurrentMoney >= m_selectedNode.UsedTurretBlueprint.UpgradeCost)
        {
            m_selectedNode.IsUpgraded = true;
            Destroy(m_selectedNode.Turret);
            GameObject turret = Instantiate(m_selectedNode.UsedTurretBlueprint.UpgradedTurret, m_selectedNode.transform.position + m_buildOffset, Quaternion.identity);
            m_selectedNode.Turret = turret;
            AddMoney(-m_selectedNode.UsedTurretBlueprint.UpgradeCost);
            GameObject buildEFfect = Instantiate(m_buildEffect, turret.transform.position, Quaternion.identity);
            Destroy(buildEFfect.gameObject, 2f);
        }
        else
        {
            Debug.Log("Not enough money");
        }
        DeselectNode();

    }

    public void SellTurret()
    {
        m_playerStats.CurrentMoney += Mathf.RoundToInt(m_selectedNode.UsedTurretBlueprint.Cost * .5f);
        Destroy(m_selectedNode.Turret);
        m_selectedNode.UsedTurretBlueprint = null;
        m_selectedNode.IsUpgraded = false;
        DeselectNode();
    }



    internal void AddMoney(int value)
    {
        m_playerStats.CurrentMoney += value;
        Debug.Log(value);
    }
}
