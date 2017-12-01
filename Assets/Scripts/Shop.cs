using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject Turret;
    public int Cost;

    public GameObject UpgradedTurret;
    public int UpgradeCost;
}

public class Shop : MonoBehaviour {

    [SerializeField]
    private TurretBlueprint
        m_standardTurret,
        m_missileTurret,
        m_laserTurret;

    
    public void SelectStandardTurret()
    {
        BuildManager.Instance.SetTurretBluePrint(m_standardTurret);
    }

    public void SelectMissileTurret()
    {
        BuildManager.Instance.SetTurretBluePrint(m_missileTurret);
    }

    public void SelectLaserTurret()
    {
        BuildManager.Instance.SetTurretBluePrint(m_laserTurret);
    }
}
